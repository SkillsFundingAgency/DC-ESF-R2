
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface ISourceFileModelMapper
    {
        SourceFileModel GetModelFromEntity(SourceFile entity);
    }
}