using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Builders;

namespace ESFA.DC.ESF.R2.ValidationService.Commands
{
    public class CrossRecordCommands : ICrossRecordCommand
    {
        private readonly IList<ICrossRecordValidator> _validators;

        public CrossRecordCommands(IList<ICrossRecordValidator> validators)
        {
            _validators = validators;
        }

        public IList<ValidationErrorModel> Errors { get; private set; }

        public bool RejectFile => false;

        public int Priority => 3;

        public IList<SupplementaryDataModel> AllRecords { get; set; }

        public bool IsValid(SupplementaryDataModel model)
        {
            Errors = new List<ValidationErrorModel>();

            foreach (var validator in _validators)
            {
                if (!validator.IsValid(AllRecords, model))
                {
                    Errors.Add(ValidationErrorBuilder.BuildValidationErrorModel(model, validator));
                }
            }

            return !Errors.Any();
        }
    }
}
