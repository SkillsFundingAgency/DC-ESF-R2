using System;
using ESFA.DC.ILR.DataService.ILR1819EF.Rulebase;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Interface
{
    public interface IILR1819RuleBaseContext : IDisposable
    {
        DbSet<AecApprenticeshipPriceEpisode> AecApprenticeshipPriceEpisodes { get; set; }

        DbSet<AecApprenticeshipPriceEpisodePeriod> AecApprenticeshipPriceEpisodePeriods { get; set; }

        DbSet<AecApprenticeshipPriceEpisodePeriodisedValue> AecApprenticeshipPriceEpisodePeriodisedValues { get; set; }

        DbSet<AecGlobal> AecGlobals { get; set; }

        DbSet<AecHistoricEarningOutput> AecHistoricEarningOutputs { get; set; }

        DbSet<AecLearner> AecLearners { get; set; }

        DbSet<AecLearningDelivery> AecLearningDeliveries { get; set; }

        DbSet<AecLearningDeliveryPeriod> AecLearningDeliveryPeriods { get; set; }

        DbSet<AecLearningDeliveryPeriodisedTextValue> AecLearningDeliveryPeriodisedTextValues { get; set; }

        DbSet<AecLearningDeliveryPeriodisedValue> AecLearningDeliveryPeriodisedValues { get; set; }

        DbSet<AlbGlobal> AlbGlobals { get; set; }

        DbSet<AlbLearner> AlbLearners { get; set; }

        DbSet<AlbLearnerPeriod> AlbLearnerPeriods { get; set; }

        DbSet<AlbLearnerPeriodisedValue> AlbLearnerPeriodisedValues { get; set; }

        DbSet<AlbLearningDelivery> AlbLearningDeliveries { get; set; }

        DbSet<AlbLearningDeliveryPeriod> AlbLearningDeliveryPeriods { get; set; }

        DbSet<AlbLearningDeliveryPeriodisedValue> AlbLearningDeliveryPeriodisedValues { get; set; }

        DbSet<DvGlobal> DvGlobals { get; set; }

        DbSet<DvLearner> DvLearners { get; set; }

        DbSet<DvLearningDelivery> DvLearningDeliveries { get; set; }

        DbSet<EsfDpoutcome> EsfDpoutcomes { get; set; }

        DbSet<EsfGlobal> EsfGlobals { get; set; }

        DbSet<EsfLearner> EsfLearners { get; set; }

        DbSet<EsfLearningDelivery> EsfLearningDeliveries { get; set; }

        DbSet<EsfLearningDeliveryDeliverable> EsfLearningDeliveryDeliverables { get; set; }

        DbSet<EsfLearningDeliveryDeliverablePeriod> EsfLearningDeliveryDeliverablePeriods { get; set; }

        DbSet<EsfLearningDeliveryDeliverablePeriodisedValue> EsfLearningDeliveryDeliverablePeriodisedValues { get; set; }

        DbSet<EsfvalGlobal> EsfvalGlobals { get; set; }

        DbSet<EsfvalValidationError> EsfvalValidationErrors { get; set; }

        DbSet<FileDetail> FileDetails { get; set; }

        DbSet<Fm25Fm35Global> Fm25Fm35Globals { get; set; }

        DbSet<Fm25Fm35LearnerPeriod> Fm25Fm35LearnerPeriods { get; set; }

        DbSet<Fm25Fm35LearnerPeriodisedValue> Fm25Fm35LearnerPeriodisedValues { get; set; }

        DbSet<Fm25Global> Fm25Globals { get; set; }

        DbSet<Fm25Learner> Fm25Learners { get; set; }

        DbSet<Fm35Global> Fm35Globals { get; set; }

        DbSet<Fm35Learner> Fm35Learners { get; set; }

        DbSet<Fm35LearningDelivery> Fm35LearningDeliveries { get; set; }

        DbSet<Fm35LearningDeliveryPeriod> Fm35LearningDeliveryPeriods { get; set; }

        DbSet<Fm35LearningDeliveryPeriodisedValue> Fm35LearningDeliveryPeriodisedValues { get; set; }

        DbSet<ProcessingData> ProcessingDatas { get; set; }

        DbSet<TblGlobal> TblGlobals { get; set; }

        DbSet<TblLearner> TblLearners { get; set; }

        DbSet<TblLearningDelivery> TblLearningDeliveries { get; set; }

        DbSet<TblLearningDeliveryPeriod> TblLearningDeliveryPeriods { get; set; }

        DbSet<TblLearningDeliveryPeriodisedValue> TblLearningDeliveryPeriodisedValues { get; set; }

        DbSet<ValGlobal> ValGlobals { get; set; }

        DbSet<ValLearner> ValLearners { get; set; }

        DbSet<ValLearningDelivery> ValLearningDeliveries { get; set; }

        DbSet<ValValidationError> ValValidationErrors { get; set; }

        DbSet<ValdpGlobal> ValdpGlobals { get; set; }

        DbSet<ValdpValidationError> ValdpValidationErrors { get; set; }

        DbSet<ValfdValidationError> ValfdValidationErrors { get; set; }

        DbSet<ValidationError> ValidationErrors { get; set; }

        DbSet<VersionInfo> VersionInfos { get; set; }
    }
}