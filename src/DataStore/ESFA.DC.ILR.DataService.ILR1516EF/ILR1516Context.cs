using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class ILR1516Context : DbContext
    {
        public ILR1516Context()
        {
        }

        public ILR1516Context(DbContextOptions<ILR1516Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AlbLearnerPeriod> AlbLearnerPeriods { get; set; }
        public virtual DbSet<AlbLearnerPeriodisedValue> AlbLearnerPeriodisedValues { get; set; }
        public virtual DbSet<AlbLearningDelivery> AlbLearningDeliveries { get; set; }
        public virtual DbSet<AlbLearningDeliveryPeriod> AlbLearningDeliveryPeriods { get; set; }
        public virtual DbSet<AlbLearningDeliveryPeriodisedValue> AlbLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<CollectionDetail1> CollectionDetails1 { get; set; }
        public virtual DbSet<ContactPreference1> ContactPreferences1 { get; set; }
        public virtual DbSet<Dpoutcome1> Dpoutcomes1 { get; set; }
        public virtual DbSet<DvLearner> DvLearners { get; set; }
        public virtual DbSet<DvLearningDelivery> DvLearningDeliveries { get; set; }
        public virtual DbSet<EfaLearner> EfaLearners { get; set; }
        public virtual DbSet<EfaSfaLearnerPeriod> EfaSfaLearnerPeriods { get; set; }
        public virtual DbSet<EfaSfaLearnerPeriodisedValue> EfaSfaLearnerPeriodisedValues { get; set; }
        public virtual DbSet<EmploymentStatusMonitoring> EmploymentStatusMonitorings { get; set; }
        public virtual DbSet<EsfDpoutcome> EsfDpoutcomes { get; set; }
        public virtual DbSet<EsfLearningDelivery> EsfLearningDeliveries { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverable> EsfLearningDeliveryDeliverables { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverablePeriod> EsfLearningDeliveryDeliverablePeriods { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverablePeriodisedValue> EsfLearningDeliveryDeliverablePeriodisedValues { get; set; }
        public virtual DbSet<FileDetail> FileDetails { get; set; }
        public virtual DbSet<Learner> Learners { get; set; }
        public virtual DbSet<Learner1> Learners1 { get; set; }
        public virtual DbSet<LearnerContact> LearnerContacts { get; set; }
        public virtual DbSet<LearnerContact1> LearnerContacts1 { get; set; }
        public virtual DbSet<LearnerDestinationandProgression> LearnerDestinationandProgressions { get; set; }
        public virtual DbSet<LearnerDestinationandProgression1> LearnerDestinationandProgressions1 { get; set; }
        public virtual DbSet<LearnerEmploymentStatus> LearnerEmploymentStatuses { get; set; }
        public virtual DbSet<LearnerEmploymentStatus1> LearnerEmploymentStatuses1 { get; set; }
        public virtual DbSet<LearnerFam> LearnerFams { get; set; }
        public virtual DbSet<LearnerHe> LearnerHes { get; set; }
        public virtual DbSet<LearnerHe1> LearnerHes1 { get; set; }
        public virtual DbSet<LearnerHefinancialSupport> LearnerHefinancialSupports { get; set; }
        public virtual DbSet<LearnerHefinancialSupport1> LearnerHefinancialSupports1 { get; set; }
        public virtual DbSet<LearningDelivery> LearningDeliveries { get; set; }
        public virtual DbSet<LearningDelivery1> LearningDeliveries1 { get; set; }
        public virtual DbSet<LearningDeliveryFam1> LearningDeliveryFams1 { get; set; }
        public virtual DbSet<LearningDeliveryHe> LearningDeliveryHes { get; set; }
        public virtual DbSet<LearningDeliveryHe1> LearningDeliveryHes1 { get; set; }
        public virtual DbSet<LearningDeliveryWorkPlacement1> LearningDeliveryWorkPlacements1 { get; set; }
        public virtual DbSet<LearningProvider> LearningProviders { get; set; }
        public virtual DbSet<LearningProvider1> LearningProviders1 { get; set; }
        public virtual DbSet<LlddandHealthProblem> LlddandHealthProblems { get; set; }
        public virtual DbSet<LlddandHealthProblem1> LlddandHealthProblems1 { get; set; }
        public virtual DbSet<PostAdd> PostAdds { get; set; }
        public virtual DbSet<ProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitorings { get; set; }
        public virtual DbSet<ProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings { get; set; }
        public virtual DbSet<SfaLearningDelivery> SfaLearningDeliveries { get; set; }
        public virtual DbSet<SfaLearningDeliveryPeriod> SfaLearningDeliveryPeriods { get; set; }
        public virtual DbSet<SfaLearningDeliveryPeriodisedValue> SfaLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<Source1> Sources1 { get; set; }
        public virtual DbSet<SourceFile1> SourceFiles1 { get; set; }
        public virtual DbSet<TblLearningDelivery> TblLearningDeliveries { get; set; }
        public virtual DbSet<TblLearningDeliveryPeriod> TblLearningDeliveryPeriods { get; set; }
        public virtual DbSet<TblLearningDeliveryPeriodisedValue> TblLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<TrailblazerApprenticeshipFinancialRecord1> TrailblazerApprenticeshipFinancialRecords1 { get; set; }
        public virtual DbSet<VersionInfo> VersionInfos { get; set; }

        // Unable to generate entity type for table 'Rulebase.ALB_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.TBLVAL_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.TBL_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VALFD_ValidationError'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VALFD_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VALDPFD_ValidationError'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VALDPFD_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VALDP_ValidationError'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VALDP_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VAL_ValidationError'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.VAL_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.SFA_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.EFA_SFA_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.EFA_global'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.TBLVAL_ValidationError'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.TrailblazerApprenticeshipFinancialRecord'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.SourceFile'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.Source'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.LearningDeliveryWorkPlacement'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.LearningDeliveryFAM'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.DPOutcome'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.ContactPreference'. Please see the warning messages.
        // Unable to generate entity type for table 'Valid.CollectionDetails'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ProcessingData'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ValidationError'. Please see the warning messages.
        // Unable to generate entity type for table 'Rulebase.DV_global'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=ILR1516_DataStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<AlbLearnerPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Period })
                    .HasName("PK__ALB_Lear__7066D5F5CEA45227");

                entity.ToTable("ALB_Learner_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AlbseqNum).HasColumnName("ALBSeqNum");
            });

            modelBuilder.Entity<AlbLearnerPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AttributeName })
                    .HasName("PK__ALB_Lear__08C04CF85D7ECE39");

                entity.ToTable("ALB_Learner_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<AlbLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__ALB_Lear__0C29443A74A10DE5");

                entity.ToTable("ALB_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AlbpaymentEndDate)
                    .HasColumnName("ALBPaymentEndDate")
                    .HasColumnType("date");

                entity.Property(e => e.ApplicProgWeightFact)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AreaCostFactAdj).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AreaCostInstalment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.CourseType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FundLine)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LiabilityDate).HasColumnType("date");

                entity.Property(e => e.SpecResUplift).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.WeightedRate).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<AlbLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period })
                    .HasName("PK__ALB_Lear__295823175631D3CA");

                entity.ToTable("ALB_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Albcode).HasColumnName("ALBCode");

                entity.Property(e => e.AlbsupportPayment)
                    .HasColumnName("ALBSupportPayment")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AreaUpliftBalPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AreaUpliftOnProgPayment).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<AlbLearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("PK__ALB_Lear__FED24A87F73D4657");

                entity.ToTable("ALB_LearningDelivery_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<CollectionDetail1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.CollectionDetailsId })
                    .HasName("PK__Collecti__0A22165FE2E331F6");

                entity.ToTable("CollectionDetails", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.CollectionDetailsId).HasColumnName("CollectionDetails_Id");

                entity.Property(e => e.Collection)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FilePreparationDate).HasColumnType("date");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactPreference1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.ContactPreferenceId })
                    .HasName("PK__ContactP__5C3C88BA419327F7");

                entity.ToTable("ContactPreference", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.ContactPreferenceId).HasColumnName("ContactPreference_Id");

                entity.Property(e => e.ContPrefType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");
            });

            modelBuilder.Entity<Dpoutcome1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.DpoutcomeId })
                    .HasName("PK__DPOutcom__BCB936865AA40255");

                entity.ToTable("DPOutcome", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.DpoutcomeId).HasColumnName("DPOutcome_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerDestinationandProgressionId).HasColumnName("LearnerDestinationandProgression_Id");

                entity.Property(e => e.OutCollDate).HasColumnType("date");

                entity.Property(e => e.OutEndDate).HasColumnType("date");

                entity.Property(e => e.OutStartDate).HasColumnType("date");

                entity.Property(e => e.OutType)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DvLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__DV_Learn__2770A72709A75233");

                entity.ToTable("DV_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Learn3rdSector).HasColumnName("Learn_3rdSector");

                entity.Property(e => e.LearnActive).HasColumnName("Learn_Active");

                entity.Property(e => e.LearnActiveJan).HasColumnName("Learn_ActiveJan");

                entity.Property(e => e.LearnActiveNov).HasColumnName("Learn_ActiveNov");

                entity.Property(e => e.LearnActiveOct).HasColumnName("Learn_ActiveOct");

                entity.Property(e => e.LearnAge31Aug).HasColumnName("Learn_Age31Aug");

                entity.Property(e => e.LearnBasicSkill).HasColumnName("Learn_BasicSkill");

                entity.Property(e => e.LearnEmpStatFdl).HasColumnName("Learn_EmpStatFDL");

                entity.Property(e => e.LearnEmpStatPrior).HasColumnName("Learn_EmpStatPrior");

                entity.Property(e => e.LearnFirstFullLevel2).HasColumnName("Learn_FirstFullLevel2");

                entity.Property(e => e.LearnFirstFullLevel2Ach).HasColumnName("Learn_FirstFullLevel2Ach");

                entity.Property(e => e.LearnFirstFullLevel3).HasColumnName("Learn_FirstFullLevel3");

                entity.Property(e => e.LearnFirstFullLevel3Ach).HasColumnName("Learn_FirstFullLevel3Ach");

                entity.Property(e => e.LearnFullLevel2).HasColumnName("Learn_FullLevel2");

                entity.Property(e => e.LearnFullLevel2Ach).HasColumnName("Learn_FullLevel2Ach");

                entity.Property(e => e.LearnFullLevel3).HasColumnName("Learn_FullLevel3");

                entity.Property(e => e.LearnFullLevel3Ach).HasColumnName("Learn_FullLevel3Ach");

                entity.Property(e => e.LearnFundAgency).HasColumnName("Learn_FundAgency");

                entity.Property(e => e.LearnFundPrvYr).HasColumnName("Learn_FundPrvYr");

                entity.Property(e => e.LearnFundingSource).HasColumnName("Learn_FundingSource");

                entity.Property(e => e.LearnIlacMonth1).HasColumnName("Learn_ILAcMonth1");

                entity.Property(e => e.LearnIlacMonth10).HasColumnName("Learn_ILAcMonth10");

                entity.Property(e => e.LearnIlacMonth11).HasColumnName("Learn_ILAcMonth11");

                entity.Property(e => e.LearnIlacMonth12).HasColumnName("Learn_ILAcMonth12");

                entity.Property(e => e.LearnIlacMonth2).HasColumnName("Learn_ILAcMonth2");

                entity.Property(e => e.LearnIlacMonth3).HasColumnName("Learn_ILAcMonth3");

                entity.Property(e => e.LearnIlacMonth4).HasColumnName("Learn_ILAcMonth4");

                entity.Property(e => e.LearnIlacMonth5).HasColumnName("Learn_ILAcMonth5");

                entity.Property(e => e.LearnIlacMonth6).HasColumnName("Learn_ILAcMonth6");

                entity.Property(e => e.LearnIlacMonth7).HasColumnName("Learn_ILAcMonth7");

                entity.Property(e => e.LearnIlacMonth8).HasColumnName("Learn_ILAcMonth8");

                entity.Property(e => e.LearnIlacMonth9).HasColumnName("Learn_ILAcMonth9");

                entity.Property(e => e.LearnIlcurrAcYr).HasColumnName("Learn_ILCurrAcYr");

                entity.Property(e => e.LearnLargeEmployer).HasColumnName("Learn_LargeEmployer");

                entity.Property(e => e.LearnLenEmp).HasColumnName("Learn_LenEmp");

                entity.Property(e => e.LearnLenUnemp).HasColumnName("Learn_LenUnemp");

                entity.Property(e => e.LearnLrnAimRecords).HasColumnName("Learn_LrnAimRecords");

                entity.Property(e => e.LearnModeAttPlanHrs).HasColumnName("Learn_ModeAttPlanHrs");

                entity.Property(e => e.LearnNotionLev).HasColumnName("Learn_NotionLev");

                entity.Property(e => e.LearnNotionLevV2).HasColumnName("Learn_NotionLevV2");

                entity.Property(e => e.LearnOlass).HasColumnName("Learn_OLASS");

                entity.Property(e => e.LearnPrefMethContact).HasColumnName("Learn_PrefMethContact");

                entity.Property(e => e.LearnPrimaryLldd).HasColumnName("Learn_PrimaryLLDD");

                entity.Property(e => e.LearnPriorEducationStatus).HasColumnName("Learn_PriorEducationStatus");

                entity.Property(e => e.LearnRiskOfNeet).HasColumnName("Learn_RiskOfNEET");

                entity.Property(e => e.LearnUnempBenFdl).HasColumnName("Learn_UnempBenFDL");

                entity.Property(e => e.LearnUnempBenPrior).HasColumnName("Learn_UnempBenPrior");

                entity.Property(e => e.LearnUplift1516Efa)
                    .HasColumnName("Learn_Uplift1516EFA")
                    .HasColumnType("decimal(6, 5)");

                entity.Property(e => e.LearnUplift1516Sfa)
                    .HasColumnName("Learn_Uplift1516SFA")
                    .HasColumnType("decimal(6, 5)");
            });

            modelBuilder.Entity<DvLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__DV_Learn__0C29443A450AA788");

                entity.ToTable("DV_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelAcMonthYtd)
                    .HasColumnName("LearnDel_AcMonthYTD")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelAccToApp).HasColumnName("LearnDel_AccToApp");

                entity.Property(e => e.LearnDelAccToAppEmpDate)
                    .HasColumnName("LearnDel_AccToAppEmpDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelAccToAppEmpStat).HasColumnName("LearnDel_AccToAppEmpStat");

                entity.Property(e => e.LearnDelAchFullLevel2Pct)
                    .HasColumnName("LearnDel_AchFullLevel2Pct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelAchFullLevel3Pct)
                    .HasColumnName("LearnDel_AchFullLevel3Pct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelAchieved).HasColumnName("LearnDel_Achieved");

                entity.Property(e => e.LearnDelAchievedIy).HasColumnName("LearnDel_AchievedIY");

                entity.Property(e => e.LearnDelActDaysIlafterCurrAcYr).HasColumnName("LearnDel_ActDaysILAfterCurrAcYr");

                entity.Property(e => e.LearnDelActDaysIlcurrAcYr).HasColumnName("LearnDel_ActDaysILCurrAcYr");

                entity.Property(e => e.LearnDelActEndDateOnAfterJan1).HasColumnName("LearnDel_ActEndDateOnAfterJan1");

                entity.Property(e => e.LearnDelActEndDateOnAfterNov1).HasColumnName("LearnDel_ActEndDateOnAfterNov1");

                entity.Property(e => e.LearnDelActEndDateOnAfterOct1).HasColumnName("LearnDel_ActEndDateOnAfterOct1");

                entity.Property(e => e.LearnDelActTotalDaysIl).HasColumnName("LearnDel_ActTotalDaysIL");

                entity.Property(e => e.LearnDelActiveIy).HasColumnName("LearnDel_ActiveIY");

                entity.Property(e => e.LearnDelActiveJan).HasColumnName("LearnDel_ActiveJan");

                entity.Property(e => e.LearnDelActiveNov).HasColumnName("LearnDel_ActiveNov");

                entity.Property(e => e.LearnDelActiveOct).HasColumnName("LearnDel_ActiveOct");

                entity.Property(e => e.LearnDelAdvLoan).HasColumnName("LearnDel_AdvLoan");

                entity.Property(e => e.LearnDelAgeAimOrigStart).HasColumnName("LearnDel_AgeAimOrigStart");

                entity.Property(e => e.LearnDelAgeAtOrigStart).HasColumnName("LearnDel_AgeAtOrigStart");

                entity.Property(e => e.LearnDelAgeAtStart).HasColumnName("LearnDel_AgeAtStart");

                entity.Property(e => e.LearnDelApp).HasColumnName("LearnDel_App");

                entity.Property(e => e.LearnDelApp1618Fund).HasColumnName("LearnDel_App1618Fund");

                entity.Property(e => e.LearnDelApp1925Fund).HasColumnName("LearnDel_App1925Fund");

                entity.Property(e => e.LearnDelAppAimType).HasColumnName("LearnDel_AppAimType");

                entity.Property(e => e.LearnDelAppKnowl).HasColumnName("LearnDel_AppKnowl");

                entity.Property(e => e.LearnDelAppMainAim).HasColumnName("LearnDel_AppMainAim");

                entity.Property(e => e.LearnDelAppNonFund).HasColumnName("LearnDel_AppNonFund");

                entity.Property(e => e.LearnDelBasicSkills).HasColumnName("LearnDel_BasicSkills");

                entity.Property(e => e.LearnDelBasicSkillsParticipation).HasColumnName("LearnDel_BasicSkillsParticipation");

                entity.Property(e => e.LearnDelBasicSkillsType).HasColumnName("LearnDel_BasicSkillsType");

                entity.Property(e => e.LearnDelCarryIn).HasColumnName("LearnDel_CarryIn");

                entity.Property(e => e.LearnDelClassRm).HasColumnName("LearnDel_ClassRm");

                entity.Property(e => e.LearnDelCompAimApp).HasColumnName("LearnDel_CompAimApp");

                entity.Property(e => e.LearnDelCompAimProg).HasColumnName("LearnDel_CompAimProg");

                entity.Property(e => e.LearnDelCompleteFullLevel2Pct)
                    .HasColumnName("LearnDel_CompleteFullLevel2Pct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelCompleteFullLevel3Pct)
                    .HasColumnName("LearnDel_CompleteFullLevel3Pct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelCompleted).HasColumnName("LearnDel_Completed");

                entity.Property(e => e.LearnDelCompletedIy).HasColumnName("LearnDel_CompletedIY");

                entity.Property(e => e.LearnDelEfacoreAim).HasColumnName("LearnDel_EFACoreAim");

                entity.Property(e => e.LearnDelEmp6MonthAimStart).HasColumnName("LearnDel_Emp6MonthAimStart");

                entity.Property(e => e.LearnDelEmp6MonthProgStart).HasColumnName("LearnDel_Emp6MonthProgStart");

                entity.Property(e => e.LearnDelEmpDateBeforeFdl)
                    .HasColumnName("LearnDel_EmpDateBeforeFDL")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelEmpDatePriorFdl)
                    .HasColumnName("LearnDel_EmpDatePriorFDL")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelEmpId).HasColumnName("LearnDel_EmpID");

                entity.Property(e => e.LearnDelEmpStatFdl).HasColumnName("LearnDel_EmpStatFDL");

                entity.Property(e => e.LearnDelEmpStatPrior).HasColumnName("LearnDel_EmpStatPrior");

                entity.Property(e => e.LearnDelEmpStatPriorFdl).HasColumnName("LearnDel_EmpStatPriorFDL");

                entity.Property(e => e.LearnDelEmployed).HasColumnName("LearnDel_Employed");

                entity.Property(e => e.LearnDelEnhanAppFund).HasColumnName("LearnDel_EnhanAppFund");

                entity.Property(e => e.LearnDelFullLevel2AchPct)
                    .HasColumnName("LearnDel_FullLevel2AchPct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelFullLevel2ContPct)
                    .HasColumnName("LearnDel_FullLevel2ContPct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelFullLevel3AchPct)
                    .HasColumnName("LearnDel_FullLevel3AchPct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelFullLevel3ContPct)
                    .HasColumnName("LearnDel_FullLevel3ContPct")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LearnDelFuncSkills).HasColumnName("LearnDel_FuncSkills");

                entity.Property(e => e.LearnDelFundAgency).HasColumnName("LearnDel_FundAgency");

                entity.Property(e => e.LearnDelFundPrvYr).HasColumnName("LearnDel_FundPrvYr");

                entity.Property(e => e.LearnDelFundStart).HasColumnName("LearnDel_FundStart");

                entity.Property(e => e.LearnDelFundingLineType)
                    .HasColumnName("LearnDel_FundingLineType")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelFundingSource).HasColumnName("LearnDel_FundingSource");

                entity.Property(e => e.LearnDelGce).HasColumnName("LearnDel_GCE");

                entity.Property(e => e.LearnDelGcse).HasColumnName("LearnDel_GCSE");

                entity.Property(e => e.LearnDelIlacMonth1).HasColumnName("LearnDel_ILAcMonth1");

                entity.Property(e => e.LearnDelIlacMonth10).HasColumnName("LearnDel_ILAcMonth10");

                entity.Property(e => e.LearnDelIlacMonth11).HasColumnName("LearnDel_ILAcMonth11");

                entity.Property(e => e.LearnDelIlacMonth12).HasColumnName("LearnDel_ILAcMonth12");

                entity.Property(e => e.LearnDelIlacMonth2).HasColumnName("LearnDel_ILAcMonth2");

                entity.Property(e => e.LearnDelIlacMonth3).HasColumnName("LearnDel_ILAcMonth3");

                entity.Property(e => e.LearnDelIlacMonth4).HasColumnName("LearnDel_ILAcMonth4");

                entity.Property(e => e.LearnDelIlacMonth5).HasColumnName("LearnDel_ILAcMonth5");

                entity.Property(e => e.LearnDelIlacMonth6).HasColumnName("LearnDel_ILAcMonth6");

                entity.Property(e => e.LearnDelIlacMonth7).HasColumnName("LearnDel_ILAcMonth7");

                entity.Property(e => e.LearnDelIlacMonth8).HasColumnName("LearnDel_ILAcMonth8");

                entity.Property(e => e.LearnDelIlacMonth9).HasColumnName("LearnDel_ILAcMonth9");

                entity.Property(e => e.LearnDelIlcurrAcYr).HasColumnName("LearnDel_ILCurrAcYr");

                entity.Property(e => e.LearnDelIyactEndDate)
                    .HasColumnName("LearnDel_IYActEndDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelIyplanEndDate)
                    .HasColumnName("LearnDel_IYPlanEndDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelIystartDate)
                    .HasColumnName("LearnDel_IYStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelKeySkills).HasColumnName("LearnDel_KeySkills");

                entity.Property(e => e.LearnDelLargeEmpDiscountId).HasColumnName("LearnDel_LargeEmpDiscountId");

                entity.Property(e => e.LearnDelLargeEmployer).HasColumnName("LearnDel_LargeEmployer");

                entity.Property(e => e.LearnDelLastEmpDate)
                    .HasColumnName("LearnDel_LastEmpDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelLeaveMonth).HasColumnName("LearnDel_LeaveMonth");

                entity.Property(e => e.LearnDelLenEmp).HasColumnName("LearnDel_LenEmp");

                entity.Property(e => e.LearnDelLenUnemp).HasColumnName("LearnDel_LenUnemp");

                entity.Property(e => e.LearnDelLoanBursFund).HasColumnName("LearnDel_LoanBursFund");

                entity.Property(e => e.LearnDelNeetAtStart).HasColumnName("LearnDel_NeetAtStart");

                entity.Property(e => e.LearnDelNotionLevel).HasColumnName("LearnDel_NotionLevel");

                entity.Property(e => e.LearnDelNotionLevelV2).HasColumnName("LearnDel_NotionLevelV2");

                entity.Property(e => e.LearnDelNumHedatasets).HasColumnName("LearnDel_NumHEDatasets");

                entity.Property(e => e.LearnDelOccupAim).HasColumnName("LearnDel_OccupAim");

                entity.Property(e => e.LearnDelOlass).HasColumnName("LearnDel_OLASS");

                entity.Property(e => e.LearnDelOlasscom).HasColumnName("LearnDel_OLASSCom");

                entity.Property(e => e.LearnDelOlasscus).HasColumnName("LearnDel_OLASSCus");

                entity.Property(e => e.LearnDelOrigStartDate)
                    .HasColumnName("LearnDel_OrigStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelPlanDaysIlafterCurrAcYr).HasColumnName("LearnDel_PlanDaysILAfterCurrAcYr");

                entity.Property(e => e.LearnDelPlanDaysIlcurrAcYr).HasColumnName("LearnDel_PlanDaysILCurrAcYr");

                entity.Property(e => e.LearnDelPlanEndBeforeAug1).HasColumnName("LearnDel_PlanEndBeforeAug1");

                entity.Property(e => e.LearnDelPlanEndOnAfterJan1).HasColumnName("LearnDel_PlanEndOnAfterJan1");

                entity.Property(e => e.LearnDelPlanEndOnAfterNov1).HasColumnName("LearnDel_PlanEndOnAfterNov1");

                entity.Property(e => e.LearnDelPlanEndOnAfterOct1).HasColumnName("LearnDel_PlanEndOnAfterOct1");

                entity.Property(e => e.LearnDelPlanTotalDaysIl).HasColumnName("LearnDel_PlanTotalDaysIL");

                entity.Property(e => e.LearnDelPlannedTotalDaysIl).HasColumnName("LearnDel_PlannedTotalDaysIL");

                entity.Property(e => e.LearnDelPriorEducationStatus).HasColumnName("LearnDel_PriorEducationStatus");

                entity.Property(e => e.LearnDelProg).HasColumnName("LearnDel_Prog");

                entity.Property(e => e.LearnDelProgAimAch).HasColumnName("LearnDel_ProgAimAch");

                entity.Property(e => e.LearnDelProgAimApp).HasColumnName("LearnDel_ProgAimApp");

                entity.Property(e => e.LearnDelProgCompleted).HasColumnName("LearnDel_ProgCompleted");

                entity.Property(e => e.LearnDelProgCompletedIy).HasColumnName("LearnDel_ProgCompletedIY");

                entity.Property(e => e.LearnDelProgStartDate)
                    .HasColumnName("LearnDel_ProgStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelQcf).HasColumnName("LearnDel_QCF");

                entity.Property(e => e.LearnDelQcfcert).HasColumnName("LearnDel_QCFCert");

                entity.Property(e => e.LearnDelQcfdipl).HasColumnName("LearnDel_QCFDipl");

                entity.Property(e => e.LearnDelQcftype).HasColumnName("LearnDel_QCFType");

                entity.Property(e => e.LearnDelRegAim).HasColumnName("LearnDel_RegAim");

                entity.Property(e => e.LearnDelRiskOfNeet).HasColumnName("LearnDel_RiskOfNEET");

                entity.Property(e => e.LearnDelSecSubAreaTier1)
                    .HasColumnName("LearnDel_SecSubAreaTier1")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelSecSubAreaTier2)
                    .HasColumnName("LearnDel_SecSubAreaTier2")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelSfaapproved).HasColumnName("LearnDel_SFAApproved");

                entity.Property(e => e.LearnDelSourceFundEfa).HasColumnName("LearnDel_SourceFundEFA");

                entity.Property(e => e.LearnDelSourceFundSfa).HasColumnName("LearnDel_SourceFundSFA");

                entity.Property(e => e.LearnDelStartBeforeApr1).HasColumnName("LearnDel_StartBeforeApr1");

                entity.Property(e => e.LearnDelStartBeforeAug1).HasColumnName("LearnDel_StartBeforeAug1");

                entity.Property(e => e.LearnDelStartBeforeDec1).HasColumnName("LearnDel_StartBeforeDec1");

                entity.Property(e => e.LearnDelStartBeforeFeb1).HasColumnName("LearnDel_StartBeforeFeb1");

                entity.Property(e => e.LearnDelStartBeforeJan1).HasColumnName("LearnDel_StartBeforeJan1");

                entity.Property(e => e.LearnDelStartBeforeJun1).HasColumnName("LearnDel_StartBeforeJun1");

                entity.Property(e => e.LearnDelStartBeforeMar1).HasColumnName("LearnDel_StartBeforeMar1");

                entity.Property(e => e.LearnDelStartBeforeMay1).HasColumnName("LearnDel_StartBeforeMay1");

                entity.Property(e => e.LearnDelStartBeforeNov1).HasColumnName("LearnDel_StartBeforeNov1");

                entity.Property(e => e.LearnDelStartBeforeOct1).HasColumnName("LearnDel_StartBeforeOct1");

                entity.Property(e => e.LearnDelStartBeforeSep1).HasColumnName("LearnDel_StartBeforeSep1");

                entity.Property(e => e.LearnDelStartIy).HasColumnName("LearnDel_StartIY");

                entity.Property(e => e.LearnDelStartJan1).HasColumnName("LearnDel_StartJan1");

                entity.Property(e => e.LearnDelStartMonth).HasColumnName("LearnDel_StartMonth");

                entity.Property(e => e.LearnDelStartNov1).HasColumnName("LearnDel_StartNov1");

                entity.Property(e => e.LearnDelStartOct1).HasColumnName("LearnDel_StartOct1");

                entity.Property(e => e.LearnDelSuccRateStat).HasColumnName("LearnDel_SuccRateStat");

                entity.Property(e => e.LearnDelTrainAimType).HasColumnName("LearnDel_TrainAimType");

                entity.Property(e => e.LearnDelTransferDiffProvider).HasColumnName("LearnDel_TransferDiffProvider");

                entity.Property(e => e.LearnDelTransferDiffProviderGovStrat).HasColumnName("LearnDel_TransferDiffProviderGovStrat");

                entity.Property(e => e.LearnDelTransferProvider).HasColumnName("LearnDel_TransferProvider");

                entity.Property(e => e.LearnDelUfIprov).HasColumnName("LearnDel_UfIProv");

                entity.Property(e => e.LearnDelUnempBenFdl).HasColumnName("LearnDel_UnempBenFDL");

                entity.Property(e => e.LearnDelUnempBenPrior).HasColumnName("LearnDel_UnempBenPrior");

                entity.Property(e => e.LearnDelWithdrawn).HasColumnName("LearnDel_Withdrawn");

                entity.Property(e => e.LearnDelWorkplaceLocPostcode)
                    .HasColumnName("LearnDel_WorkplaceLocPostcode")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelWplprov).HasColumnName("LearnDel_WPLProv");

                entity.Property(e => e.ProgAccToApp).HasColumnName("Prog_AccToApp");

                entity.Property(e => e.ProgAchieved).HasColumnName("Prog_Achieved");

                entity.Property(e => e.ProgAchievedIy).HasColumnName("Prog_AchievedIY");

                entity.Property(e => e.ProgActEndDate)
                    .HasColumnName("Prog_ActEndDate")
                    .HasColumnType("date");

                entity.Property(e => e.ProgActiveIy).HasColumnName("Prog_ActiveIY");

                entity.Property(e => e.ProgAgeAtStart).HasColumnName("Prog_AgeAtStart");

                entity.Property(e => e.ProgEarliestAim).HasColumnName("Prog_EarliestAim");

                entity.Property(e => e.ProgEmployed).HasColumnName("Prog_Employed");

                entity.Property(e => e.ProgFundPrvYr).HasColumnName("Prog_FundPrvYr");

                entity.Property(e => e.ProgIlcurrAcYear).HasColumnName("Prog_ILCurrAcYear");

                entity.Property(e => e.ProgLatestAim).HasColumnName("Prog_LatestAim");

                entity.Property(e => e.ProgSourceFundEfa).HasColumnName("Prog_SourceFundEFA");

                entity.Property(e => e.ProgSourceFundSfa).HasColumnName("Prog_SourceFundSFA");
            });

            modelBuilder.Entity<EfaLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("pk_EFA_Learner");

                entity.ToTable("EFA_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ActualDaysIlcurrYear).HasColumnName("ActualDaysILCurrYear");

                entity.Property(e => e.AreaCostFact1618Hist).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Block1DisadvUpliftNew).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Block2DisadvElementsNew).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ConditionOfFundingEnglish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConditionOfFundingMaths)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullTimeEquiv).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerActEndDate).HasColumnType("date");

                entity.Property(e => e.LearnerPlanEndDate).HasColumnType("date");

                entity.Property(e => e.LearnerStartDate).HasColumnType("date");

                entity.Property(e => e.NatRate).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.OnProgPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PlannedDaysIlcurrYear).HasColumnName("PlannedDaysILCurrYear");

                entity.Property(e => e.ProgWeightHist).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ProgWeightNew).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PrvDisadvPropnHist).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PrvRetentFactHist).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.RateBand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RetentNew).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EfaSfaLearnerPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Period })
                    .HasName("PK__EFA_SFA___7066D5F5BC3DDB29");

                entity.ToTable("EFA_SFA_Learner_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LnrOnProgPay).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EfaSfaLearnerPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AttributeName })
                    .HasName("PK__EFA_SFA___08C04CF8BBC80922");

                entity.ToTable("EFA_SFA_Learner_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<EmploymentStatusMonitoring>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.EmploymentStatusMonitoringId })
                    .HasName("PK__Employme__0B37F4C9C20C750A");

                entity.ToTable("EmploymentStatusMonitoring", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.EmploymentStatusMonitoringId).HasColumnName("EmploymentStatusMonitoring_Id");

                entity.Property(e => e.DateEmpStatApp).HasColumnType("date");

                entity.Property(e => e.Esmcode).HasColumnName("ESMCode");

                entity.Property(e => e.Esmtype)
                    .HasColumnName("ESMType")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerEmploymentStatusId).HasColumnName("LearnerEmploymentStatus_Id");
            });

            modelBuilder.Entity<EsfDpoutcome>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.OutCode, e.OutType, e.OutStartDate })
                    .HasName("PK__ESF_DPOu__1D621D29FEA06B11");

                entity.ToTable("ESF_DPOutcome", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OutType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.OutStartDate).HasColumnType("date");

                entity.Property(e => e.OutcomeDateForProgression).HasColumnType("date");

                entity.Property(e => e.PotentialEsfprogressionType).HasColumnName("PotentialESFProgressionType");

                entity.Property(e => e.ProgressionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EsfLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__ESF_Lear__0C29443AEFB98E31");

                entity.ToTable("ESF_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AdjustedAreaCostFactor).HasColumnType("decimal(9, 5)");

                entity.Property(e => e.AdjustedPremiumFactor).HasColumnType("decimal(9, 5)");

                entity.Property(e => e.AdjustedStartDate).HasColumnType("date");

                entity.Property(e => e.AimClassification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AimValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ApplicWeightFundRate).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EligibleProgressionOutcomeType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.EligibleProgressionOutomeStartDate).HasColumnType("date");

                entity.Property(e => e.LarsweightedRate)
                    .HasColumnName("LARSWeightedRate")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LatestPossibleStartDate).HasColumnType("date");

                entity.Property(e => e.LdesfengagementStartDate)
                    .HasColumnName("LDESFEngagementStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.ProgressionEndDate).HasColumnType("date");

                entity.Property(e => e.WeightedRateFromEsol)
                    .HasColumnName("WeightedRateFromESOL")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EsfLearningDeliveryDeliverable>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode })
                    .HasName("PK__ESF_Lear__C21F732A6A6A9814");

                entity.ToTable("ESF_LearningDeliveryDeliverable", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableUnitCost).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EsfLearningDeliveryDeliverablePeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode, e.Period })
                    .HasName("PK__ESF_Lear__104865583087E208");

                entity.ToTable("ESF_LearningDeliveryDeliverable_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AchievementEarnings).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AdditionalProgCostEarnings).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ProgressionEarnings).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.StartEarnings).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EsfLearningDeliveryDeliverablePeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode, e.AttributeName })
                    .HasName("PK__ESF_Lear__1D30C3C18CC7F50D");

                entity.ToTable("ESF_LearningDeliveryDeliverable_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<FileDetail>(entity =>
            {
                entity.HasIndex(e => new { e.Ukprn, e.Filename, e.Success })
                    .HasName("PK_dbo.FileDetails")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Filename).HasMaxLength(50);

                entity.Property(e => e.SubmittedTime).HasColumnType("datetime");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<Learner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__Learner__2770A7275AC5B218");

                entity.ToTable("Learner", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Alscost).HasColumnName("ALSCost");

                entity.Property(e => e.CurrentPostcode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EngGrade)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GivenNames)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HomePostcode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LlddhealthProb).HasColumnName("LLDDHealthProb");

                entity.Property(e => e.LrnFamDla).HasColumnName("LrnFAM_DLA");

                entity.Property(e => e.LrnFamEcf).HasColumnName("LrnFAM_ECF");

                entity.Property(e => e.LrnFamEdf1).HasColumnName("LrnFAM_EDF1");

                entity.Property(e => e.LrnFamEdf2).HasColumnName("LrnFAM_EDF2");

                entity.Property(e => e.LrnFamEhc).HasColumnName("LrnFAM_EHC");

                entity.Property(e => e.LrnFamFme).HasColumnName("LrnFAM_FME");

                entity.Property(e => e.LrnFamHns).HasColumnName("LrnFAM_HNS");

                entity.Property(e => e.LrnFamLda).HasColumnName("LrnFAM_LDA");

                entity.Property(e => e.LrnFamLsr1).HasColumnName("LrnFAM_LSR1");

                entity.Property(e => e.LrnFamLsr2).HasColumnName("LrnFAM_LSR2");

                entity.Property(e => e.LrnFamLsr3).HasColumnName("LrnFAM_LSR3");

                entity.Property(e => e.LrnFamLsr4).HasColumnName("LrnFAM_LSR4");

                entity.Property(e => e.LrnFamMcf).HasColumnName("LrnFAM_MCF");

                entity.Property(e => e.LrnFamNlm1).HasColumnName("LrnFAM_NLM1");

                entity.Property(e => e.LrnFamNlm2).HasColumnName("LrnFAM_NLM2");

                entity.Property(e => e.LrnFamPpe1).HasColumnName("LrnFAM_PPE1");

                entity.Property(e => e.LrnFamPpe2).HasColumnName("LrnFAM_PPE2");

                entity.Property(e => e.LrnFamSen).HasColumnName("LrnFAM_SEN");

                entity.Property(e => e.MathGrade)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Ninumber)
                    .HasColumnName("NINumber")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.PlanEephours).HasColumnName("PlanEEPHours");

                entity.Property(e => e.PrevLearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PrevUkprn).HasColumnName("PrevUKPRN");

                entity.Property(e => e.ProvSpecMonA)
                    .HasColumnName("ProvSpecMon_A")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecMonB)
                    .HasColumnName("ProvSpecMon_B")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<Learner1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerId })
                    .HasName("PK__Learner__B32C4C0CB1A6D2A8");

                entity.ToTable("Learner", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");

                entity.Property(e => e.Alscost).HasColumnName("ALSCost");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EngGrade)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GivenNames)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LlddhealthProb).HasColumnName("LLDDHealthProb");

                entity.Property(e => e.MathGrade)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Ninumber)
                    .HasColumnName("NINumber")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PlanEephours).HasColumnName("PlanEEPHours");

                entity.Property(e => e.PrevLearnRefNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PrevUkprn).HasColumnName("PrevUKPRN");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<LearnerContact>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__LearnerC__2770A72771F2E081");

                entity.ToTable("LearnerContact", "Valid");

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

                entity.Property(e => e.CurrentPostcode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HomePostcode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TelNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LearnerContact1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerContactId })
                    .HasName("PK__LearnerC__762F480C054FFDCA");

                entity.ToTable("LearnerContact", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerContactId).HasColumnName("LearnerContact_Id");

                entity.Property(e => e.Email)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TelNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LearnerDestinationandProgression>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__LearnerD__2770A727FB8BF2AE");

                entity.ToTable("LearnerDestinationandProgression", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<LearnerDestinationandProgression1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerDestinationandProgressionId })
                    .HasName("PK__LearnerD__89AF0D0633E0ECAF");

                entity.ToTable("LearnerDestinationandProgression", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerDestinationandProgressionId).HasColumnName("LearnerDestinationandProgression_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<LearnerEmploymentStatus>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.DateEmpStatApp })
                    .HasName("PK__LearnerE__7200C4BE01ECC0E4");

                entity.ToTable("LearnerEmploymentStatus", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DateEmpStatApp).HasColumnType("date");

                entity.Property(e => e.EmpStatMonBsi).HasColumnName("EmpStatMon_BSI");

                entity.Property(e => e.EmpStatMonEii).HasColumnName("EmpStatMon_EII");

                entity.Property(e => e.EmpStatMonLoe).HasColumnName("EmpStatMon_LOE");

                entity.Property(e => e.EmpStatMonLou).HasColumnName("EmpStatMon_LOU");

                entity.Property(e => e.EmpStatMonPei).HasColumnName("EmpStatMon_PEI");

                entity.Property(e => e.EmpStatMonRon).HasColumnName("EmpStatMon_RON");

                entity.Property(e => e.EmpStatMonSei).HasColumnName("EmpStatMon_SEI");

                entity.Property(e => e.EmpStatMonSem).HasColumnName("EmpStatMon_SEM");
            });

            modelBuilder.Entity<LearnerEmploymentStatus1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerEmploymentStatusId })
                    .HasName("PK__LearnerE__C46094A18467F5C5");

                entity.ToTable("LearnerEmploymentStatus", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerEmploymentStatusId).HasColumnName("LearnerEmploymentStatus_Id");

                entity.Property(e => e.DateEmpStatApp).HasColumnType("date");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");
            });

            modelBuilder.Entity<LearnerFam>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerFamId })
                    .HasName("PK__LearnerF__A4FBFECA97F2B670");

                entity.ToTable("LearnerFAM", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerFamId).HasColumnName("LearnerFAM_Id");

                entity.Property(e => e.LearnFamcode).HasColumnName("LearnFAMCode");

                entity.Property(e => e.LearnFamtype)
                    .HasColumnName("LearnFAMType")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");
            });

            modelBuilder.Entity<LearnerHe>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__LearnerH__2770A7277B9D02A8");

                entity.ToTable("LearnerHE", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ttaccom).HasColumnName("TTACCOM");

                entity.Property(e => e.Ucasperid).HasColumnName("UCASPERID");
            });

            modelBuilder.Entity<LearnerHe1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerHeId })
                    .HasName("PK__LearnerH__C753F49BD9E0BDB5");

                entity.ToTable("LearnerHE", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerHeId).HasColumnName("LearnerHE_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");

                entity.Property(e => e.Ttaccom).HasColumnName("TTACCOM");

                entity.Property(e => e.Ucasperid).HasColumnName("UCASPERID");
            });

            modelBuilder.Entity<LearnerHefinancialSupport>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Fintype })
                    .HasName("PK__LearnerH__09F54B7219D4E294");

                entity.ToTable("LearnerHEFinancialSupport", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Fintype).HasColumnName("FINTYPE");

                entity.Property(e => e.Finamount).HasColumnName("FINAMOUNT");
            });

            modelBuilder.Entity<LearnerHefinancialSupport1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnerHefinancialSupportId })
                    .HasName("PK__LearnerH__10F897139C27510A");

                entity.ToTable("LearnerHEFinancialSupport", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnerHefinancialSupportId).HasColumnName("LearnerHEFinancialSupport_Id");

                entity.Property(e => e.Finamount).HasColumnName("FINAMOUNT");

                entity.Property(e => e.Fintype).HasColumnName("FINTYPE");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerHeId).HasColumnName("LearnerHE_Id");
            });

            modelBuilder.Entity<LearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__Learning__0C29443A022792D6");

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

                entity.Property(e => e.LearnActEndDate).HasColumnType("date");

                entity.Property(e => e.LearnAimRef)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnPlanEndDate).HasColumnType("date");

                entity.Property(e => e.LearnStartDate).HasColumnType("date");

                entity.Property(e => e.LrnDelFamAdl)
                    .HasColumnName("LrnDelFAM_ADL")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamAsl)
                    .HasColumnName("LrnDelFAM_ASL")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamEef)
                    .HasColumnName("LrnDelFAM_EEF")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamFfi)
                    .HasColumnName("LrnDelFAM_FFI")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamFln)
                    .HasColumnName("LrnDelFAM_FLN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamHem1)
                    .HasColumnName("LrnDelFAM_HEM1")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamHem2)
                    .HasColumnName("LrnDelFAM_HEM2")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamHem3)
                    .HasColumnName("LrnDelFAM_HEM3")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamHhs1)
                    .HasColumnName("LrnDelFAM_HHS1")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamHhs2)
                    .HasColumnName("LrnDelFAM_HHS2")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamLdm1)
                    .HasColumnName("LrnDelFAM_LDM1")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamLdm2)
                    .HasColumnName("LrnDelFAM_LDM2")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamLdm3)
                    .HasColumnName("LrnDelFAM_LDM3")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamLdm4)
                    .HasColumnName("LrnDelFAM_LDM4")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamNsa)
                    .HasColumnName("LrnDelFAM_NSA")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamPod)
                    .HasColumnName("LrnDelFAM_POD")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamRes)
                    .HasColumnName("LrnDelFAM_RES")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamSof)
                    .HasColumnName("LrnDelFAM_SOF")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamSpp)
                    .HasColumnName("LrnDelFAM_SPP")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamTbs)
                    .HasColumnName("LrnDelFAM_TBS")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamWpl)
                    .HasColumnName("LrnDelFAM_WPL")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LrnDelFamWpp)
                    .HasColumnName("LrnDelFAM_WPP")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OrigLearnStartDate).HasColumnType("date");

                entity.Property(e => e.OutGrade)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerUkprn).HasColumnName("PartnerUKPRN");

                entity.Property(e => e.ProvSpecMonA)
                    .HasColumnName("ProvSpecMon_A")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecMonB)
                    .HasColumnName("ProvSpecMon_B")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecMonC)
                    .HasColumnName("ProvSpecMon_C")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecMonD)
                    .HasColumnName("ProvSpecMon_D")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SwsupAimId)
                    .HasColumnName("SWSupAimId")
                    .HasMaxLength(36)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LearningDelivery1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearningDeliveryId })
                    .HasName("PK__Learning__B20E26911AD1CAF1");

                entity.ToTable("LearningDelivery", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearningDeliveryId).HasColumnName("LearningDelivery_Id");

                entity.Property(e => e.AchDate).HasColumnType("date");

                entity.Property(e => e.ConRefNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DelLocPostCode)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnActEndDate).HasColumnType("date");

                entity.Property(e => e.LearnAimRef)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnPlanEndDate).HasColumnType("date");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnStartDate).HasColumnType("date");

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");

                entity.Property(e => e.OrigLearnStartDate).HasColumnType("date");

                entity.Property(e => e.OutGrade)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerUkprn).HasColumnName("PartnerUKPRN");

                entity.Property(e => e.SwsupAimId)
                    .HasColumnName("SWSupAimId")
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LearningDeliveryFam1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearningDeliveryFamId })
                    .HasName("PK__Learning__B60D2E5266A7B3CB");

                entity.ToTable("LearningDeliveryFAM", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearningDeliveryFamId).HasColumnName("LearningDeliveryFAM_Id");

                entity.Property(e => e.LearnDelFamcode)
                    .HasColumnName("LearnDelFAMCode")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelFamdateFrom)
                    .HasColumnName("LearnDelFAMDateFrom")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelFamdateTo)
                    .HasColumnName("LearnDelFAMDateTo")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelFamtype)
                    .HasColumnName("LearnDelFAMType")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearningDeliveryId).HasColumnName("LearningDelivery_Id");
            });

            modelBuilder.Entity<LearningDeliveryHe>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__Learning__0C29443A1214B784");

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
            });

            modelBuilder.Entity<LearningDeliveryHe1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearningDeliveryHeId })
                    .HasName("PK__Learning__CD5B09B580BCF154");

                entity.ToTable("LearningDeliveryHE", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearningDeliveryHeId).HasColumnName("LearningDeliveryHE_Id");

                entity.Property(e => e.Domicile)
                    .HasColumnName("DOMICILE")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Elq).HasColumnName("ELQ");

                entity.Property(e => e.Fundcomp).HasColumnName("FUNDCOMP");

                entity.Property(e => e.Fundlev).HasColumnName("FUNDLEV");

                entity.Property(e => e.Grossfee).HasColumnName("GROSSFEE");

                entity.Property(e => e.HepostCode)
                    .HasColumnName("HEPostCode")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearningDeliveryId).HasColumnName("LearningDelivery_Id");

                entity.Property(e => e.Modestud).HasColumnName("MODESTUD");

                entity.Property(e => e.Mstufee).HasColumnName("MSTUFEE");

                entity.Property(e => e.Netfee).HasColumnName("NETFEE");

                entity.Property(e => e.Numhus)
                    .HasColumnName("NUMHUS")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Pcfldcs).HasColumnName("PCFLDCS");

                entity.Property(e => e.Pcolab).HasColumnName("PCOLAB");

                entity.Property(e => e.Pcsldcs).HasColumnName("PCSLDCS");

                entity.Property(e => e.Pctldcs).HasColumnName("PCTLDCS");

                entity.Property(e => e.Qualent3)
                    .HasColumnName("QUALENT3")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Sec).HasColumnName("SEC");

                entity.Property(e => e.Soc2000).HasColumnName("SOC2000");

                entity.Property(e => e.Specfee).HasColumnName("SPECFEE");

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Stuload).HasColumnName("STULOAD");

                entity.Property(e => e.Typeyr).HasColumnName("TYPEYR");

                entity.Property(e => e.Ucasappid)
                    .HasColumnName("UCASAPPID")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Yearstu).HasColumnName("YEARSTU");
            });

            modelBuilder.Entity<LearningDeliveryWorkPlacement1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearningDeliveryWorkPlacementId })
                    .HasName("PK__Learning__4809923638FBB169");

                entity.ToTable("LearningDeliveryWorkPlacement", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearningDeliveryWorkPlacementId).HasColumnName("LearningDeliveryWorkPlacement_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearningDeliveryId).HasColumnName("LearningDelivery_Id");

                entity.Property(e => e.WorkPlaceEndDate).HasColumnType("date");

                entity.Property(e => e.WorkPlaceStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<LearningProvider>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__Learning__50F26B7175BD6CC3");

                entity.ToTable("LearningProvider", "Valid");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<LearningProvider1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearningProviderId })
                    .HasName("PK__Learning__4E0C86EE111A661A");

                entity.ToTable("LearningProvider", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearningProviderId).HasColumnName("LearningProvider_Id");
            });

            modelBuilder.Entity<LlddandHealthProblem>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Llddcat })
                    .HasName("PK__LLDDandH__7DC49456EEF1C2DA");

                entity.ToTable("LLDDandHealthProblem", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Llddcat).HasColumnName("LLDDCat");

                entity.Property(e => e.PrimaryLldd).HasColumnName("PrimaryLLDD");
            });

            modelBuilder.Entity<LlddandHealthProblem1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LlddandHealthProblemId })
                    .HasName("PK__LLDDandH__3D2821C18796C10D");

                entity.ToTable("LLDDandHealthProblem", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LlddandHealthProblemId).HasColumnName("LLDDandHealthProblem_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");

                entity.Property(e => e.Llddcat).HasColumnName("LLDDCat");

                entity.Property(e => e.PrimaryLldd).HasColumnName("PrimaryLLDD");
            });

            modelBuilder.Entity<PostAdd>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.PostAddId })
                    .HasName("PK__PostAdd__06859E6EB8D93717");

                entity.ToTable("PostAdd", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.PostAddId).HasColumnName("PostAdd_Id");

                entity.Property(e => e.AddLine1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine3)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AddLine4)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerContactId).HasColumnName("LearnerContact_Id");
            });

            modelBuilder.Entity<ProviderSpecDeliveryMonitoring>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.ProviderSpecDeliveryMonitoringId })
                    .HasName("PK__Provider__59EDCAF098A9343E");

                entity.ToTable("ProviderSpecDeliveryMonitoring", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.ProviderSpecDeliveryMonitoringId).HasColumnName("ProviderSpecDeliveryMonitoring_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearningDeliveryId).HasColumnName("LearningDelivery_Id");

                entity.Property(e => e.ProvSpecDelMon)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecDelMonOccur)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProviderSpecLearnerMonitoring>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.ProviderSpecLearnerMonitoringId })
                    .HasName("PK__Provider__CFD386452F7812F8");

                entity.ToTable("ProviderSpecLearnerMonitoring", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.ProviderSpecLearnerMonitoringId).HasColumnName("ProviderSpecLearnerMonitoring_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnerId).HasColumnName("Learner_Id");

                entity.Property(e => e.ProvSpecLearnMon)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProvSpecLearnMonOccur)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SfaLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__SFA_Lear__0C29443AD37AEF1F");

                entity.ToTable("SFA_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchApplicDate).HasColumnType("date");

                entity.Property(e => e.AchPayTransHeldBack).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AchieveElement).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AchievePayPctPreTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ActualDaysIl).HasColumnName("ActualDaysIL");

                entity.Property(e => e.AdjLearnStartDate).HasColumnType("date");

                entity.Property(e => e.AimValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AppAdjLearnStartDate).HasColumnType("date");

                entity.Property(e => e.AppAgeFact).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AppAtagta).HasColumnName("AppATAGTA");

                entity.Property(e => e.AppFuncSkill1618AdjFact).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AppLearnStartDate).HasColumnType("date");

                entity.Property(e => e.ApplicFundRateDate).HasColumnType("date");

                entity.Property(e => e.ApplicProgWeightFact)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicUnweightFundRate).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ApplicWeightFundRate).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AreaCostFactAdj).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.BaseValueUnweight).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.CapFactor).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.DisUpFactAdj).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.EmpOutcomePctHeldBackTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EmpOutcomePctPreTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Esol).HasColumnName("ESOL");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LargeEmployerId).HasColumnName("LargeEmployerID");

                entity.Property(e => e.LargeEmployerSfafctr)
                    .HasColumnName("LargeEmployerSFAFctr")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.LargeEmployerStatusDate).HasColumnType("date");

                entity.Property(e => e.LtrcupliftFctr)
                    .HasColumnName("LTRCUpliftFctr")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.NonGovCont).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Olasscustody).HasColumnName("OLASSCustody");

                entity.Property(e => e.OnProgPayPctPreTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PlannedTotalDaysIl).HasColumnName("PlannedTotalDaysIL");

                entity.Property(e => e.PlannedTotalDaysIlpreTrans).HasColumnName("PlannedTotalDaysILPreTrans");

                entity.Property(e => e.PropFundRemain).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PropFundRemainAch).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PrscHeaim).HasColumnName("PrscHEAim");

                entity.Property(e => e.SpecResUplift).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.StartPropTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.TrnAdjLearnStartDate).HasColumnType("date");

                entity.Property(e => e.UnWeightedRateFromEsol)
                    .HasColumnName("UnWeightedRateFromESOL")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.UnweightedRateFromLars)
                    .HasColumnName("UnweightedRateFromLARS")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.WeightedRateFromEsol)
                    .HasColumnName("WeightedRateFromESOL")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.WeightedRateFromLars)
                    .HasColumnName("WeightedRateFromLARS")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Wplprov).HasColumnName("WPLProv");
            });

            modelBuilder.Entity<SfaLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period })
                    .HasName("PK__SFA_Lear__29582317D5F9B06D");

                entity.ToTable("SFA_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchievePayPct).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AchievePayPctTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AchievePayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.BalancePayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.BalancePaymentUncapped).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.BalancePct).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.BalancePctTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EmpOutcomePay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EmpOutcomePct).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EmpOutcomePctTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnSuppFundCash).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.OnProgPayPct).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.OnProgPayPctTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.OnProgPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.OnProgPaymentUncapped).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<SfaLearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("PK__SFA_Lear__FED24A87D8F0B21A");

                entity.ToTable("SFA_LearningDelivery_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<Source1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.SourceId })
                    .HasName("PK__Source__87B3359B20AB8215");

                entity.ToTable("Source", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.SourceId).HasColumnName("Source_Id");

                entity.Property(e => e.ComponentSetVersion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.ProtectiveMarking)
                    .HasMaxLength(100)
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

            modelBuilder.Entity<SourceFile1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.SourceFileId })
                    .HasName("PK__SourceFi__82EC4B8427491B4B");

                entity.ToTable("SourceFile", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.SourceFileId).HasColumnName("SourceFile_Id");

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

                entity.Property(e => e.SourceFileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("pk_TBL_LearningDelivery");

                entity.ToTable("TBL_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchApplicDate).HasColumnType("date");

                entity.Property(e => e.AchievementApplicVal).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ActualDaysIl).HasColumnName("ActualDaysIL");

                entity.Property(e => e.AdjProgStartDate).HasColumnType("date");

                entity.Property(e => e.AdjStartDate).HasColumnType("date");

                entity.Property(e => e.ApplicFundValDate).HasColumnType("date");

                entity.Property(e => e.CombinedAdjProp).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.CoreGovContCapApplicVal).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LearnAimRef)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelDaysIl).HasColumnName("LearnDelDaysIL");

                entity.Property(e => e.LearnDelStandardAccDaysIl).HasColumnName("LearnDelStandardAccDaysIL");

                entity.Property(e => e.LearnDelStandardPrevAccDaysIl).HasColumnName("LearnDelStandardPrevAccDaysIL");

                entity.Property(e => e.LearnDelStandardTotalDaysIl).HasColumnName("LearnDelStandardTotalDaysIL");

                entity.Property(e => e.MathEngAimValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngLsffundStart).HasColumnName("MathEngLSFFundStart");

                entity.Property(e => e.MathEngLsfthresholdDays).HasColumnName("MathEngLSFThresholdDays");

                entity.Property(e => e.PlannedTotalDaysIl).HasColumnName("PlannedTotalDaysIL");

                entity.Property(e => e.ProgStandardStartDate).HasColumnType("date");

                entity.Property(e => e.SmallBusApplicVal).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.SmallBusThresholdDate).HasColumnType("date");

                entity.Property(e => e.YoungAppApplicVal).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.YoungAppFirstThresholdDate).HasColumnType("date");

                entity.Property(e => e.YoungAppSecondThresholdDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period })
                    .HasName("pk_TBL_LearningDelivery_Period");

                entity.ToTable("TBL_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.CoreGovContPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.CoreGovContUncapped).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnSuppFundCash).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngBalPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngBalPct).HasColumnType("decimal(8, 5)");

                entity.Property(e => e.MathEngOnProgPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngOnProgPct).HasColumnType("decimal(8, 5)");

                entity.Property(e => e.SmallBusPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.YoungAppFirstPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.YoungAppPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.YoungAppSecondPayment).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<TblLearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("pk_TBL_LearningDelivery_PeriodisedValues");

                entity.ToTable("TBL_LearningDelivery_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<TrailblazerApprenticeshipFinancialRecord1>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.TrailblazerApprenticeshipFinancialRecordId })
                    .HasName("PK__Trailbla__4EFD33E4DC3C909A");

                entity.ToTable("TrailblazerApprenticeshipFinancialRecord", "Invalid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.TrailblazerApprenticeshipFinancialRecordId).HasColumnName("TrailblazerApprenticeshipFinancialRecord_Id");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearningDeliveryId).HasColumnName("LearningDelivery_Id");

                entity.Property(e => e.TbfinAmount).HasColumnName("TBFinAmount");

                entity.Property(e => e.TbfinCode).HasColumnName("TBFinCode");

                entity.Property(e => e.TbfinDate)
                    .HasColumnName("TBFinDate")
                    .HasColumnType("date");

                entity.Property(e => e.TbfinType)
                    .HasColumnName("TBFinType")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VersionInfo>(entity =>
            {
                entity.HasKey(e => e.Version);

                entity.ToTable("VersionInfo");

                entity.Property(e => e.Version).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");
            });
        }
    }
}
