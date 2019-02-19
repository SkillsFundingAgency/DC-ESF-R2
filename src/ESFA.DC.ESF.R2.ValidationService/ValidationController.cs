using System;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ESFA.DC.ESF.R2.ValidationService
{
    public class ValidationController : IValidationController
    {
        private readonly ILooseValidatorCommand _looseValidatorCommand;
        private readonly IList<IValidatorCommand> _validatorCommands;
        private readonly IPopulationService _populationService;
        private readonly ISupplementaryDataModelMapper _mapper;

        public ValidationController(
            ILooseValidatorCommand looseValidatorCommand,
            IList<IValidatorCommand> validatorCommands,
            IPopulationService populationService,
            ISupplementaryDataModelMapper mapper)
        {
            _looseValidatorCommand = looseValidatorCommand;
            _validatorCommands = validatorCommands.OrderBy(c => c.Priority).ToList();
            _populationService = populationService;
            _mapper = mapper;
        }

        public bool RejectFile { get; private set; }

        public void ValidateData(
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            foreach (var looseModel in wrapper.SupplementaryDataLooseModels)
            {
                if (_looseValidatorCommand.Execute(looseModel))
                {
                    continue;
                }

                foreach (var error in _looseValidatorCommand.Errors)
                {
                    wrapper.ValidErrorModels.Add(error);
                }

                if (!_looseValidatorCommand.RejectFile)
                {
                    continue;
                }

                return;
            }

            wrapper.SupplementaryDataLooseModels = FilterOutInvalidLooseRows(wrapper);

            wrapper.SupplementaryDataModels = wrapper.SupplementaryDataLooseModels.Select(m => _mapper.GetSupplementaryDataModelFromLooseModel(m)).ToList();

            var allUlns = wrapper.SupplementaryDataModels.Select(m => m.ULN).ToList();
            _populationService.PrePopulateUlnCache(allUlns, cancellationToken);

            var ukPrn = Convert.ToInt32(sourceFile.UKPRN);
            _populationService.PrePopulateContractAllocations(ukPrn, wrapper.SupplementaryDataModels, cancellationToken);

            _populationService.PrePopulateContractDeliverableUnitCosts(ukPrn, cancellationToken);

            foreach (var command in _validatorCommands)
            {
                if (command is ICrossRecordCommand)
                {
                    ((ICrossRecordCommand)command).AllRecords = wrapper.SupplementaryDataModels;
                }

                foreach (var model in wrapper.SupplementaryDataModels)
                {
                    if (command.IsValid(model))
                    {
                        continue;
                    }

                    foreach (var error in command.Errors)
                    {
                        wrapper.ValidErrorModels.Add(error);
                    }

                    if (!command.RejectFile)
                    {
                        continue;
                    }

                    RejectFile = true;
                    return;
                }
            }

            wrapper.SupplementaryDataModels = FilterOutInvalidRows(wrapper);
        }

        private IList<SupplementaryDataLooseModel> FilterOutInvalidLooseRows(
            SupplementaryDataWrapper wrapper)
        {
            return wrapper.SupplementaryDataLooseModels.Where(model => !wrapper.ValidErrorModels.Any(e => e.ConRefNumber == model.ConRefNumber
                                                                                                     && e.DeliverableCode == model.DeliverableCode
                                                                                                     && e.CalendarYear == model.CalendarYear
                                                                                                     && e.CalendarMonth == model.CalendarMonth
                                                                                                     && e.ReferenceType == model.ReferenceType
                                                                                                     && e.Reference == model.Reference
                                                                                                     && !e.IsWarning)).ToList();
        }

        private IList<SupplementaryDataModel> FilterOutInvalidRows(
            SupplementaryDataWrapper wrapper)
        {
            return wrapper.SupplementaryDataModels.Where(model => !wrapper.ValidErrorModels.Any(e => e.ConRefNumber == model.ConRefNumber
                                                                                                     && e.DeliverableCode == model.DeliverableCode
                                                                                                     && e.CalendarYear == model.CalendarYear.ToString()
                                                                                                     && e.CalendarMonth == model.CalendarMonth.ToString()
                                                                                                     && e.ReferenceType == model.ReferenceType
                                                                                                     && e.Reference == model.Reference
                                                                                                     && !e.IsWarning)).ToList();
        }
    }
}
