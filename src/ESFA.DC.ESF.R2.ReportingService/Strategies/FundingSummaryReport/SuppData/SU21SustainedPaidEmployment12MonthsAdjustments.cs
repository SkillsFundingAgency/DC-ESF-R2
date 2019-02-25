using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class SU21SustainedPaidEmployment12MonthsAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public SU21SustainedPaidEmployment12MonthsAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "SU21";
    }
}