using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class ESFR2Context : DbContext
    {
        public ESFR2Context()
        {
        }

        public ESFR2Context(DbContextOptions<ESFR2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<SourceFile> SourceFiles { get; set; }
        public virtual DbSet<SupplementaryData> SupplementaryDatas { get; set; }
        public virtual DbSet<SupplementaryDataUnitCost> SupplementaryDataUnitCosts { get; set; }
        public virtual DbSet<ValidationError> ValidationErrors { get; set; }

        // Unable to generate entity type for table 'dbo.VersionInfo'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ESF_R2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "3.0.0-preview.19074.3");

            modelBuilder.Entity<SourceFile>(entity =>
            {
                entity.ToTable("SourceFile");

                entity.Property(e => e.ConRefNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FilePreparationDate).HasColumnType("datetime");

                entity.Property(e => e.Ukprn)
                    .IsRequired()
                    .HasColumnName("UKPRN")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SupplementaryData>(entity =>
            {
                entity.HasKey(e => new { e.ConRefNumber, e.DeliverableCode, e.CalendarYear, e.CalendarMonth, e.CostType, e.ReferenceType, e.Reference });

                entity.ToTable("SupplementaryData");

                entity.Property(e => e.ConRefNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CostType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnAimRef)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderSpecifiedReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupplementaryDataId).ValueGeneratedOnAdd();

                entity.Property(e => e.SupplementaryDataPanelDate).HasColumnType("date");

                entity.Property(e => e.Uln).HasColumnName("ULN");

                entity.Property(e => e.Value).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.SourceFile)
                    .WithMany(p => p.SupplementaryDatas)
                    .HasForeignKey(d => d.SourceFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SourceFile");
            });

            modelBuilder.Entity<SupplementaryDataUnitCost>(entity =>
            {
                entity.HasKey(e => new { e.ConRefNumber, e.DeliverableCode, e.CalendarYear, e.CalendarMonth, e.CostType, e.ReferenceType, e.Reference });

                entity.ToTable("SupplementaryDataUnitCost");

                entity.Property(e => e.ConRefNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CostType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.SupplementaryData)
                    .WithOne(p => p.SupplementaryDataUnitCost)
                    .HasForeignKey<SupplementaryDataUnitCost>(d => new { d.ConRefNumber, d.DeliverableCode, d.CalendarYear, d.CalendarMonth, d.CostType, d.ReferenceType, d.Reference })
                    .HasConstraintName("FK_SupplementaryDataUnitCost_SupplementaryData");
            });

            modelBuilder.Entity<ValidationError>(entity =>
            {
                entity.HasKey(e => new { e.SourceFileId, e.ValidationErrorId })
                    .HasName("PK__Validati__97356EBC17BFEB82");

                entity.ToTable("ValidationError");

                entity.Property(e => e.ValidationErrorId)
                    .HasColumnName("ValidationError_Id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CalendarMonth).IsUnicode(false);

                entity.Property(e => e.CalendarYear).IsUnicode(false);

                entity.Property(e => e.ConRefNumber).IsUnicode(false);

                entity.Property(e => e.CostType).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeliverableCode).IsUnicode(false);

                entity.Property(e => e.ErrorMessage).IsUnicode(false);

                entity.Property(e => e.LearnAimRef).IsUnicode(false);

                entity.Property(e => e.ProviderSpecifiedReference).IsUnicode(false);

                entity.Property(e => e.Reference).IsUnicode(false);

                entity.Property(e => e.ReferenceType).IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Severity)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName).IsUnicode(false);

                entity.Property(e => e.SupplementaryDataPanelDate).IsUnicode(false);

                entity.Property(e => e.Uln)
                    .HasColumnName("ULN")
                    .IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);
            });
        }
    }
}
