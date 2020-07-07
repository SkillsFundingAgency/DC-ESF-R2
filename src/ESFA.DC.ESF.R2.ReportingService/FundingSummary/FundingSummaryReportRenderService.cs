using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportRenderService : IRenderService
    {
        public Task Render<T>(T model, Worksheet workSheet)
        {
            return Task.CompletedTask;
        }
    }
}
