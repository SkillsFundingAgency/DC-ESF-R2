using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Mappers
{
    public class SourceFileModelMapper : ISourceFileModelMapper
    {
        public SourceFileModel GetModelFromEntity(SourceFile entity)
        {
            return new SourceFileModel
            {
                SourceFileId = entity.SourceFileId,
                ConRefNumber = entity.ConRefNumber,
                UKPRN = entity.Ukprn,
                FileName = entity.FileName,
                PreparationDate = entity.FilePreparationDate,
                SuppliedDate = entity.DateTime
            };
        }
    }
}