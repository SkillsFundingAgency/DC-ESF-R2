using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class ILR1920ValidLearnerContext
    {
        public virtual DbSet<LearnerDetailsEntity> LearnerDetails { get; set; }

        public virtual DbSet<PeriodEndMetricsEntity> PeriodEndMetrics { get; set; }
    }
}