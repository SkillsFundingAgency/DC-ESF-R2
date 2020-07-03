using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class ILR1617Context : DbContext
    {
        public ILR1617Context()
        {
        }

        public ILR1617Context(DbContextOptions<ILR1617Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AecApprenticeshipPriceEpisode> AecApprenticeshipPriceEpisode { get; set; }
        public virtual DbSet<AecApprenticeshipPriceEpisodePeriod> AecApprenticeshipPriceEpisodePeriod { get; set; }
        public virtual DbSet<AecApprenticeshipPriceEpisodePeriodisedValues> AecApprenticeshipPriceEpisodePeriodisedValues { get; set; }
        public virtual DbSet<AecGlobal> AecGlobal { get; set; }
        public virtual DbSet<AecLearningDelivery> AecLearningDelivery { get; set; }
        public virtual DbSet<AecLearningDeliveryPeriod> AecLearningDeliveryPeriod { get; set; }
        public virtual DbSet<AecLearningDeliveryPeriodisedTextValues> AecLearningDeliveryPeriodisedTextValues { get; set; }
        public virtual DbSet<AecLearningDeliveryPeriodisedValues> AecLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<AeLearningDelivery> AeLearningDelivery { get; set; }
        public virtual DbSet<AeLearningDeliveryPeriod> AeLearningDeliveryPeriod { get; set; }
        public virtual DbSet<AeLearningDeliveryPeriodisedValues> AeLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<AlbGlobal> AlbGlobal { get; set; }
        public virtual DbSet<AlbLearnerPeriod> AlbLearnerPeriod { get; set; }
        public virtual DbSet<AlbLearnerPeriodisedValues> AlbLearnerPeriodisedValues { get; set; }
        public virtual DbSet<AlbLearningDelivery> AlbLearningDelivery { get; set; }
        public virtual DbSet<AlbLearningDeliveryPeriod> AlbLearningDeliveryPeriod { get; set; }
        public virtual DbSet<AlbLearningDeliveryPeriodisedValues> AlbLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<CollectionDetails> CollectionDetails { get; set; }
        public virtual DbSet<ContactPreference> ContactPreference { get; set; }
        public virtual DbSet<DasLearnerCommitment> DasLearnerCommitment { get; set; }
        public virtual DbSet<Dpoutcome> Dpoutcome { get; set; }
        public virtual DbSet<DvGlobal> DvGlobal { get; set; }
        public virtual DbSet<DvLearner> DvLearner { get; set; }
        public virtual DbSet<DvLearningDelivery> DvLearningDelivery { get; set; }
        public virtual DbSet<EfaGlobal> EfaGlobal { get; set; }
        public virtual DbSet<EfaLearner> EfaLearner { get; set; }
        public virtual DbSet<EfaSfaGlobal> EfaSfaGlobal { get; set; }
        public virtual DbSet<EfaSfaLearnerPeriod> EfaSfaLearnerPeriod { get; set; }
        public virtual DbSet<EfaSfaLearnerPeriodisedValues> EfaSfaLearnerPeriodisedValues { get; set; }
        public virtual DbSet<EsfDpoutcome> EsfDpoutcome { get; set; }
        public virtual DbSet<EsfGlobal> EsfGlobal { get; set; }
        public virtual DbSet<EsfLearningDelivery> EsfLearningDelivery { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverable> EsfLearningDeliveryDeliverable { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverablePeriod> EsfLearningDeliveryDeliverablePeriod { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverablePeriodisedValues> EsfLearningDeliveryDeliverablePeriodisedValues { get; set; }
        public virtual DbSet<EsfvalGlobal> EsfvalGlobal { get; set; }
        public virtual DbSet<EsfvalValidationError> EsfvalValidationError { get; set; }
        public virtual DbSet<FileDetails> FileDetails { get; set; }
        public virtual DbSet<Learner> Learner { get; set; }
        public virtual DbSet<LearnerContact> LearnerContact { get; set; }
        public virtual DbSet<LearnerDestinationandProgression> LearnerDestinationandProgression { get; set; }
        public virtual DbSet<LearnerEmploymentStatus> LearnerEmploymentStatus { get; set; }
        public virtual DbSet<LearnerHe> LearnerHe { get; set; }
        public virtual DbSet<LearnerHefinancialSupport> LearnerHefinancialSupport { get; set; }
        public virtual DbSet<LearningDelivery> LearningDelivery { get; set; }
        public virtual DbSet<LearningDeliveryFam> LearningDeliveryFam { get; set; }
        public virtual DbSet<LearningDeliveryHe> LearningDeliveryHe { get; set; }
        public virtual DbSet<LearningDeliveryWorkPlacement> LearningDeliveryWorkPlacement { get; set; }
        public virtual DbSet<LearningProvider> LearningProvider { get; set; }
        public virtual DbSet<LlddandHealthProblem> LlddandHealthProblem { get; set; }
        public virtual DbSet<PriceEpisodeMatch> PriceEpisodeMatch { get; set; }
        public virtual DbSet<PriceEpisodePeriodMatch> PriceEpisodePeriodMatch { get; set; }
        public virtual DbSet<ProcessingData> ProcessingData { get; set; }
        public virtual DbSet<SfaGlobal> SfaGlobal { get; set; }
        public virtual DbSet<SfaLearningDelivery> SfaLearningDelivery { get; set; }
        public virtual DbSet<SfaLearningDeliveryPeriod> SfaLearningDeliveryPeriod { get; set; }
        public virtual DbSet<SfaLearningDeliveryPeriodisedValues> SfaLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<SourceFile> SourceFile { get; set; }
        public virtual DbSet<TblGlobal> TblGlobal { get; set; }
        public virtual DbSet<TblLearningDelivery> TblLearningDelivery { get; set; }
        public virtual DbSet<TblLearningDeliveryPeriod> TblLearningDeliveryPeriod { get; set; }
        public virtual DbSet<TblLearningDeliveryPeriodisedValues> TblLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<TblvalGlobal> TblvalGlobal { get; set; }
        public virtual DbSet<TblvalValidationError> TblvalValidationError { get; set; }
        public virtual DbSet<TrailblazerApprenticeshipFinancialRecord> TrailblazerApprenticeshipFinancialRecord { get; set; }
        public virtual DbSet<ValdpGlobal> ValdpGlobal { get; set; }
        public virtual DbSet<ValdpValidationError> ValdpValidationError { get; set; }
        public virtual DbSet<ValfdValidationError> ValfdValidationError { get; set; }
        public virtual DbSet<ValGlobal> ValGlobal { get; set; }
        public virtual DbSet<ValidationError> ValidationError { get; set; }
        public virtual DbSet<ValidationError1> ValidationError1 { get; set; }
        public virtual DbSet<ValValidationError> ValValidationError { get; set; }
        public virtual DbSet<VersionInfo> VersionInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AecApprenticeshipPriceEpisode>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier });

                entity.ToTable("AEC_ApprenticeshipPriceEpisode", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EpisodeEffectiveTnpstartDate)
                    .HasColumnName("EpisodeEffectiveTNPStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.EpisodeStartDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeActualEndDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeCappedRemainingTnpamount)
                    .HasColumnName("PriceEpisodeCappedRemainingTNPAmount")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeCompletionElement).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeContractType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeExpectedTotalMonthlyValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeFirstAdditionalPaymentThresholdDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeFundLineType)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeInstalmentValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodePlannedEndDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodePreviousEarnings).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodePreviousEarningsSameProvider).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeRemainingAmountWithinUpperLimit).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeRemainingTnpamount)
                    .HasColumnName("PriceEpisodeRemainingTNPAmount")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeSecondAdditionalPaymentThresholdDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeTotalEarnings).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeTotalTnpprice)
                    .HasColumnName("PriceEpisodeTotalTNPPrice")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeUpperBandLimit).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeUpperLimitAdjustment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Tnp1)
                    .HasColumnName("TNP1")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Tnp2)
                    .HasColumnName("TNP2")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Tnp3)
                    .HasColumnName("TNP3")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Tnp4)
                    .HasColumnName("TNP4")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<AecApprenticeshipPriceEpisodePeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier, e.Period });

                entity.ToTable("AEC_ApprenticeshipPriceEpisode_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftBalancing).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftCompletionPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftOnProgPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeBalancePayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeBalanceValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeCompletionPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeFirstDisadvantagePayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeFirstEmp1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeFirstProv1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeLsfcash)
                    .HasColumnName("PriceEpisodeLSFCash")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeOnProgPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeSecondDisadvantagePayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeSecondEmp1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeSecondProv1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PriceEpisodeSfacontribPct)
                    .HasColumnName("PriceEpisodeSFAContribPct")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<AecApprenticeshipPriceEpisodePeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier, e.AttributeName });

                entity.ToTable("AEC_ApprenticeshipPriceEpisode_PeriodisedValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .HasMaxLength(25)
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

            modelBuilder.Entity<AecGlobal>(entity =>
            {
                entity.ToTable("AEC_global", "Rulebase");

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AecLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

                entity.ToTable("AEC_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ActualDaysIl).HasColumnName("ActualDaysIL");

                entity.Property(e => e.AdjStartDate).HasColumnType("date");

                entity.Property(e => e.AppAdjLearnStartDate).HasColumnType("date");

                entity.Property(e => e.AppAdjLearnStartDateMatchPathway).HasColumnType("date");

                entity.Property(e => e.ApplicCompDate).HasColumnType("date");

                entity.Property(e => e.CombinedAdjProp).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.DisUpFactAdj).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.FirstIncentiveThresholdDate).HasColumnType("date");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftBalancingValue)
                    .HasColumnName("LDApplic1618FrameworkUpliftBalancingValue")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftCompElement)
                    .HasColumnName("LDApplic1618FrameworkUpliftCompElement")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftCompletionValue)
                    .HasColumnName("LDApplic1618FRameworkUpliftCompletionValue")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftMonthInstalVal)
                    .HasColumnName("LDApplic1618FrameworkUpliftMonthInstalVal")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftPrevEarnings)
                    .HasColumnName("LDApplic1618FrameworkUpliftPrevEarnings")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftPrevEarningsStage1)
                    .HasColumnName("LDApplic1618FrameworkUpliftPrevEarningsStage1")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftRemainingAmount)
                    .HasColumnName("LDApplic1618FrameworkUpliftRemainingAmount")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftTotalActEarnings)
                    .HasColumnName("LDApplic1618FrameworkUpliftTotalActEarnings")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnAimRef)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelAppAccDaysIl).HasColumnName("LearnDelAppAccDaysIL");

                entity.Property(e => e.LearnDelAppPrevAccDaysIl).HasColumnName("LearnDelAppPrevAccDaysIL");

                entity.Property(e => e.LearnDelApplicDisadvAmount).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelApplicEmp1618Incentive).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelApplicEmpDate).HasColumnType("date");

                entity.Property(e => e.LearnDelApplicProv1618FrameworkUplift).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelApplicProv1618Incentive).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelApplicTot1618Incentive).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelDaysIl).HasColumnName("LearnDelDaysIL");

                entity.Property(e => e.LearnDelDisadAmount).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelHistProgEarnings).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelInitialFundLineType)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelSemcontWaiver).HasColumnName("LearnDelSEMContWaiver");

                entity.Property(e => e.MathEngAimValue).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PlannedTotalDaysIl).HasColumnName("PlannedTotalDaysIL");

                entity.Property(e => e.SecondIncentiveThresholdDate).HasColumnType("date");
            });

            modelBuilder.Entity<AecLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period });

                entity.ToTable("AEC_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DisadvFirstPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.DisadvSecondPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.FundLineType)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Ldapplic1618FrameworkUpliftBalancingPayment)
                    .HasColumnName("LDApplic1618FrameworkUpliftBalancingPayment")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftCompletionPayment)
                    .HasColumnName("LDApplic1618FrameworkUpliftCompletionPayment")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftOnProgPayment)
                    .HasColumnName("LDApplic1618FrameworkUpliftOnProgPayment")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelContType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelFirstEmp1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelFirstProv1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelSecondEmp1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelSecondProv1618Pay).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnDelSfacontribPct)
                    .HasColumnName("LearnDelSFAContribPct")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LearnSuppFundCash).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngBalPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngBalPct).HasColumnType("decimal(8, 5)");

                entity.Property(e => e.MathEngOnProgPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MathEngOnProgPct).HasColumnType("decimal(8, 5)");

                entity.Property(e => e.ProgrammeAimBalPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ProgrammeAimCompletionPayment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ProgrammeAimOnProgPayment).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<AecLearningDeliveryPeriodisedTextValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

                entity.ToTable("AEC_LearningDelivery_PeriodisedTextValues", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AecLearningDeliveryPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

                entity.ToTable("AEC_LearningDelivery_PeriodisedValues", "Rulebase");

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

            modelBuilder.Entity<AeLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.LearnRefNumber, e.AimSeqNumber, e.Ukprn });

                entity.ToTable("AE_LearningDelivery", "Rulebase");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.CompletionPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.CompletionPaymentUncapped).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.LearnActEndDate).HasColumnType("date");

                entity.Property(e => e.LearnPlanEndDate).HasColumnType("date");

                entity.Property(e => e.LearnStartDate).HasColumnType("date");

                entity.Property(e => e.MonthlyInstallment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MonthlyInstallmentUncapped).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.NiNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.OrigLearnStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<AeLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period });

                entity.ToTable("AE_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ProgrammeAimBalPayment)
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProgrammeAimCompletionPayment)
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProgrammeAimOnProgPayment)
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<AeLearningDeliveryPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

                entity.ToTable("AE_LearningDelivery_PeriodisedValues", "Rulebase");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Period1)
                    .HasColumnName("Period_1")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(15, 5)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<AlbGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("ALB_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostcodeAreaCostVersion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlbLearnerPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Period });

                entity.ToTable("ALB_Learner_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AlbseqNum).HasColumnName("ALBSeqNum");
            });

            modelBuilder.Entity<AlbLearnerPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AttributeName });

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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

                entity.ToTable("ALB_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicFactDate).HasColumnType("date");

                entity.Property(e => e.ApplicProgWeightFact)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AreaCostFactAdj).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AreaCostInstalment).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LiabilityDate).HasColumnType("date");

                entity.Property(e => e.WeightedRate).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<AlbLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period });

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

            modelBuilder.Entity<AlbLearningDeliveryPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

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

            modelBuilder.Entity<CollectionDetails>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("CollectionDetails", "Valid");

                entity.HasIndex(e => e.Ukprn)
                    .HasName("IX_Valid_CollectionDetails");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

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

            modelBuilder.Entity<ContactPreference>(entity =>
            {
                entity.ToTable("ContactPreference", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("IX_Valid_ContactPreference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContPrefType)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<DasLearnerCommitment>(entity =>
            {
                entity.ToTable("DasLearnerCommitment", "DataLock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommitmentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dpoutcome>(entity =>
            {
                entity.ToTable("DPOutcome", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("IX_Valid_DPOutcome");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OutCollDate).HasColumnType("date");

                entity.Property(e => e.OutEndDate).HasColumnType("date");

                entity.Property(e => e.OutStartDate).HasColumnType("date");

                entity.Property(e => e.OutType)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<DvGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("DV_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DvLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

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

            modelBuilder.Entity<EfaGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("EFA_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrgVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostcodeDisadvantageVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EfaLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

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
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ConditionOfFundingMaths)
                    .HasMaxLength(100)
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

                entity.Property(e => e.PrvHistLrgProgPropn).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PrvRetentFactHist).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.RateBand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RetentNew).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EfaSfaGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("EFA_SFA_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EfaSfaLearnerPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Period });

                entity.ToTable("EFA_SFA_Learner_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LnrOnProgPay).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<EfaSfaLearnerPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AttributeName });

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

            modelBuilder.Entity<EsfDpoutcome>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.OutCode, e.OutType, e.OutStartDate });

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

            modelBuilder.Entity<EsfGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("ESF_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EsfLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode });

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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode, e.Period });

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

            modelBuilder.Entity<EsfLearningDeliveryDeliverablePeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode, e.AttributeName });

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

            modelBuilder.Entity<EsfvalGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("ESFVAL_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EsfvalValidationError>(entity =>
            {
                entity.ToTable("ESFVAL_ValidationError", "Rulebase");

                entity.Property(e => e.ErrorString)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FieldValues)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<FileDetails>(entity =>
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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

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

            modelBuilder.Entity<LearnerContact>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

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

            modelBuilder.Entity<LearnerDestinationandProgression>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

                entity.ToTable("LearnerDestinationandProgression", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");
            });

            modelBuilder.Entity<LearnerEmploymentStatus>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.DateEmpStatApp });

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

                entity.Property(e => e.EmpStatMonSei).HasColumnName("EmpStatMon_SEI");

                entity.Property(e => e.EmpStatMonSem).HasColumnName("EmpStatMon_SEM");
            });

            modelBuilder.Entity<LearnerHe>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

                entity.ToTable("LearnerHE", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ttaccom).HasColumnName("TTACCOM");

                entity.Property(e => e.Ucasperid).HasColumnName("UCASPERID");
            });

            modelBuilder.Entity<LearnerHefinancialSupport>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Fintype });

                entity.ToTable("LearnerHEFinancialSupport", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Fintype).HasColumnName("FINTYPE");

                entity.Property(e => e.Finamount).HasColumnName("FINAMOUNT");
            });

            modelBuilder.Entity<LearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

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

            modelBuilder.Entity<LearningDeliveryFam>(entity =>
            {
                entity.ToTable("LearningDeliveryFAM", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.LearnDelFamtype })
                    .HasName("IX_Valid_LearningDeliveryFAM");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LearnDelFamcode)
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
                    .HasColumnName("LearnDelFAMType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<LearningDeliveryHe>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

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

            modelBuilder.Entity<LearningDeliveryWorkPlacement>(entity =>
            {
                entity.ToTable("LearningDeliveryWorkPlacement", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("IX_Valid_LearningDeliveryWorkPlacement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.WorkPlaceEndDate).HasColumnType("date");

                entity.Property(e => e.WorkPlaceStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<LearningProvider>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("LearningProvider", "Valid");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<LlddandHealthProblem>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Llddcat });

                entity.ToTable("LLDDandHealthProblem", "Valid");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Llddcat).HasColumnName("LLDDCat");

                entity.Property(e => e.PrimaryLldd).HasColumnName("PrimaryLLDD");
            });

            modelBuilder.Entity<PriceEpisodeMatch>(entity =>
            {
                entity.ToTable("PriceEpisodeMatch", "DataLock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CollectionPeriodName)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CommitmentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PriceEpisodePeriodMatch>(entity =>
            {
                entity.ToTable("PriceEpisodePeriodMatch", "DataLock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CollectionPeriodName)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProcessingData>(entity =>
            {
                entity.Property(e => e.ExecutionTime).HasMaxLength(20);

                entity.Property(e => e.FileDetailsId).HasColumnName("FileDetailsID");

                entity.Property(e => e.ProcessingStep).HasMaxLength(100);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<SfaGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("SFA_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.CurFundYr)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrgVersion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostcodeDisadvantageVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SfaLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

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

                entity.Property(e => e.ApplicEmpFactDate).HasColumnType("date");

                entity.Property(e => e.ApplicFactDate).HasColumnType("date");

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
            });

            modelBuilder.Entity<SfaLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period });

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

            modelBuilder.Entity<SfaLearningDeliveryPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

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

            modelBuilder.Entity<SourceFile>(entity =>
            {
                entity.ToTable("SourceFile", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.SourceFileName })
                    .HasName("IX_Valid_SourceFile");

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<TblGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("TBL_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.CurFundYr)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

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

                entity.Property(e => e.LdlnrRedDate)
                    .HasColumnName("LDLnrRedDate")
                    .HasColumnType("date");

                entity.Property(e => e.LdredundancyCashMonths).HasColumnName("LDRedundancyCashMonths");

                entity.Property(e => e.LdredundancyCashRemainingCapped)
                    .HasColumnName("LDRedundancyCashRemainingCapped")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.LdredundancyCashRemainingUncapped)
                    .HasColumnName("LDRedundancyCashRemainingUncapped")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.LdredundancyCashStartDate)
                    .HasColumnName("LDRedundancyCashStartDate")
                    .HasColumnType("date");

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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period });

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

            modelBuilder.Entity<TblLearningDeliveryPeriodisedValues>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

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

            modelBuilder.Entity<TblvalGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("TBLVAL_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblvalValidationError>(entity =>
            {
                entity.ToTable("TBLVAL_ValidationError", "Rulebase");

                entity.Property(e => e.ErrorString)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FieldValues)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<TrailblazerApprenticeshipFinancialRecord>(entity =>
            {
                entity.ToTable("TrailblazerApprenticeshipFinancialRecord", "Valid");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.TbfinType })
                    .HasName("IX_Valid_TrailblazerApprenticeshipFinancialRecord");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LearnRefNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TbfinAmount).HasColumnName("TBFinAmount");

                entity.Property(e => e.TbfinCode).HasColumnName("TBFinCode");

                entity.Property(e => e.TbfinDate)
                    .HasColumnName("TBFinDate")
                    .HasColumnType("date");

                entity.Property(e => e.TbfinType)
                    .IsRequired()
                    .HasColumnName("TBFinType")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<ValdpGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("VALDP_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrgVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ulnversion)
                    .HasColumnName("ULNVersion")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValdpValidationError>(entity =>
            {
                entity.ToTable("VALDP_ValidationError", "Rulebase");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ErrorString)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FieldValues)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<ValfdValidationError>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.AimSeqNumber, e.LearnRefNumber });

                entity.ToTable("VALFD_ValidationError", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorString)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FieldValues)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("VAL_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployerVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrgVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostcodeVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValidationError>(entity =>
            {
                entity.Property(e => e.FieldValues).HasMaxLength(2000);

                entity.Property(e => e.LearnAimRef)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RuleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Severity)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SwsupAimId)
                    .HasColumnName("SWSupAimID")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<ValidationError1>(entity =>
            {
                entity.ToTable("ValidationError", "DataLock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CollectionPeriodName)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValValidationError>(entity =>
            {
                entity.ToTable("VAL_ValidationError", "Rulebase");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ErrorString)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FieldValues)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<VersionInfo>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
