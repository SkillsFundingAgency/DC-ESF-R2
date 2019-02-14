using ESFA.DC.ESF.R2.Database.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class ESFR2Context : IESFR2Context

    {
        DbSet<SourceFile> IESFR2Context.SourceFiles => SourceFiles;

        DbSet<SupplementaryData> IESFR2Context.SupplementaryDatas => SupplementaryDatas;

        DbSet<SupplementaryDataUnitCost> IESFR2Context.SupplementaryDataUnitCosts => SupplementaryDataUnitCosts;

        DbSet<ValidationError> IESFR2Context.ValidationErrors => ValidationErrors;
    }
}
