using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface ISupplementaryDataModelMapper
    {
        SupplementaryDataModel GetModelFromEntity(SupplementaryData entity);

        SupplementaryDataModel GetSupplementaryDataModelFromLooseModel(SupplementaryDataLooseModel looseModel);
    }
}