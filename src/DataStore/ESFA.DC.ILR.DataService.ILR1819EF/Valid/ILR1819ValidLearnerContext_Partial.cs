using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Valid
{
    public partial class ILR1819ValidLearnerContext
    {
        public virtual DbSet<LearnerDetailsEntity> LearnerDetails { get; set; }

        public virtual DbSet<PeriodEndMetricsEntity> PeriodEndMetrics { get; set; }
    }
}