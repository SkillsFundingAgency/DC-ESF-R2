using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class ILR1920_Rulebase : DbContext
    {
        public ILR1920_Rulebase()
        {
        }

        public ILR1920_Rulebase(DbContextOptions<ILR1920_Rulebase> options)
            : base(options)
        {
        }

        public virtual DbSet<AecApprenticeshipPriceEpisode> AecApprenticeshipPriceEpisodes { get; set; }
        public virtual DbSet<AecApprenticeshipPriceEpisodePeriod> AecApprenticeshipPriceEpisodePeriods { get; set; }
        public virtual DbSet<AecApprenticeshipPriceEpisodePeriodisedValue> AecApprenticeshipPriceEpisodePeriodisedValues { get; set; }
        public virtual DbSet<AecGlobal> AecGlobals { get; set; }
        public virtual DbSet<AecHistoricEarningOutput> AecHistoricEarningOutputs { get; set; }
        public virtual DbSet<AecLearner> AecLearners { get; set; }
        public virtual DbSet<AecLearningDelivery> AecLearningDeliveries { get; set; }
        public virtual DbSet<AecLearningDeliveryPeriod> AecLearningDeliveryPeriods { get; set; }
        public virtual DbSet<AecLearningDeliveryPeriodisedTextValue> AecLearningDeliveryPeriodisedTextValues { get; set; }
        public virtual DbSet<AecLearningDeliveryPeriodisedValue> AecLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<AlbGlobal> AlbGlobals { get; set; }
        public virtual DbSet<AlbLearner> AlbLearners { get; set; }
        public virtual DbSet<AlbLearnerPeriod> AlbLearnerPeriods { get; set; }
        public virtual DbSet<AlbLearnerPeriodisedValue> AlbLearnerPeriodisedValues { get; set; }
        public virtual DbSet<AlbLearningDelivery> AlbLearningDeliveries { get; set; }
        public virtual DbSet<AlbLearningDeliveryPeriod> AlbLearningDeliveryPeriods { get; set; }
        public virtual DbSet<AlbLearningDeliveryPeriodisedValue> AlbLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<DvGlobal> DvGlobals { get; set; }
        public virtual DbSet<DvLearner> DvLearners { get; set; }
        public virtual DbSet<DvLearningDelivery> DvLearningDeliveries { get; set; }
        public virtual DbSet<EsfDpoutcome> EsfDpoutcomes { get; set; }
        public virtual DbSet<EsfGlobal> EsfGlobals { get; set; }
        public virtual DbSet<EsfLearner> EsfLearners { get; set; }
        public virtual DbSet<EsfLearningDelivery> EsfLearningDeliveries { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverable> EsfLearningDeliveryDeliverables { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverablePeriod> EsfLearningDeliveryDeliverablePeriods { get; set; }
        public virtual DbSet<EsfLearningDeliveryDeliverablePeriodisedValue> EsfLearningDeliveryDeliverablePeriodisedValues { get; set; }
        public virtual DbSet<EsfvalGlobal> EsfvalGlobals { get; set; }
        public virtual DbSet<EsfvalValidationError> EsfvalValidationErrors { get; set; }
        public virtual DbSet<FileDetail> FileDetails { get; set; }
        public virtual DbSet<Fm25Fm35Global> Fm25Fm35Globals { get; set; }
        public virtual DbSet<Fm25Fm35LearnerPeriod> Fm25Fm35LearnerPeriods { get; set; }
        public virtual DbSet<Fm25Fm35LearnerPeriodisedValue> Fm25Fm35LearnerPeriodisedValues { get; set; }
        public virtual DbSet<Fm25Global> Fm25Globals { get; set; }
        public virtual DbSet<Fm25Learner> Fm25Learners { get; set; }
        public virtual DbSet<Fm35Global> Fm35Globals { get; set; }
        public virtual DbSet<Fm35Learner> Fm35Learners { get; set; }
        public virtual DbSet<Fm35LearningDelivery> Fm35LearningDeliveries { get; set; }
        public virtual DbSet<Fm35LearningDeliveryPeriod> Fm35LearningDeliveryPeriods { get; set; }
        public virtual DbSet<Fm35LearningDeliveryPeriodisedValue> Fm35LearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<ProcessingData> ProcessingDatas { get; set; }
        public virtual DbSet<TblGlobal> TblGlobals { get; set; }
        public virtual DbSet<TblLearner> TblLearners { get; set; }
        public virtual DbSet<TblLearningDelivery> TblLearningDeliveries { get; set; }
        public virtual DbSet<TblLearningDeliveryPeriod> TblLearningDeliveryPeriods { get; set; }
        public virtual DbSet<TblLearningDeliveryPeriodisedValue> TblLearningDeliveryPeriodisedValues { get; set; }
        public virtual DbSet<ValGlobal> ValGlobals { get; set; }
        public virtual DbSet<ValLearner> ValLearners { get; set; }
        public virtual DbSet<ValLearningDelivery> ValLearningDeliveries { get; set; }
        public virtual DbSet<ValValidationError> ValValidationErrors { get; set; }
        public virtual DbSet<ValdpGlobal> ValdpGlobals { get; set; }
        public virtual DbSet<ValdpValidationError> ValdpValidationErrors { get; set; }
        public virtual DbSet<ValfdValidationError> ValfdValidationErrors { get; set; }
        public virtual DbSet<ValidationError> ValidationErrors { get; set; }
        public virtual DbSet<VersionInfo> VersionInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=ILR1920_DataStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<AecApprenticeshipPriceEpisode>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier })
                    .HasName("PK__AEC_Appr__BCF596CA0B39A701");

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

                entity.Property(e => e.PriceEpisode1618FrameworkUpliftRemainingAmount).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisode1618FrameworkUpliftTotPrevEarnings).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisode1618FubalValue)
                    .HasColumnName("PriceEpisode1618FUBalValue")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisode1618FumonthInstValue)
                    .HasColumnName("PriceEpisode1618FUMonthInstValue")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisode1618FutotEarnings)
                    .HasColumnName("PriceEpisode1618FUTotEarnings")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeActualEndDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeActualEndDateIncEpa)
                    .HasColumnName("PriceEpisodeActualEndDateIncEPA")
                    .HasColumnType("date");

                entity.Property(e => e.PriceEpisodeAgreeId)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftCompElement).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeCappedRemainingTnpamount)
                    .HasColumnName("PriceEpisodeCappedRemainingTNPAmount")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeCompletionElement).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeContractType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeCumulativePmrs)
                    .HasColumnName("PriceEpisodeCumulativePMRs")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeExpectedTotalMonthlyValue).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeFirstAdditionalPaymentThresholdDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeFundLineType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeInstalmentValue).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeLearnerAdditionalPaymentThresholdDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodePlannedEndDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodePreviousEarnings).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodePreviousEarningsSameProvider).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeRedStartDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeRemainingAmountWithinUpperLimit).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeRemainingTnpamount)
                    .HasColumnName("PriceEpisodeRemainingTNPAmount")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeSecondAdditionalPaymentThresholdDate).HasColumnType("date");

                entity.Property(e => e.PriceEpisodeTotalEarnings).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeTotalPmrs)
                    .HasColumnName("PriceEpisodeTotalPMRs")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeTotalTnpprice)
                    .HasColumnName("PriceEpisodeTotalTNPPrice")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeUpperBandLimit).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeUpperLimitAdjustment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Tnp1)
                    .HasColumnName("TNP1")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Tnp2)
                    .HasColumnName("TNP2")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Tnp3)
                    .HasColumnName("TNP3")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Tnp4)
                    .HasColumnName("TNP4")
                    .HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AecLearner)
                    .WithMany(p => p.AecApprenticeshipPriceEpisodes)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECApprenticeshipPriceEpisode_AECLearner");
            });

            modelBuilder.Entity<AecApprenticeshipPriceEpisodePeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier, e.Period })
                    .HasName("PK__AEC_Appr__9984F1E77288B81D");

                entity.ToTable("AEC_ApprenticeshipPriceEpisode_Period", "Rulebase");

                entity.HasIndex(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier })
                    .HasName("ix_AEC_ApprenticeshipPriceEpisodePeriod");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeIdentifier)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftBalancing).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftCompletionPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeApplic1618FrameworkUpliftOnProgPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeBalancePayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeBalanceValue).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeCompletionPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeFirstDisadvantagePayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeFirstEmp1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeFirstProv1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeLearnerAdditionalPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeLsfcash)
                    .HasColumnName("PriceEpisodeLSFCash")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeOnProgPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeProgFundIndMaxEmpCont).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeProgFundIndMinCoInvest).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeSecondDisadvantagePayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeSecondEmp1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeSecondProv1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeSfacontribPct)
                    .HasColumnName("PriceEpisodeSFAContribPct")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PriceEpisodeTotProgFunding).HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AecApprenticeshipPriceEpisode)
                    .WithMany(p => p.AecApprenticeshipPriceEpisodePeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.PriceEpisodeIdentifier })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECApprenticeshipPriceEpisodePeriod_AECApprenticeshipPriceEpisode");
            });

            modelBuilder.Entity<AecApprenticeshipPriceEpisodePeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.PriceEpisodeIdentifier, e.AttributeName })
                    .HasName("PK__AEC_Appr__4E0E987763675384");

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
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AecApprenticeshipPriceEpisode)
                    .WithMany(p => p.AecApprenticeshipPriceEpisodePeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.PriceEpisodeIdentifier })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECApprenticeshipPriceEpisodePeriodisedValues_AECApprenticeshipPriceEpisode");
            });

            modelBuilder.Entity<AecGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn);

                entity.ToTable("AEC_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.Larsversion)
                    .HasColumnName("LARSVersion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AecHistoricEarningOutput>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AppIdentifierOutput })
                    .HasName("PK__AEC_Hist__9CDF07426DC4DB13");

                entity.ToTable("AEC_HistoricEarningOutput", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AppIdentifierOutput)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HistoricEffectiveTnpstartDateOutput)
                    .HasColumnName("HistoricEffectiveTNPStartDateOutput")
                    .HasColumnType("date");

                entity.Property(e => e.HistoricLearnDelProgEarliestAct2dateOutput)
                    .HasColumnName("HistoricLearnDelProgEarliestACT2DateOutput")
                    .HasColumnType("date");

                entity.Property(e => e.HistoricPmramountOutput)
                    .HasColumnName("HistoricPMRAmountOutput")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricProgrammeStartDateIgnorePathwayOutput).HasColumnType("date");

                entity.Property(e => e.HistoricProgrammeStartDateMatchPathwayOutput).HasColumnType("date");

                entity.Property(e => e.HistoricStdcodeOutput).HasColumnName("HistoricSTDCodeOutput");

                entity.Property(e => e.HistoricTnp1output)
                    .HasColumnName("HistoricTNP1Output")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricTnp2output)
                    .HasColumnName("HistoricTNP2Output")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricTnp3output)
                    .HasColumnName("HistoricTNP3Output")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricTnp4output)
                    .HasColumnName("HistoricTNP4Output")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricTotal1618UpliftPaymentsInTheYear).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricTotalProgAimPaymentsInTheYear).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricUlnoutput).HasColumnName("HistoricULNOutput");

                entity.Property(e => e.HistoricUptoEndDateOutput).HasColumnType("date");

                entity.Property(e => e.HistoricVirtualTnp3endofThisYearOutput)
                    .HasColumnName("HistoricVirtualTNP3EndofThisYearOutput")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.HistoricVirtualTnp4endofThisYearOutput)
                    .HasColumnName("HistoricVirtualTNP4EndofThisYearOutput")
                    .HasColumnType("decimal(12, 5)");
            });

            modelBuilder.Entity<AecLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__AEC_Lear__2770A72759D65E19");

                entity.ToTable("AEC_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Uln).HasColumnName("ULN");

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.AecLearners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECLearner_AECglobal");
            });

            modelBuilder.Entity<AecLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__AEC_Lear__0C29443AE18CF161");

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

                entity.Property(e => e.CombinedAdjProp).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.FirstIncentiveThresholdDate).HasColumnType("date");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftTotalActEarnings)
                    .HasColumnName("LDApplic1618FrameworkUpliftTotalActEarnings")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnAimRef)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelAccDaysIlcareLeavers).HasColumnName("LearnDelAccDaysILCareLeavers");

                entity.Property(e => e.LearnDelAppAccDaysIl).HasColumnName("LearnDelAppAccDaysIL");

                entity.Property(e => e.LearnDelAppPrevAccDaysIl).HasColumnName("LearnDelAppPrevAccDaysIL");

                entity.Property(e => e.LearnDelApplicCareLeaverIncentive).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelApplicDisadvAmount).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelApplicEmp1618Incentive).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelApplicEmpDate).HasColumnType("date");

                entity.Property(e => e.LearnDelApplicProv1618FrameworkUplift).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelApplicProv1618Incentive).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelDaysIl).HasColumnName("LearnDelDaysIL");

                entity.Property(e => e.LearnDelDisadAmount).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelHistProgEarnings).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelInitialFundLineType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelLearnerAddPayThresholdDate).HasColumnType("date");

                entity.Property(e => e.LearnDelPrevAccDaysIlcareLeavers).HasColumnName("LearnDelPrevAccDaysILCareLeavers");

                entity.Property(e => e.LearnDelProgEarliestAct2date)
                    .HasColumnName("LearnDelProgEarliestACT2Date")
                    .HasColumnType("date");

                entity.Property(e => e.LearnDelRedStartDate).HasColumnType("date");

                entity.Property(e => e.MathEngAimValue).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.PlannedTotalDaysIl).HasColumnName("PlannedTotalDaysIL");

                entity.Property(e => e.SecondIncentiveThresholdDate).HasColumnType("date");

                entity.HasOne(d => d.AecLearner)
                    .WithMany(p => p.AecLearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECLearningDelivery_AECLearner");
            });

            modelBuilder.Entity<AecLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period })
                    .HasName("PK__AEC_Lear__295823176481E496");

                entity.ToTable("AEC_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DisadvFirstPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.DisadvSecondPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.FundLineType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ldapplic1618FrameworkUpliftBalancingPayment)
                    .HasColumnName("LDApplic1618FrameworkUpliftBalancingPayment")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftCompletionPayment)
                    .HasColumnName("LDApplic1618FrameworkUpliftCompletionPayment")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Ldapplic1618FrameworkUpliftOnProgPayment)
                    .HasColumnName("LDApplic1618FrameworkUpliftOnProgPayment")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelContType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelFirstEmp1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelFirstProv1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelLearnAddPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelSecondEmp1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelSecondProv1618Pay).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnDelSemcontWaiver).HasColumnName("LearnDelSEMContWaiver");

                entity.Property(e => e.LearnDelSfacontribPct)
                    .HasColumnName("LearnDelSFAContribPct")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.LearnSuppFundCash).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.MathEngBalPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.MathEngBalPct).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.MathEngOnProgPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.MathEngOnProgPct).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.ProgrammeAimBalPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.ProgrammeAimCompletionPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.ProgrammeAimOnProgPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.ProgrammeAimProgFundIndMaxEmpCont).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.ProgrammeAimProgFundIndMinCoInvest).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.ProgrammeAimTotProgFund).HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AecLearningDelivery)
                    .WithMany(p => p.AecLearningDeliveryPeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECLearningDeliveryPeriod_AECLearningDelivery");
            });

            modelBuilder.Entity<AecLearningDeliveryPeriodisedTextValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("PK__AEC_Lear__FED24A873F1C9B25");

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

                entity.HasOne(d => d.AecLearningDelivery)
                    .WithMany(p => p.AecLearningDeliveryPeriodisedTextValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECLearningDeliveryPeriodisedTextValues_AECLearningDeliveryPeriod");
            });

            modelBuilder.Entity<AecLearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("PK__AEC_Lear__FED24A87CC827287");

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
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period10)
                    .HasColumnName("Period_10")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period11)
                    .HasColumnName("Period_11")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period12)
                    .HasColumnName("Period_12")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period2)
                    .HasColumnName("Period_2")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period3)
                    .HasColumnName("Period_3")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period4)
                    .HasColumnName("Period_4")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period5)
                    .HasColumnName("Period_5")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period6)
                    .HasColumnName("Period_6")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period7)
                    .HasColumnName("Period_7")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period8)
                    .HasColumnName("Period_8")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.Period9)
                    .HasColumnName("Period_9")
                    .HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AecLearningDelivery)
                    .WithMany(p => p.AecLearningDeliveryPeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AECLearningDeliveryPeriodisedValues_AECLearningDeliveryPeriod");
            });

            modelBuilder.Entity<AlbGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__ALB_glob__50F26B713CE83BC3");

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

            modelBuilder.Entity<AlbLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__ALB_Lear__2770A727B4C91AD3");

                entity.ToTable("ALB_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.AlbLearners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALBLearner_ALBglobal");
            });

            modelBuilder.Entity<AlbLearnerPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Period })
                    .HasName("PK__ALB_Lear__7066D5F5CE2686D7");

                entity.ToTable("ALB_Learner_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AlbseqNum).HasColumnName("ALBSeqNum");

                entity.HasOne(d => d.AlbLearner)
                    .WithMany(p => p.AlbLearnerPeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALBLearnerPeriod_ALBLearner");
            });

            modelBuilder.Entity<AlbLearnerPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AttributeName })
                    .HasName("PK__ALB_Lear__08C04CF8DA2C05EF");

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

                entity.HasOne(d => d.AlbLearner)
                    .WithMany(p => p.AlbLearnerPeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALBLearnerPeriodisedValues_ALBLearner");
            });

            modelBuilder.Entity<AlbLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__ALB_Lear__0C29443A3068AAC2");

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

                entity.Property(e => e.AreaCostInstalment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LiabilityDate).HasColumnType("date");

                entity.Property(e => e.WeightedRate).HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AlbLearner)
                    .WithMany(p => p.AlbLearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALBLearningDelivery_ALBLearner");
            });

            modelBuilder.Entity<AlbLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period })
                    .HasName("PK__ALB_Lear__295823179A38001D");

                entity.ToTable("ALB_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Albcode).HasColumnName("ALBCode");

                entity.Property(e => e.AlbsupportPayment)
                    .HasColumnName("ALBSupportPayment")
                    .HasColumnType("decimal(12, 5)");

                entity.Property(e => e.AreaUpliftBalPayment).HasColumnType("decimal(12, 5)");

                entity.Property(e => e.AreaUpliftOnProgPayment).HasColumnType("decimal(12, 5)");

                entity.HasOne(d => d.AlbLearningDelivery)
                    .WithMany(p => p.AlbLearningDeliveryPeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALBLearningDeliveryPeriod_ALBLearningDelivery");
            });

            modelBuilder.Entity<AlbLearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("PK__ALB_Lear__FED24A8735DEEDE2");

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

                entity.HasOne(d => d.AlbLearningDelivery)
                    .WithMany(p => p.AlbLearningDeliveryPeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALBLearningDeliveryPeriodisedValues_ALBLearningDelivery");
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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__DV_Learn__2770A7276DFFD0B9");

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

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.DvLearners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DVLearner_DVglobal");
            });

            modelBuilder.Entity<DvLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__DV_Learn__0C29443A64B9CD22");

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

                entity.HasOne(d => d.DvLearner)
                    .WithMany(p => p.DvLearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DVLearningDelivery_DVLearner");
            });

            modelBuilder.Entity<EsfDpoutcome>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.OutCode, e.OutType, e.OutStartDate })
                    .HasName("PK__ESF_DPOu__1D621D29CEB5AD58");

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

            modelBuilder.Entity<EsfLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__ESF_Lear__2770A7279D0807E9");

                entity.ToTable("ESF_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.EsfLearners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESFLearner_ESFglobal");
            });

            modelBuilder.Entity<EsfLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__ESF_Lear__0C29443A716A3CA1");

                entity.ToTable("ESF_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AdjustedAreaCostFactor).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.AdjustedPremiumFactor).HasColumnType("decimal(10, 5)");

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

                entity.HasOne(d => d.EsfLearner)
                    .WithMany(p => p.EsfLearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESFLearningDelivery_ESFLearner");
            });

            modelBuilder.Entity<EsfLearningDeliveryDeliverable>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode })
                    .HasName("PK__ESF_Lear__C21F732ACE3FFFEA");

                entity.ToTable("ESF_LearningDeliveryDeliverable", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableUnitCost).HasColumnType("decimal(15, 5)");

                entity.HasOne(d => d.EsfLearningDelivery)
                    .WithMany(p => p.EsfLearningDeliveryDeliverables)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESFLearningDeliveryDeliverable_ESFLearningDelivery");
            });

            modelBuilder.Entity<EsfLearningDeliveryDeliverablePeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode, e.Period })
                    .HasName("PK__ESF_Lear__104865581DEEFBDA");

                entity.ToTable("ESF_LearningDeliveryDeliverable_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverableCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AchievementEarnings).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.AdditionalProgCostEarnings).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.ProgressionEarnings).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.StartEarnings).HasColumnType("decimal(15, 5)");

                entity.HasOne(d => d.EsfLearningDelivery)
                    .WithMany(p => p.EsfLearningDeliveryDeliverablePeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESFLearningDeliveryDeliverablePeriod_ESFLearningDelivery");
            });

            modelBuilder.Entity<EsfLearningDeliveryDeliverablePeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.DeliverableCode, e.AttributeName })
                    .HasName("PK__ESF_Lear__1D30C3C123D04188");

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

                entity.HasOne(d => d.EsfLearningDeliveryDeliverable)
                    .WithMany(p => p.EsfLearningDeliveryDeliverablePeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber, d.DeliverableCode })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESFLearningDeliveryDeliverablePeriodisedValues_ESFLearningDelivery");
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
                entity.HasKey(e => new { e.Ukprn, e.AimSeqNumber, e.LearnRefNumber });

                entity.ToTable("ESFVAL_ValidationError", "Rulebase");

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

            modelBuilder.Entity<Fm25Fm35Global>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__FM25_FM3__50F26B715A8E054C");

                entity.ToTable("FM25_FM35_global", "Rulebase");

                entity.Property(e => e.Ukprn)
                    .HasColumnName("UKPRN")
                    .ValueGeneratedNever();

                entity.Property(e => e.RulebaseVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fm25Fm35LearnerPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.Period })
                    .HasName("PK__FM25_FM3__7066D5F53B329480");

                entity.ToTable("FM25_FM35_Learner_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LnrOnProgPay).HasColumnType("decimal(15, 5)");

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.Fm25Fm35LearnerPeriods)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM25FM35LearnerPeriod_FM25FM35global");

                entity.HasOne(d => d.Fm25Learner)
                    .WithMany(p => p.Fm25Fm35LearnerPeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM25_FM35_Learner_Period_FM25_Learner");
            });

            modelBuilder.Entity<Fm25Fm35LearnerPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AttributeName })
                    .HasName("PK__FM25_FM3__08C04CF8DBAE0372");

                entity.ToTable("FM25_FM35_Learner_PeriodisedValues", "Rulebase");

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

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.Fm25Fm35LearnerPeriodisedValues)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM25FM35LearnerPeriodisedValues_FM25FM35global");

                entity.HasOne(d => d.Fm25Learner)
                    .WithMany(p => p.Fm25Fm35LearnerPeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM25_FM35_Learner_PeriodisedValues_FM25_Learner");
            });

            modelBuilder.Entity<Fm25Global>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__FM25_glo__50F26B71385FA288");

                entity.ToTable("FM25_global", "Rulebase");

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

            modelBuilder.Entity<Fm25Learner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__FM25_Lea__2770A72714126DCA");

                entity.ToTable("FM25_Learner", "Rulebase");

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

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.Fm25Learners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM25Learner_FM25global");
            });

            modelBuilder.Entity<Fm35Global>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK_FM35_Global");

                entity.ToTable("FM35_global", "Rulebase");

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

            modelBuilder.Entity<Fm35Learner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__FM35_Lea__2770A7277B3A29BF");

                entity.ToTable("FM35_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.Fm35Learners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM35Learner_FM35global");
            });

            modelBuilder.Entity<Fm35LearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber });

                entity.ToTable("FM35_LearningDelivery", "Rulebase");

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

                entity.Property(e => e.DisUpFactAdj).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EmpOutcomePctHeldBackTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.EmpOutcomePctPreTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Esol).HasColumnName("ESOL");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LargeEmployerFm35fctr)
                    .HasColumnName("LargeEmployerFM35Fctr")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LargeEmployerId).HasColumnName("LargeEmployerID");

                entity.Property(e => e.LargeEmployerStatusDate).HasColumnType("date");

                entity.Property(e => e.LearnDelFundOrgCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LtrcupliftFctr)
                    .HasColumnName("LTRCUpliftFctr")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.NonGovCont).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Olasscustody).HasColumnName("OLASSCustody");

                entity.Property(e => e.OnProgPayPctPreTrans).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PlannedTotalDaysIl).HasColumnName("PlannedTotalDaysIL");

                entity.Property(e => e.PlannedTotalDaysIlpreTrans).HasColumnName("PlannedTotalDaysILPreTrans");

                entity.Property(e => e.PropFundRemain).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.PropFundRemainAch).HasColumnType("decimal(10, 5)");

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

                entity.HasOne(d => d.Fm35Learner)
                    .WithMany(p => p.Fm35LearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM35LearningDelivery_FM35Learner");
            });

            modelBuilder.Entity<Fm35LearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period });

                entity.ToTable("FM35_LearningDelivery_Period", "Rulebase");

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

                entity.HasOne(d => d.Fm35LearningDelivery)
                    .WithMany(p => p.Fm35LearningDeliveryPeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM35LearningDeliveryPeriod_FM35LearningDelivery");
            });

            modelBuilder.Entity<Fm35LearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName });

                entity.ToTable("FM35_LearningDelivery_PeriodisedValues", "Rulebase");

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

                entity.HasOne(d => d.Fm35LearningDelivery)
                    .WithMany(p => p.Fm35LearningDeliveryPeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FM35LearningDeliveryPeriodisedValues_FM35LearningDelivery");
            });

            modelBuilder.Entity<ProcessingData>(entity =>
            {
                entity.ToTable("ProcessingData");

                entity.Property(e => e.ExecutionTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FileDetailsId).HasColumnName("FileDetailsID");

                entity.Property(e => e.ProcessingStep)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<TblGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__TBL_glob__50F26B714BDA8B80");

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

            modelBuilder.Entity<TblLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__TBL_Lear__2770A72737F5C886");

                entity.ToTable("TBL_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.TblLearners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLLearner_TBLglobal");
            });

            modelBuilder.Entity<TblLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber })
                    .HasName("PK__TBL_Lear__0C29443AC283628D");

                entity.ToTable("TBL_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchApplicDate).HasColumnType("date");

                entity.Property(e => e.AchPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.AchievementApplicVal).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.ActualDaysIl).HasColumnName("ActualDaysIL");

                entity.Property(e => e.AdjProgStartDate).HasColumnType("date");

                entity.Property(e => e.AdjStartDate).HasColumnType("date");

                entity.Property(e => e.ApplicFundValDate).HasColumnType("date");

                entity.Property(e => e.CombinedAdjProp).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.CoreGovContPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.CoreGovContUncapped).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.FundLine)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LearnDelDaysIl).HasColumnName("LearnDelDaysIL");

                entity.Property(e => e.LearnDelStandardAccDaysIl).HasColumnName("LearnDelStandardAccDaysIL");

                entity.Property(e => e.LearnDelStandardPrevAccDaysIl).HasColumnName("LearnDelStandardPrevAccDaysIL");

                entity.Property(e => e.LearnDelStandardTotalDaysIl).HasColumnName("LearnDelStandardTotalDaysIL");

                entity.Property(e => e.LearnSuppFundCash).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngAimValue).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngBalPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngLsffundStart).HasColumnName("MathEngLSFFundStart");

                entity.Property(e => e.MathEngLsfthresholdDays).HasColumnName("MathEngLSFThresholdDays");

                entity.Property(e => e.MathEngOnProgPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.PlannedTotalDaysIl).HasColumnName("PlannedTotalDaysIL");

                entity.Property(e => e.ProgStandardStartDate).HasColumnType("date");

                entity.Property(e => e.SmallBusApplicVal).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.SmallBusPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.SmallBusThresholdDate).HasColumnType("date");

                entity.Property(e => e.YoungAppApplicVal).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppFirstPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppFirstThresholdDate).HasColumnType("date");

                entity.Property(e => e.YoungAppPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppSecondPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppSecondThresholdDate).HasColumnType("date");

                entity.HasOne(d => d.TblLearner)
                    .WithMany(p => p.TblLearningDeliveries)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLLearningDelivery_TBLLearner");
            });

            modelBuilder.Entity<TblLearningDeliveryPeriod>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.Period })
                    .HasName("PK__TBL_Lear__29582317B27C2464");

                entity.ToTable("TBL_LearningDelivery_Period", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AchPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.CoreGovContPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.CoreGovContUncapped).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.LearnSuppFundCash).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngBalPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngBalPct).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngOnProgPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.MathEngOnProgPct).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.SmallBusPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppFirstPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppPayment).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.YoungAppSecondPayment).HasColumnType("decimal(15, 5)");

                entity.HasOne(d => d.TblLearningDelivery)
                    .WithMany(p => p.TblLearningDeliveryPeriods)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLLearningDeliveryPeriod_TBLLearningDelivery");
            });

            modelBuilder.Entity<TblLearningDeliveryPeriodisedValue>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber, e.AimSeqNumber, e.AttributeName })
                    .HasName("PK__TBL_Lear__FED24A8787A220B3");

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

                entity.HasOne(d => d.TblLearningDelivery)
                    .WithMany(p => p.TblLearningDeliveryPeriodisedValues)
                    .HasForeignKey(d => new { d.Ukprn, d.LearnRefNumber, d.AimSeqNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLLearningDeliveryPeriodisedValues_TBLLearningDelivery");
            });

            modelBuilder.Entity<ValGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__VAL_glob__50F26B719DF9C9D2");

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

            modelBuilder.Entity<ValLearner>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber })
                    .HasName("PK__VAL_Lear__2770A7278D56775C");

                entity.ToTable("VAL_Learner", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");

                entity.Property(e => e.LearnRefNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.UkprnNavigation)
                    .WithMany(p => p.ValLearners)
                    .HasForeignKey(d => d.Ukprn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VALLearner_VALglobal");
            });

            modelBuilder.Entity<ValLearningDelivery>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.AimSeqNumber })
                    .HasName("PK__VAL_Lear__E56C5AA31DCE3384");

                entity.ToTable("VAL_LearningDelivery", "Rulebase");

                entity.Property(e => e.Ukprn).HasColumnName("UKPRN");
            });

            modelBuilder.Entity<ValValidationError>(entity =>
            {
                entity.HasKey(e => new { e.Ukprn, e.AimSeqNumber, e.LearnRefNumber });

                entity.ToTable("VAL_ValidationError", "Rulebase");

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

            modelBuilder.Entity<ValdpGlobal>(entity =>
            {
                entity.HasKey(e => e.Ukprn)
                    .HasName("PK__VALDP_gl__50F26B71A94ADF4D");

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
                entity.HasKey(e => new { e.Ukprn, e.LearnRefNumber });

                entity.ToTable("VALDP_ValidationError", "Rulebase");

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

            modelBuilder.Entity<ValidationError>(entity =>
            {
                entity.ToTable("ValidationError");

                entity.HasIndex(e => e.Ukprn)
                    .HasName("IX_ValidationError");

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
