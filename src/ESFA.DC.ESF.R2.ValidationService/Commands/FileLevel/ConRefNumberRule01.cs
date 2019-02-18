using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel
{
    public class ConRefNumberRule01 : IFileLevelValidator
    {
        public string ErrorName => "ConRefNumber_01";

        public bool IsWarning => false;

        public string ErrorMessage => "There is a discrepency between the filename ConRefNumber and ConRefNumbers within the file.";

        public bool RejectFile => true;

        public bool Execute(SourceFileModel sourceFileModel, SupplementaryDataLooseModel model)
        {
            string[] filenameParts = FileNameHelper.SplitFileName(sourceFileModel.FileName);

            return filenameParts[2] == model.ConRefNumber;
        }
    }
}
