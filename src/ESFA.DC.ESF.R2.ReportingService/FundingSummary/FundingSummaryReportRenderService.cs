using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportRenderService : IFundingSummaryReportRenderService
    {
        private const int StartColumn = 0;
        private const int DefaultBodyColumnCount = 7;

        private readonly Style _defaultStyle;
        private readonly Style _futureMonthStyle;
        private readonly Style _reportTitleStyle;
        private readonly Style _deliverableGroupStyle;
        private readonly Style _deliverableSubGroupStyle;
        private readonly Style _deliverableLineStyle;
        private readonly Style _headerAndFooterStyle;
        private readonly Style _reportMonthlyTotalsStyle;

        private readonly StyleFlag _styleFlag = new StyleFlag()
        {
            All = true,
        };

        private readonly StyleFlag _italicStyleFlag = new StyleFlag()
        {
            FontItalic = true
        };

        private readonly StyleFlag _horizontalAlignStyleFlag = new StyleFlag()
        {
            HorizontalAlignment = true,
        };

        public FundingSummaryReportRenderService()
        {
            var cellsFactory = new CellsFactory();

            _defaultStyle = cellsFactory.CreateStyle();
            _futureMonthStyle = cellsFactory.CreateStyle();
            _reportTitleStyle = cellsFactory.CreateStyle();
            _deliverableGroupStyle = cellsFactory.CreateStyle();
            _deliverableSubGroupStyle = cellsFactory.CreateStyle();
            _deliverableLineStyle = cellsFactory.CreateStyle();
            _headerAndFooterStyle = cellsFactory.CreateStyle();
            _reportMonthlyTotalsStyle = cellsFactory.CreateStyle();

            ConfigureStyles();
        }

        protected int ColumnCount { get; set; }

        public async Task Render(IFundingSummaryReportTab fundingSummaryReportTab, Worksheet worksheet)
        {
            ColumnCount = CalculateColumns(fundingSummaryReportTab.Body.Count());

            worksheet.Workbook.DefaultStyle = _defaultStyle;
            worksheet.Cells.StandardWidth = 20;
            worksheet.Cells.Columns[0].Width = 65;

            RenderHeader(worksheet, NextMaxRow(worksheet), fundingSummaryReportTab.Header);

            RenderBody(worksheet, fundingSummaryReportTab);

            RenderFooter(worksheet, NextMaxRow(worksheet) + 1, fundingSummaryReportTab.Footer);

            AddImageToReport(worksheet);

            UpdateColumnProperties(worksheet);
        }

        private void UpdateColumnProperties(Worksheet worksheet)
        {
            worksheet.AutoFitColumn(0);

            var firstColumnAlignStyle = worksheet.Cells.Columns[0].Style;
            firstColumnAlignStyle.HorizontalAlignment = TextAlignmentType.Left;
            worksheet.Cells.Columns[0].ApplyStyle(firstColumnAlignStyle, _horizontalAlignStyleFlag);
        }

        private int CalculateColumns(int bodyCount)
        {
            // number of body elements * 13 (12 months plus total) - 6 (less 6 for base April to July plus Totals)
            return (bodyCount * 13) - 6;
        }

        private Worksheet RenderHeader(Worksheet worksheet, int row, FundingSummaryReportHeaderModel header)
        {
            worksheet.Cells.ImportTwoDimensionArray(
              new object[,]
              {
                  { "Provider Name : ", header.ProviderName },
                  { "UKPRN : ", header.Ukprn },
                  { "Contract Reference Number : ", header.ContractReferenceNumber },
                  { "Round 2 Supplementary Data File : ", header.SupplementaryDataFile },
                  { "Last Round 2 Supplementary Data File Update : ", header.LastSupplementaryDataFileUpdate },
                  { "Security Classification :", header.SecurityClassification },
              },
              row,
              0);

            var rowsToStyle = row + 6;

            while (row <= rowsToStyle)
            {
                ApplyStyleToRow(worksheet, row, _headerAndFooterStyle);

                row++;
            }

            var column = 3;
            var ilrHeaderRow = 1;

            worksheet.Cells.ImportObjectArray(
            new object[]
            {
                string.Empty,
                "ILR File : ",
                "Last ILR File Update : ",
                "File Preparation Date : ",
                string.Empty,
            },
            ilrHeaderRow,
            column,
            true);

            foreach (var ilrDetail in header.IlrFileDetails)
            {
                column = NextColumn(column);

                worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    ilrDetail.AcademicYear,
                    ilrDetail.IlrFile,
                    ilrDetail.LastIlrFileUpdate,
                    ilrDetail.FilePrepDate,
                    ilrDetail.MostRecent,
                },
                ilrHeaderRow,
                column,
                true);

                column = NextColumn(column);
            }

            return worksheet;
        }

        private Worksheet RenderFooter(Worksheet worksheet, int row, FundingSummaryReportFooterModel footer)
        {
            worksheet.Cells.ImportTwoDimensionArray(
                new object[,]
                {
                    { "Application Version : ", footer.ApplicationVersion },
                    { "LARS Data : ", footer.LarsData },
                    { "Postcode Data : ", footer.PostcodeData },
                    { "Organisation Data : ", footer.OrganisationData },
                    { "Report generated at : ", footer.ReportGeneratedAt },
                },
                row,
                0);

            var rowsToStyle = row + 5;

            while (row <= rowsToStyle)
            {
                ApplyStyleToRow(worksheet, row, _headerAndFooterStyle);

                row++;
            }

            return worksheet;
        }

        private Worksheet RenderBody(Worksheet worksheet, IFundingSummaryReportTab fundingSummaryReportTab)
        {
            var row = NextMaxRow(worksheet) + 1;

             // Title
            worksheet.Cells.ImportObjectArray(new object[] { fundingSummaryReportTab.Title }, row, 0, false);
            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            // Categories
            var baseYear = fundingSummaryReportTab.Body.Min(x => x.Year);
            var baseYearData = fundingSummaryReportTab.Body.Where(x => x.Year == baseYear).FirstOrDefault();
            var subsequentYearsData = fundingSummaryReportTab.Body.Where(x => x.Year > baseYear).OrderBy(x => x.Year).ToList();

            RenderDeliverables(worksheet, baseYearData, subsequentYearsData, row);

            return worksheet;
        }

        private Worksheet RenderDeliverables(
            Worksheet worksheet,
            FundingSummaryReportEarnings baseModel,
            ICollection<FundingSummaryReportEarnings> subsequentModels,
            int row)
        {
            int column;
            column = RenderBaseFundingYear(worksheet, baseModel, row);

            foreach (var model in subsequentModels)
            {
                column = RenderSubsequentFundingYear(worksheet, model, column, row);
            }

            column = NextMaxColumn(worksheet);
            RenderTotals(worksheet, baseModel, subsequentModels, column, row);

            column = NextMaxColumn(worksheet);
            RenderGrandTotals(worksheet, subsequentModels.Count() + 1, column, row);

            return worksheet;
        }

        private Worksheet RenderGrandTotals(Worksheet worksheet, int yearsCount, int column, int row)
        {
            var maxRow = worksheet.Cells.GetLastDataRow(column - 1);

            row = NextRowWithBreak(row);

            BuildCell(worksheet, FundingSummaryReportConstants.GrandTotalHeader, row, column);
            row = NextRow(row);

            int rowCounter = row;
            while (rowCounter <= maxRow)
            {
                var value = worksheet.Cells.GetCell(rowCounter, column - 1)?.Value;

                if (value == null)
                {
                    BuildCell(worksheet, value, rowCounter, column);
                }
                else if (value?.ToString().Contains("Subtotal") == true)
                {
                    BuildCell(worksheet, FundingSummaryReportConstants.GrandTotalHeader, rowCounter, column);
                }
                else
                {
                    var fundingValues = new List<object>
                    {
                        value
                    };

                    int years = 2;
                    while (years <= yearsCount)
                    {
                        fundingValues.Add(worksheet.Cells.GetCell(rowCounter, column - years)?.Value);
                        years++;
                    }

                    BuildCell(worksheet, fundingValues.Sum(x => decimal.Parse(x.ToString())), rowCounter, column);
                }

                rowCounter++;
            }

            BuildCell(worksheet, FundingSummaryReportConstants.NotApplicable, maxRow, column);

            return worksheet;
        }

        private int RenderBaseFundingYear(Worksheet worksheet, FundingSummaryReportEarnings fundingSummaryModel, int row)
        {
            foreach (var category in fundingSummaryModel.DeliverableCategories)
            {
                row = NextMaxRow(worksheet) + 1;

                BuildBaseHeaderRow(worksheet, category.GroupHeader, row);

                ApplyStyleToRow(worksheet, row, _deliverableGroupStyle);

                RenderBaseDeliverables(worksheet, category, row);
            }

            RenderBaseMonthlyTotals(worksheet, fundingSummaryModel, row);

            return LastColumnForRow(worksheet, row);
        }

        private Worksheet RenderBaseMonthlyTotals(Worksheet worksheet, FundingSummaryReportEarnings fundingSummaryModel, int row)
        {
            row = NextMaxRow(worksheet) + 1;

            BuildBaseRow(worksheet, fundingSummaryModel.MonthlyTotals, row);

            row = NextRow(row);

            BuildBaseRow(worksheet, fundingSummaryModel.CumulativeMonthlyTotals, row);

            return worksheet;
        }

        private Worksheet RenderBaseDeliverables(Worksheet worksheet, IDeliverableCategory category, int row)
        {
            foreach (var subCategory in category.DeliverableSubCategories)
            {
                if (subCategory.DisplayTitle)
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = NextMaxRow(worksheet);

                        BuildBaseRow(worksheet, value, row);
                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }

                    row = NextMaxRow(worksheet);

                    BuildBaseSubCatRow(worksheet, subCategory, row);
                    ApplyStyleToRow(worksheet, row, _deliverableSubGroupStyle);
                }
                else
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = NextMaxRow(worksheet);

                        BuildBaseRow(worksheet, value, row);

                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }
                }
            }

            row = NextMaxRow(worksheet);

            //totals
            BuildBaseRow(worksheet, category.Totals, row);

            ApplyStyleToRow(worksheet, row, _deliverableGroupStyle);

            return worksheet;
        }

        private int RenderSubsequentFundingYear(Worksheet worksheet, FundingSummaryReportEarnings fundingSummaryModel, int column, int row)
        {
            column = NextColumn(column);

            foreach (var category in fundingSummaryModel.DeliverableCategories)
            {
                row = NextRowWithBreak(row);

                BuildHeaderRow(worksheet, category.GroupHeader, row, column);

                row = RenderSubsequentDeliverables(worksheet, category, column, row);
            }

            RenderSubsequentMonthlyTotals(worksheet, fundingSummaryModel, column, row);

            return worksheet.Cells.MaxDataColumn;
        }

        private Worksheet RenderSubsequentMonthlyTotals(Worksheet worksheet, FundingSummaryReportEarnings fundingSummaryModel, int column, int row)
        {
            row = NextRowWithBreak(row);

            BuildRow(worksheet, fundingSummaryModel.MonthlyTotals, row, column);

            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            row = NextRow(row);

            BuildRow(worksheet, fundingSummaryModel.CumulativeMonthlyTotals, row, column);

            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            return worksheet;
        }

        private int RenderSubsequentDeliverables(Worksheet worksheet, IDeliverableCategory category, int column, int row)
        {
            foreach (var subCategory in category.DeliverableSubCategories)
            {
                if (subCategory.DisplayTitle)
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = NextRow(row);

                        BuildRow(worksheet, value, row, column);
                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }

                    row = NextRow(row);

                    BuildSubCatRow(worksheet, subCategory, row, column);
                    ApplyStyleToRow(worksheet, row, _deliverableSubGroupStyle);
                }
                else
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = NextRow(row);

                        BuildRow(worksheet, value, row, column);

                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }
                }
            }

            row = NextRow(row);

            //totals
            BuildRow(worksheet, category.Totals, row, column);

            return row;
        }

        private Worksheet RenderSubsequentDeliverableTotals(Worksheet worksheet, IDeliverableCategory category, int row)
        {
            var deliverableRow = NextMaxRow(worksheet);
            var column = NextMaxColumn(worksheet);

            BuildRow(worksheet, category.Totals, deliverableRow, column);

            return worksheet;
        }

        private Worksheet RenderTotals(
          Worksheet worksheet,
          FundingSummaryReportEarnings baseModel,
          ICollection<FundingSummaryReportEarnings> subsequentModels,
          int column,
          int row)
        {
            RenderColumnTotal(worksheet, baseModel, string.Concat(baseModel.AcademicYear, " Subtotal"), column, row);

            foreach (var model in subsequentModels)
            {
                column = NextMaxColumn(worksheet);

                RenderColumnTotal(worksheet, model, string.Concat(model.AcademicYear, " Subtotal"), column, row);
            }

            return worksheet;
        }

        private Worksheet RenderColumnTotal(Worksheet worksheet, FundingSummaryReportEarnings model, string header, int column, int row)
        {
            column = NextMaxColumn(worksheet);

            foreach (var category in model.DeliverableCategories)
            {
                row = NextRowWithBreak(row);

                BuildCell(worksheet, header, row, column);

                row = RenderDeliverableColumnTotals(worksheet, category, column, row);
            }

            row = NextRowWithBreak(row);

            BuildCell(worksheet, model.YearTotal, row, column);

            ApplyStyleToRow(worksheet, row, _reportMonthlyTotalsStyle);

            row = NextRow(row);

            BuildCell(worksheet, model.CumulativeYearTotal, row, column);

            ApplyStyleToRow(worksheet, row, _reportMonthlyTotalsStyle);

            return worksheet;
        }

        private int RenderDeliverableColumnTotals(Worksheet worksheet, IDeliverableCategory category, int column, int row)
        {
            foreach (var subCategory in category.DeliverableSubCategories)
            {
                if (subCategory.DisplayTitle)
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = NextRow(row);

                        BuildCell(worksheet, value.Total, row, column);

                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }

                    row = NextRow(row);

                    BuildCell(worksheet, subCategory.Totals.Total, row, column);

                    ApplyStyleToRow(worksheet, row, _deliverableSubGroupStyle);
                }
                else
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = NextRow(row);

                        BuildCell(worksheet, value.Total, row, column);
                    }
                }
            }

            row = NextRow(row);

            BuildCell(worksheet, category.Totals.Total, row, column);

            return row;
        }

        private Worksheet BuildBaseRow(Worksheet worksheet, IPeriodisedReportValue periodisedValues, int row)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    periodisedValues.Title,
                    periodisedValues.April,
                    periodisedValues.May,
                    periodisedValues.June,
                    periodisedValues.July,
                },
                row,
                0,
                false);

            return worksheet;
        }

        private Worksheet BuildBaseSubCatRow(Worksheet worksheet, IDeliverableSubCategory subCategory, int row)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    subCategory.SubCategoryTitle,
                    subCategory.Totals.April,
                    subCategory.Totals.May,
                    subCategory.Totals.June,
                    subCategory.Totals.July,
                },
                row,
                0,
                false);

            return worksheet;
        }

        private Worksheet BuildSubCatRow(Worksheet worksheet, IDeliverableSubCategory subCategory, int row, int column)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    subCategory.Totals.August,
                    subCategory.Totals.September,
                    subCategory.Totals.October,
                    subCategory.Totals.November,
                    subCategory.Totals.December,
                    subCategory.Totals.January,
                    subCategory.Totals.February,
                    subCategory.Totals.March,
                    subCategory.Totals.April,
                    subCategory.Totals.May,
                    subCategory.Totals.June,
                    subCategory.Totals.July,
                },
                row,
                column,
                false);

            return worksheet;
        }

        private Worksheet BuildRow(Worksheet worksheet, IPeriodisedReportValue periodisedValues, int row, int column)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    periodisedValues.August,
                    periodisedValues.September,
                    periodisedValues.October,
                    periodisedValues.November,
                    periodisedValues.December,
                    periodisedValues.January,
                    periodisedValues.February,
                    periodisedValues.March,
                    periodisedValues.April,
                    periodisedValues.May,
                    periodisedValues.June,
                    periodisedValues.July,
                },
                row,
                column,
                false);

            return worksheet;
        }

        private Worksheet BuildBaseHeaderRow(Worksheet worksheet, GroupHeader header, int row)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    header.Title,
                    header.HeaderApril,
                    header.HeaderMay,
                    header.HeaderJune,
                    header.HeaderJuly,
                },
                row,
                0,
                false);

            return worksheet;
        }

        private Worksheet BuildHeaderRow(Worksheet worksheet, GroupHeader header, int row, int column)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    header.HeaderAugust,
                    header.HeaderSeptember,
                    header.HeaderOctober,
                    header.HeaderNovember,
                    header.HeaderDecember,
                    header.HeaderJanuary,
                    header.HeaderFebruary,
                    header.HeaderMarch,
                    header.HeaderApril,
                    header.HeaderMay,
                    header.HeaderJune,
                    header.HeaderJuly,
                },
                row,
                column,
                false);

            return worksheet;
        }

        private Worksheet BuildCell(Worksheet worksheet, object value, int row, int column)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                   value
                },
                row,
                column,
                false);

            return worksheet;
        }

        private int NextRow(int row)
        {
            return row + 1;
        }

        private int NextRowWithBreak(int row)
        {
            return row + 2;
        }

        private int NextMaxRow(Worksheet worksheet)
        {
            return worksheet.Cells.MaxRow + 1;
        }

        private int NextMaxColumn(Worksheet worksheet)
        {
            return worksheet.Cells.MaxDataColumn + 1;
        }

        private int NextColumn(int column)
        {
            return column + 1;
        }

        private int LastColumnForRow(Worksheet worksheet, int row)
        {
            return worksheet.Cells.GetRow(row).LastDataCell.Column;
        }

        private void ApplyStyleToRow(Worksheet worksheet, int row, Style style)
        {
            worksheet.Cells.CreateRange(row, StartColumn, 1, ColumnCount).ApplyStyle(style, _styleFlag);
        }

        private void ApplyFutureMonthStyleToRow(Worksheet worksheet, int row, int currentPeriod)
        {
            var columnCount = 12 - currentPeriod;

            if (columnCount > 0)
            {
                worksheet.Cells.CreateRange(row, currentPeriod + 1, 1, 12 - currentPeriod).ApplyStyle(_futureMonthStyle, _italicStyleFlag);
            }
        }

        private void AddImageToReport(Worksheet worksheet)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string euFlag = assembly.GetManifestResourceNames().Single(str => str.EndsWith("ESF.png"));
            using (Stream stream = assembly.GetManifestResourceStream(euFlag))
            {
                worksheet.Pictures.Add(0, worksheet.Cells.MaxColumn - 1, stream);
                worksheet.Pictures[0].Top = 2;
            }
        }

        private void ConfigureStyles()
        {
            _defaultStyle.Font.Size = 10;
            _defaultStyle.Font.Name = "Arial";
            _defaultStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);

            _futureMonthStyle.Font.IsItalic = true;

            _reportTitleStyle.ForegroundColor = Color.FromArgb(191, 191, 191);
            _reportTitleStyle.Pattern = BackgroundType.Solid;
            _reportTitleStyle.Font.Size = 13;
            _reportTitleStyle.Font.IsBold = true;
            _reportTitleStyle.Font.Name = "Arial";
            _reportTitleStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);

            _deliverableGroupStyle.ForegroundColor = Color.FromArgb(242, 242, 242);
            _deliverableGroupStyle.Pattern = BackgroundType.Solid;
            _deliverableGroupStyle.Font.Size = 11;
            _deliverableGroupStyle.Font.IsBold = true;
            _deliverableGroupStyle.Font.Name = "Arial";
            _deliverableGroupStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);
            _deliverableGroupStyle.HorizontalAlignment = TextAlignmentType.Right;
            _deliverableGroupStyle.SetBorder(BorderType.TopBorder, CellBorderType.Medium, Color.Gray);
            _deliverableGroupStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Gray);
            _deliverableGroupStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Medium, Color.Gray);
            _deliverableGroupStyle.SetBorder(BorderType.RightBorder, CellBorderType.Medium, Color.Gray);

            _deliverableSubGroupStyle.Font.Size = 11;
            _deliverableSubGroupStyle.Font.IsBold = true;
            _deliverableSubGroupStyle.Font.Name = "Arial";
            _deliverableSubGroupStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);
            _deliverableSubGroupStyle.HorizontalAlignment = TextAlignmentType.Right;
            _deliverableSubGroupStyle.SetBorder(BorderType.TopBorder, CellBorderType.Medium, Color.Gray);
            _deliverableSubGroupStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Gray);
            _deliverableSubGroupStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Medium, Color.Gray);
            _deliverableSubGroupStyle.SetBorder(BorderType.RightBorder, CellBorderType.Medium, Color.Gray);

            _deliverableLineStyle.Font.Size = 10;
            _deliverableLineStyle.Font.Name = "Arial";
            _deliverableLineStyle.IsTextWrapped = true;
            _deliverableLineStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);
            _deliverableLineStyle.HorizontalAlignment = TextAlignmentType.Right;
            _deliverableLineStyle.SetBorder(BorderType.TopBorder, CellBorderType.Medium, Color.Gray);
            _deliverableLineStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Gray);
            _deliverableLineStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Medium, Color.Gray);
            _deliverableLineStyle.SetBorder(BorderType.RightBorder, CellBorderType.Medium, Color.Gray);

            _reportMonthlyTotalsStyle.ForegroundColor = Color.FromArgb(191, 191, 191);
            _reportMonthlyTotalsStyle.Pattern = BackgroundType.Solid;
            _reportMonthlyTotalsStyle.Font.Size = 13;
            _reportMonthlyTotalsStyle.Font.IsBold = true;
            _reportMonthlyTotalsStyle.Font.Name = "Arial";
            _reportMonthlyTotalsStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);
            _reportMonthlyTotalsStyle.SetBorder(BorderType.TopBorder, CellBorderType.Medium, Color.Gray);
            _reportMonthlyTotalsStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Gray);
            _reportMonthlyTotalsStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Medium, Color.Gray);
            _reportMonthlyTotalsStyle.SetBorder(BorderType.RightBorder, CellBorderType.Medium, Color.Gray);

            _headerAndFooterStyle.Font.Size = 10;
            _headerAndFooterStyle.Font.Name = "Arial";
            _headerAndFooterStyle.Font.IsBold = true;
        }
    }
}
