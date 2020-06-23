using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Builders
{
    public interface ISourceFileModelBuilder
    {
        ISourceFileModel Build(IEsfJobContext esfJobContext);

        ISourceFileModel BuildDefault(IEsfJobContext esfJobContext);

    }
}