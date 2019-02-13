using System.Data.Entity;
using ESFA.DC.ESF.R2.Database.EF;

namespace ESFA.DC.ESF.Database.EF.Interfaces
{
    public interface IESF_R2Entities
    {
        DbSet<SourceFile> SourceFiles { get; set; }

        DbSet<SupplementaryData> SupplementaryDatas { get; set; }

        DbSet<SupplementaryDataUnitCost> SupplementaryDataUnitCosts { get; set; }

        DbSet<ValidationError> ValidationErrors { get; set; }

        DbSet<VersionInfo> VersionInfoes { get; set; }
    }
}