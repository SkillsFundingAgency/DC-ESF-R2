using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Builders;

namespace ESFA.DC.ESF.R2.ValidationService.Commands
{
    public class FieldDefinitionCommand : ILooseValidatorCommand
    {
        private readonly IList<IFieldDefinitionValidator> _validators;

        public FieldDefinitionCommand(IList<IFieldDefinitionValidator> validators)
        {
            _validators = validators;
        }

        public IList<ValidationErrorModel> Errors { get; private set; }

        public bool RejectFile => false;

        public int Priority => 2;

        public bool Execute(SupplementaryDataLooseModel model)
        {
            Errors = new List<ValidationErrorModel>();

            foreach (var validator in _validators)
            {
                if (!validator.IsValid(model))
                {
                    Errors.Add(ValidationErrorBuilder.BuildValidationErrorModel(model, validator));
                }
            }

            return !Errors.Any();
        }
    }
}
