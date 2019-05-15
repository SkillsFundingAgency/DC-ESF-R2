using System;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.R2.Database.EF.Interfaces
{
    public interface IESFR2Context : IDisposable
    {
        DbSet<SourceFile> SourceFiles { get; }

        DbSet<SupplementaryData> SupplementaryDatas { get; }

        DbSet<SupplementaryDataUnitCost> SupplementaryDataUnitCosts { get; }

        DbSet<ValidationError> ValidationErrors { get; }

        DbSet<ValidationErrorMessage> ValidationErrorMessages { get; }
    }
}