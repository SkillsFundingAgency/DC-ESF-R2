using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.ValidEntities
{
    public partial class ILR1819ValidLearnerContext : DbContext
    {
        public ILR1819ValidLearnerContext()
        {
        }

        public ILR1819ValidLearnerContext(DbContextOptions<ILR1819ValidLearnerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppFinRecord> AppFinRecords { get; set; }
        public virtual DbSet<CollectionDetail> CollectionDetails { get; set; }
        public virtual DbSet<ContactPreference> ContactPreferences { get; set; }
        public virtual DbSet<Dpoutcome> Dpoutcomes { get; set; }
        public virtual DbSet<EmploymentStatusMonitoring> EmploymentStatusMonitorings { get; set; }
        public virtual DbSet<Learner> Learners { get; set; }
        public virtual DbSet<LearnerDestinationandProgression> LearnerDestinationandProgressions { get; set; }
        public virtual DbSet<LearnerEmploymentStatus> LearnerEmploymentStatuses { get; set; }
        public virtual DbSet<LearnerFam> LearnerFams { get; set; }
        public virtual DbSet<LearnerHe> LearnerHes { get; set; }
        public virtual DbSet<LearnerHefinancialSupport> LearnerHefinancialSupports { get; set; }
        public virtual DbSet<LearningDelivery> LearningDeliveries { get; set; }
        public virtual DbSet<LearningDeliveryFam> LearningDeliveryFams { get; set; }
        public virtual DbSet<LearningDeliveryHe> LearningDeliveryHes { get; set; }
        public virtual DbSet<LearningDeliveryWorkPlacement> LearningDeliveryWorkPlacements { get; set; }
        public virtual DbSet<LearningProvider> LearningProviders { get; set; }
        public virtual DbSet<LlddandHealthProblem> LlddandHealthProblems { get; set; }
        public virtual DbSet<ProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitorings { get; set; }
        public virtual DbSet<ProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<SourceFile> SourceFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=ILR1819_DataStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<AppFinRecord>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AfinType, e.AfinCode, e.AfinDate });

                entity.ToTable("AppFinRecord", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AfinType })
                    .HasName("IX_Valid_AppFinRecord");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AfinType)
                    .HasColumnName("AFinType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AfinCode).HasColumnName("AFinCode");

                entity.Property(e => e.AfinDate)
                    .HasColumnName("AFinDate")
                    .HasColumnType("date");

                entity.Property(e => e.AfinAmount).HasColumnName("AFinAmount");

                entity.HasOne(d => d.LearningDelivery)
                    .WithMany(p => p.AppFinRecords)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppFinRecord_LearningDelivery");
            });

            modelBuilder.Entity<CollectionDetail>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.Collection, e.Year });

                entity.ToTable("CollectionDetails", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.Collection)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FilePreparationDate).HasColumnType("date");
            });

            modelBuilder.Entity<ContactPreference>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.ContPrefType, e.ContPrefCode })
                    .HasName("PK_ContactPref");

                entity.ToTable("ContactPreference", "Valid");

                entity.HasIndex(e => new { e.LearnRefNumber, e.ContPrefType })
                    .HasName("IX_Valid_ContactPreference");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ContPrefType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.ContactPreferences)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactPreference_Learner");
            });

            modelBuilder.Entity<Dpoutcome>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.OutType, e.OutCode, e.OutStartDate, e.OutCollDate });

                entity.ToTable("DPOutcome", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OutType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.OutStartDate).HasColumnType("date");

                entity.Property(e => e.OutCollDate).HasColumnType("date");

                entity.Property(e => e.OutEndDate).HasColumnType("date");

                entity.HasOne(d => d.LearnerDestinationandProgression)
                    .WithMany(p => p.Dpoutcomes)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DPOutcome_LearnerDestinationandProgression");
            });

            modelBuilder.Entity<EmploymentStatusMonitoring>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.DateEmpStatApp, e.Esmtype })
                    .HasName("PK__Employme__316BBA3161A1A6FD");

                entity.ToTable("EmploymentStatusMonitoring", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DateEmpStatApp).HasColumnType("date");

                entity.Property(e => e.Esmtype)
                    .HasColumnName("ESMType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Esmcode).HasColumnName("ESMCode");
            });

            modelBuilder.Entity<Learner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__Learner__2770A72764515783");

                entity.ToTable("Learner", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Alscost).HasColumnName("ALSCost");

                entity.Property(e => e.CampId)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EngGrade)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GivenNames)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LlddhealthProb).HasColumnName("LLDDHealthProb");

                entity.Property(e => e.MathGrade)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Ninumber)
                    .HasColumnName("NINumber")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Otjhours).HasColumnName("OTJHours");

                entity.Property(e => e.PlanEephours).HasColumnName("PlanEEPHours");

                entity.Property(e => e.Pmukprn).HasColumnName("PMUKPRN");

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PostcodePrior)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PrevLearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PrevUkprn).HasColumnName("PrevUKPRN");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TelNo)
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<LearnerDestinationandProgression>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__LearnerD__2770A727EB08337A");

                entity.ToTable("LearnerDestinationandProgression", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<LearnerEmploymentStatus>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.DateEmpStatApp })
                    .HasName("PK__LearnerE__7200C4BE7FC82404");

                entity.ToTable("LearnerEmploymentStatus", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DateEmpStatApp).HasColumnType("date");

                entity.Property(e => e.AgreeId)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.LearnerEmploymentStatuses)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerEmploymentStatus_Learner");
            });

            modelBuilder.Entity<LearnerFam>(entity =>
            {
                entity.HasKey(e => new { e.LearnFamcode, e.LearnFamtype, e.LearnRefNumber, e.Ukprn });

                entity.ToTable("LearnerFAM", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("IX_Valid_LearnerFAM");

                entity.Property(e => e.LearnFamcode).HasColumnName("LearnFAMCode");

                entity.Property(e => e.LearnFamtype)
                    .HasColumnName("LearnFAMType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.LearnerFams)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerFAM_Learner");
            });

            modelBuilder.Entity<LearnerHe>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__LearnerH__2770A7273A51E0B3");

                entity.ToTable("LearnerHE", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ttaccom).HasColumnName("TTACCOM");

                entity.Property(e => e.Ucasperid)
                    .HasColumnName("UCASPERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Learner)
                    .WithOne(p => p.LearnerHe)
                    .HasForeignKey<LearnerHe>(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerHE_Learner");
            });

            modelBuilder.Entity<LearnerHefinancialSupport>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Fintype })
                    .HasName("PK__LearnerH__09F54B72B911450C");

                entity.ToTable("LearnerHEFinancialSupport", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Fintype).HasColumnName("FINTYPE");

                entity.Property(e => e.Finamount).HasColumnName("FINAMOUNT");

                entity.HasOne(d => d.LearnerHe)
                    .WithMany(p => p.LearnerHefinancialSupports)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerHEFinancialSupport_LearnerHE");
            });

            modelBuilder.Entity<LearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__Learning__0C29443A653C0702");

                entity.ToTable("LearningDelivery", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchDate).HasColumnType("date");

                entity.Property(e => e.ConRefNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DelLocPostCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.EpaorgId)
                    .HasColumnName("EPAOrgID")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.LearnActEndDate).HasColumnType("date");

                entity.Property(e => e.LearnAimRef)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnPlanEndDate).HasColumnType("date");

                entity.Property(e => e.LearnStartDate).HasColumnType("date");

                entity.Property(e => e.OrigLearnStartDate).HasColumnType("date");

                entity.Property(e => e.OutGrade)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerUkprn).HasColumnName("PartnerUKPRN");

                entity.Property(e => e.SwsupAimId)
                    .HasColumnName("SWSupAimId")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.LearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearningDelivery_Learner");
            });

            modelBuilder.Entity<LearningDeliveryFam>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearningDeliveryFamId });

                entity.ToTable("LearningDeliveryFAM", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.LearnDelFamtype, e.LearnDelFamdateFrom })
                    .HasName("IX_Valid_LearningDeliveryFAM");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearningDeliveryFamId).HasColumnName("LearningDeliveryFAM_Id");

                entity.Property(e => e.LearnDelFamcode)
                    .IsRequired()
                    .HasColumnName("LearnDelFAMCode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelFamdateFrom)
                    .HasColumnName("LearnDelFAMDateFrom")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelFamdateTo)
                    .HasColumnName("LearnDelFAMDateTo")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelFamtype)
                    .IsRequired()
                    .HasColumnName("LearnDelFAMType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.LearningDelivery)
                    .WithMany(p => p.LearningDeliveryFams)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearningDeliveryFAM_LearningDelivery");
            });

            modelBuilder.Entity<LearningDeliveryHe>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__Learning__0C29443A4E6DF807");

                entity.ToTable("LearningDeliveryHE", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Domicile)
                    .HasColumnName("DOMICILE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Elq).HasColumnName("ELQ");

                entity.Property(e => e.Fundcomp).HasColumnName("FUNDCOMP");

                entity.Property(e => e.Fundlev).HasColumnName("FUNDLEV");

                entity.Property(e => e.Grossfee).HasColumnName("GROSSFEE");

                entity.Property(e => e.HepostCode)
                    .HasColumnName("HEPostCode")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Modestud).HasColumnName("MODESTUD");

                entity.Property(e => e.Mstufee).HasColumnName("MSTUFEE");

                entity.Property(e => e.Netfee).HasColumnName("NETFEE");

                entity.Property(e => e.Numhus)
                    .HasColumnName("NUMHUS")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pcfldcs)
                    .HasColumnName("PCFLDCS")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Pcolab)
                    .HasColumnName("PCOLAB")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Pcsldcs)
                    .HasColumnName("PCSLDCS")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Pctldcs)
                    .HasColumnName("PCTLDCS")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Qualent3)
                    .HasColumnName("QUALENT3")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Sec).HasColumnName("SEC");

                entity.Property(e => e.Soc2000).HasColumnName("SOC2000");

                entity.Property(e => e.Specfee).HasColumnName("SPECFEE");

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Stuload)
                    .HasColumnName("STULOAD")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Typeyr).HasColumnName("TYPEYR");

                entity.Property(e => e.Ucasappid)
                    .HasColumnName("UCASAPPID")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Yearstu).HasColumnName("YEARSTU");

                entity.HasOne(d => d.LearningDelivery)
                    .WithOne(p => p.LearningDeliveryHe)
                    .HasForeignKey<LearningDeliveryHe>(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearningDeliveryHE_LearningDelivery");
            });

            modelBuilder.Entity<LearningDeliveryWorkPlacement>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.WorkPlaceStartDate });

                entity.ToTable("LearningDeliveryWorkPlacement", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.WorkPlaceStartDate, e.WorkPlaceMode })
                    .HasName("IX_Valid_LearningDeliveryWorkPlacement");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.WorkPlaceStartDate).HasColumnType("date");

                entity.Property(e => e.WorkPlaceEndDate).HasColumnType("date");

                entity.HasOne(d => d.LearningDelivery)
                    .WithMany(p => p.LearningDeliveryWorkPlacements)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearningDeliveryWorkPlacement_LearningDelivery");
            });

            modelBuilder.Entity<LearningProvider>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__Learning__50F26B71E3050B0B");

                entity.ToTable("LearningProvider", "Valid");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<LlddandHealthProblem>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Llddcat, e.LlddandHealthProblemId })
                    .HasName("PK__LLDDandH__CFA94E1C6C8E4A82");

                entity.ToTable("LLDDandHealthProblem", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Llddcat).HasColumnName("LLDDCat");

                entity.Property(e => e.LlddandHealthProblemId).HasColumnName("LLDDandHealthProblem_ID");

                entity.Property(e => e.PrimaryLldd).HasColumnName("PrimaryLLDD");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.LlddandHealthProblems)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LLDDandHealthProblem_Learner");
            });

            modelBuilder.Entity<ProviderSpecDeliveryMonitoring>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.ProvSpecDelMonOccur })
                    .HasName("PK__Provider__9F5C5085F4167A8B");

                entity.ToTable("ProviderSpecDeliveryMonitoring", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecDelMonOccur)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecDelMon)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.LearningDelivery)
                    .WithMany(p => p.ProviderSpecDeliveryMonitorings)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProviderSpecDeliveryMonitoring_LearningDelivery");
            });

            modelBuilder.Entity<ProviderSpecLearnerMonitoring>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.ProvSpecLearnMonOccur })
                    .HasName("PK__Provider__63E551EA1B973F81");

                entity.ToTable("ProviderSpecLearnerMonitoring", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecLearnMonOccur)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecLearnMon)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.ProviderSpecLearnerMonitorings)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProviderSpecLearnerMonitoring_Learner");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("Source", "Valid");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.ComponentSetVersion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.ProtectiveMarking)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceData)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Release)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwarePackage)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwareSupplier)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SourceFile>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.SourceFileName });

                entity.ToTable("SourceFile", "Valid");

                entity.HasIndex(e => e.SourceFileName)
                    .HasName("IX_Valid_SourceFile");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.SourceFileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.FilePreparationDate).HasColumnType("date");

                entity.Property(e => e.Release)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwarePackage)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwareSupplier)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });
        }
    }
}
