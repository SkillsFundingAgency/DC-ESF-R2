using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.FundingData.Database.EF
{
    public partial class ESFFundingDataContext : DbContext
    {
        public ESFFundingDataContext()
        {
        }

        public ESFFundingDataContext(DbContextOptions<ESFFundingDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ESFFundingData> ESFFundingDatas { get; set; }
        public virtual DbSet<LatestProviderSubmission> LatestProviderSubmissions { get; set; }
        public virtual DbQuery<ESFFundingData> vw_ESFFundingDatas { get; set; }
        public virtual DbQuery<LatestProviderSubmission> vw_LatestProviderSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ESFFundingData;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<ESFFundingData>(entity =>
            {
                entity.HasKey(e => new { e.AcademicYear, e.AttributeName, e.UKPRN, e.CollectionType, e.CollectionReturnCode, e.LearnRefNumber, e.AimSeqNumber, e.ConRefNumber, e.DeliverableCode });

                entity.ToTable("ESFFundingData", "Current");

                entity.Property(e => e.AcademicYear)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CollectionType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CollectionReturnCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ConRefNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Period_1).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_10).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_11).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_12).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_2).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_3).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_4).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_5).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_6).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_7).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_8).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period_9).HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<LatestProviderSubmission>(entity =>
            {
                entity.HasKey(e => new { e.UKPRN, e.CollectionType, e.CollectionReturnCode });

                entity.ToTable("LatestProviderSubmission", "Current");

                entity.Property(e => e.CollectionType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CollectionReturnCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Query<ESFFundingData>().ToView("ESFFundingData", "dbo");
            modelBuilder.Query<LatestProviderSubmission>().ToView("LatestProviderSubmissions","dbo");
        }
    }
}
