using Aspose.Cells;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary
{
    public interface IRenderService
    {
        Task Render<T>(T model, Worksheet workSheet);
    }
}
