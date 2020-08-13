using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface
{
    public interface IFundingSummaryReportModelBuilder
    {
        Task<ICollection<IFundingSummaryReportTab>> Build(IEsfJobContext esfJobContext, CancellationToken cancellationToken);
    }
}
