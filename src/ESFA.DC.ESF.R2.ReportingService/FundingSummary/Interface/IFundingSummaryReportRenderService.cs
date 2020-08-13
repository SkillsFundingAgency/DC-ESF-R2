using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface
{
    public interface IFundingSummaryReportRenderService
    {
        Task Render(IFundingSummaryReportTab model, Worksheet workSheet);
    }
}
