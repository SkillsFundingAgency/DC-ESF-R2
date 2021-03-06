﻿using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel
{
    public class ConRefNumberRule01 : BaseValidationRule, IFileLevelValidator
    {
        private const string _filenameExtension = @"\.csv";

        public ConRefNumberRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.ConRefNumber_01;

        public bool IsWarning => false;

        public bool RejectFile => true;

        public async Task<bool> IsValid(ISourceFileModel sourceFileModel, SupplementaryDataLooseModel model)
        {
            string[] filenameParts = sourceFileModel.FileName.SplitFileName(_filenameExtension);

            return filenameParts[2] == model.ConRefNumber;
        }
    }
}
