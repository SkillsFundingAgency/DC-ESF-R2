using Aspose.Cells;
using ESFA.DC.ESF.R2.Models.Styling;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IExcelStyleProvider
    {
        CellStyle[] GetFundingSummaryStyles(Workbook workbook);

        CellStyle GetCellStyle(CellStyle[] cellStyles, int index);
    }
}
