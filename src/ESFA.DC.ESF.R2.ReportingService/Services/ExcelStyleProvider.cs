﻿using System.Collections.Generic;
using System.Drawing;
using Aspose.Cells;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models.Styling;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public sealed class ExcelStyleProvider : IExcelStyleProvider
    {
        public CellStyle[] GetFundingSummaryStyles(Workbook workbook)
        {
            List<CellStyle> cellStyles = new List<CellStyle>();

            Style mainTitleStyle = workbook.CreateStyle();          // 1
            mainTitleStyle.ForegroundColor = Color.FromArgb(191, 191, 191);
            mainTitleStyle.Pattern = BackgroundType.Solid;
            mainTitleStyle.Font.Size = 13;
            mainTitleStyle.Font.IsBold = true;
            mainTitleStyle.Font.Name = Constants.FundingSummaryReportFont;

            var mainTitleStyleFlag = new StyleFlag
            {
                CellShading = true,
                Font = true
            };
            cellStyles.Add(new CellStyle(mainTitleStyle, mainTitleStyleFlag));

            Style notUsedStyle = workbook.CreateStyle();            // 2
            notUsedStyle.ForegroundColor = Color.FromArgb(216, 216, 216);
            notUsedStyle.Pattern = BackgroundType.Solid;
            notUsedStyle.Font.Size = 12;
            notUsedStyle.Font.IsBold = true;
            notUsedStyle.Font.Name = Constants.FundingSummaryReportFont;

            var notUsedStyleFlag = new StyleFlag
            {
                CellShading = true,
                Font = true
            };
            cellStyles.Add(new CellStyle(notUsedStyle, notUsedStyleFlag));

            Style sectionStyle = workbook.CreateStyle();            // 3
            sectionStyle.ForegroundColor = Color.FromArgb(242, 242, 242);
            sectionStyle.Pattern = BackgroundType.Solid;
            sectionStyle.Font.Size = 11;
            sectionStyle.Font.IsBold = true;
            sectionStyle.Font.Name = Constants.FundingSummaryReportFont;
            // sectionStyle.SetCustom(Constants.FundingSummaryReportNumberFormat, true);
            ApplyBorderToStyle(sectionStyle);

            var titleStyleFlag = new StyleFlag
            {
                CellShading = true,
                Font = true,
                Borders = true,
                NumberFormat = true
            };
            cellStyles.Add(new CellStyle(sectionStyle, titleStyleFlag));

            Style subTotalStyle = workbook.CreateStyle();           // 4
            subTotalStyle.Font.Size = 10;
            subTotalStyle.Font.IsBold = true;
            subTotalStyle.Font.Name = Constants.FundingSummaryReportFont;
            // subTotalStyle.SetCustom(Constants.FundingSummaryReportNumberFormat, true);
            ApplyBorderToStyle(subTotalStyle);

            var subTotalStyleFlag = new StyleFlag
            {
                Font = true,
                Borders = true,
                NumberFormat = true
            };
            cellStyles.Add(new CellStyle(subTotalStyle, subTotalStyleFlag));

            Style dataRowStyle = workbook.CreateStyle();            // 5
            dataRowStyle.Font.Size = 10;
            dataRowStyle.Font.Name = Constants.FundingSummaryReportFont;
            // dataRowStyle.SetCustom(Constants.FundingSummaryReportNumberFormat, true);
            ApplyBorderToStyle(dataRowStyle);

            var dataRowStyleFlag = new StyleFlag
            {
                Font = true,
                Borders = true,
                NumberFormat = true
            };
            cellStyles.Add(new CellStyle(dataRowStyle, dataRowStyleFlag));

            Style currentYearStyle = workbook.CreateStyle();        // 6
            currentYearStyle.Font.Color = Color.Red;
            currentYearStyle.Font.Size = 10;
            currentYearStyle.Font.IsBold = true;
            currentYearStyle.Font.Name = Constants.FundingSummaryReportFont;
            // currentYearStyle.SetCustom(Constants.FundingSummaryReportNumberFormat, true);
            ApplyBorderToStyle(currentYearStyle);

            var currentYearStyleFlag = new StyleFlag
            {
                Font = true,
                Borders = true,
                NumberFormat = true
            };
            cellStyles.Add(new CellStyle(currentYearStyle, currentYearStyleFlag));

            Style grandTotalStyle = workbook.CreateStyle();         // 7
            grandTotalStyle.ForegroundColor = Color.FromArgb(191, 191, 191);
            grandTotalStyle.Pattern = BackgroundType.Solid;
            grandTotalStyle.Font.Size = 13;
            grandTotalStyle.Font.IsBold = true;
            grandTotalStyle.Font.Name = Constants.FundingSummaryReportFont;
            // grandTotalStyle.SetCustom(Constants.FundingSummaryReportNumberFormat, true);
            ApplyBorderToStyle(grandTotalStyle);

            var grandTotalStyleFlag = new StyleFlag
            {
                CellShading = true,
                Borders = true,
                Font = true,
                NumberFormat = true
            };
            cellStyles.Add(new CellStyle(grandTotalStyle, grandTotalStyleFlag));

            Style headerFooterStyle = workbook.CreateStyle();       // 8
            headerFooterStyle.Font.Size = 10;
            headerFooterStyle.Font.IsBold = true;
            headerFooterStyle.Font.Name = Constants.FundingSummaryReportFont;

            var headerFooterStyleFlag = new StyleFlag
            {
                Font = true
            };
            cellStyles.Add(new CellStyle(headerFooterStyle, headerFooterStyleFlag));

            Style headerFooterCurrentYearStyle = workbook.CreateStyle();       // 9
            headerFooterCurrentYearStyle.Font.Color = Color.Red;
            headerFooterCurrentYearStyle.Font.Size = 10;
            headerFooterCurrentYearStyle.Font.IsBold = true;
            headerFooterCurrentYearStyle.Font.Name = Constants.FundingSummaryReportFont;

            var headerFooterCurrentYearStyleFlag = new StyleFlag
            {
                Font = true
            };
            cellStyles.Add(new CellStyle(headerFooterCurrentYearStyle, headerFooterCurrentYearStyleFlag));

            return cellStyles.ToArray();
        }

        public CellStyle GetCellStyle(CellStyle[] cellStyles, int index)
        {
            return index == -1 ? null : cellStyles[index];
        }

        private static void ApplyBorderToStyle(Style style)
        {
            Border topBorder = style.Borders[BorderType.TopBorder];
            topBorder.LineStyle = CellBorderType.Thin;
            topBorder.Color = Color.Black;

            Border bottomBorder = style.Borders[BorderType.BottomBorder];
            bottomBorder.LineStyle = CellBorderType.Thin;
            bottomBorder.Color = Color.Black;

            Border leftBorder = style.Borders[BorderType.LeftBorder];
            leftBorder.LineStyle = CellBorderType.Thin;
            leftBorder.Color = Color.Black;

            Border rightBorder = style.Borders[BorderType.RightBorder];
            rightBorder.LineStyle = CellBorderType.Thin;
            rightBorder.Color = Color.Black;
        }

        private static void ApplyHardBorderToStyle(Style style)
        {
            Border topBorder = style.Borders[BorderType.TopBorder];
            topBorder.LineStyle = CellBorderType.Thin;
            topBorder.Color = Color.Black;

            Border bottomBorder = style.Borders[BorderType.BottomBorder];
            bottomBorder.LineStyle = CellBorderType.Thin;
            bottomBorder.Color = Color.Black;

            Border leftBorder = style.Borders[BorderType.LeftBorder];
            leftBorder.LineStyle = CellBorderType.Thin;
            leftBorder.Color = Color.Black;

            Border rightBorder = style.Borders[BorderType.RightBorder];
            rightBorder.LineStyle = CellBorderType.Thick;
            rightBorder.Color = Color.Black;
        }
    }
}