using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.ESF.R2.Interfaces;
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

        private const int FirstYearStartPeriod = 9;
        private const int StartPeriod = 1;
        private const int DataRowsStart = 10;

        private readonly Style _defaultStyle;
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
            _reportTitleStyle = cellsFactory.CreateStyle();
            _deliverableGroupStyle = cellsFactory.CreateStyle();
            _deliverableSubGroupStyle = cellsFactory.CreateStyle();
            _deliverableLineStyle = cellsFactory.CreateStyle();
            _headerAndFooterStyle = cellsFactory.CreateStyle();
            _reportMonthlyTotalsStyle = cellsFactory.CreateStyle();

            ConfigureStyles();
        }

        protected int ColumnCount { get; set; }

        public async Task RenderAsync(IEsfJobContext esfJobContext, IFundingSummaryReportTab fundingSummaryReportTab, Worksheet worksheet)
        {
            var currentPeriod = GetCurrentPeriod(esfJobContext.CurrentPeriod);

            ColumnCount = CalculateColumns(fundingSummaryReportTab.Body.Count());

            worksheet.Workbook.DefaultStyle = _defaultStyle;
            worksheet.Cells.StandardWidth = 20;
            worksheet.Cells.Columns[0].Width = 65;

            RenderHeader(worksheet, NextMaxRow(worksheet), fundingSummaryReportTab.Header);

            RenderBody(worksheet, currentPeriod, fundingSummaryReportTab);

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
                    { FundingSummaryReportConstants.HeaderProviderName, header.ProviderName },
                    { FundingSummaryReportConstants.HeaderUkprn, header.Ukprn },
                    { FundingSummaryReportConstants.HeaderContractNumber, header.ContractReferenceNumber },
                    { FundingSummaryReportConstants.HeaderEsfFileName, header.SupplementaryDataFile },
                    { FundingSummaryReportConstants.HeaderEsfFileUpdated, header.LastSupplementaryDataFileUpdate },
                    { FundingSummaryReportConstants.HeaderClassification, header.SecurityClassification },
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
                    FundingSummaryReportConstants.HeaderIlrFileName,
                    FundingSummaryReportConstants.HeaderIlrFileUpdated,
                    FundingSummaryReportConstants.HeaderIlrFilePrepDate,
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
                    { FundingSummaryReportConstants.FooterApplicationVersion, footer.ApplicationVersion },
                    { FundingSummaryReportConstants.FooterLARSData, footer.LarsData },
                    { FundingSummaryReportConstants.FooterPostcodeData, footer.PostcodeData },
                    { FundingSummaryReportConstants.FooterOrgData, footer.OrganisationData },
                    { FundingSummaryReportConstants.FooterReportGenerated, footer.ReportGeneratedAt },
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

        private Worksheet RenderBody(Worksheet worksheet, int currentPeriod, IFundingSummaryReportTab fundingSummaryReportTab)
        {
            var row = NextMaxRow(worksheet) + 1;

            // Title
            worksheet.Cells.ImportObjectArray(new object[] { fundingSummaryReportTab.Title }, row, 0, false);
            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            // Categories
            var models = fundingSummaryReportTab.Body.OrderBy(x => x.Year).ToArray();

            var lastDataRow = RenderDeliverables(worksheet, models, row);

            ApplyItalics(worksheet, models.Length, lastDataRow, currentPeriod);

            return worksheet;
        }

        private void ApplyItalics(Worksheet worksheet, int fundingYearCount, int lastDataRow, int currentPeriod)
        {
            var lastDataColumn = ColumnCount - 1 - fundingYearCount; // (Total Columns - GrandTotal Column - SubTotalColumns)

            var italicsColumnRangeEnd = worksheet.Cells.Columns[lastDataColumn].Index;
            var italicsColumnRangeStart = worksheet.Cells.Columns[lastDataColumn - (12 - currentPeriod)].Index;

            var italicsRowStart = worksheet.Cells.Rows[DataRowsStart].Index;

            for (int i = italicsColumnRangeStart; i < italicsColumnRangeEnd; i++)
            {
                var italicStyle = worksheet.Cells.Columns[i].Style;
                italicStyle.Font.IsItalic = true;
                worksheet.Cells.Columns[i].ApplyStyle(italicStyle, _italicStyleFlag);
            }
        }

        private int RenderDeliverables(
           Worksheet worksheet,
           FundingSummaryReportEarnings[] models,
           int row)
        {
            int column;
            column = RenderFundingYear(0, worksheet, models, row);

            var modelLength = models.Length;
            for (int i = 1; i < modelLength; i++)
            {
                column = RenderFundingYear(i, worksheet, models, row, column);
            }

            column = NextMaxColumn(worksheet);
            RenderTotals(worksheet, models, column, row);

            column = NextMaxColumn(worksheet);
            row = RenderGrandTotals(worksheet, modelLength, column, row);

            return row;
        }

        private int RenderGrandTotals(Worksheet worksheet, int yearsCount, int column, int row)
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

            return maxRow;
        }

        private int RenderFundingYear(int index, Worksheet worksheet, FundingSummaryReportEarnings[] models, int row, int? column = null)
        {
            if (index > 0)
            {
                column = NextColumn(column.Value);

                foreach (var category in models[index].DeliverableCategories)
                {
                    row = NextRowWithBreak(row);

                    BuildCategoryHeaderRow(false, StartPeriod, worksheet, category.GroupHeader, row, column.Value);

                    row = RenderDeliverableCategory(false, StartPeriod, worksheet, category, row, column.Value);
                }

                RenderMonthlyTotals(false, StartPeriod, worksheet, models[index], row, column.Value);

                return worksheet.Cells.MaxDataColumn;
            }
            else
            {
                foreach (var category in models[index].DeliverableCategories)
                {
                    row = NextMaxRow(worksheet) + 1;

                    BuildCategoryHeaderRow(true, FirstYearStartPeriod, worksheet, category.GroupHeader, row);

                    ApplyStyleToRow(worksheet, row, _deliverableGroupStyle);

                    row = RenderDeliverableCategory(true, FirstYearStartPeriod, worksheet, category, row);
                }

                RenderMonthlyTotals(true, FirstYearStartPeriod, worksheet, models[index], row);

                return LastColumnForRow(worksheet, row);
            }
        }

        private Worksheet RenderMonthlyTotals(bool firstFundingYear, int monthStart, Worksheet worksheet, FundingSummaryReportEarnings fundingSummaryModel, int row, int? column = null)
        {
            row = firstFundingYear ? NextMaxRow(worksheet) + 1 : NextRowWithBreak(row);

            BuildRow(firstFundingYear, monthStart, worksheet, fundingSummaryModel.MonthlyTotals, row, column ?? 0);

            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            row = NextRow(row);

            BuildRow(firstFundingYear, monthStart, worksheet, fundingSummaryModel.CumulativeMonthlyTotals, row, column ?? 0);

            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            return worksheet;
        }

        private int RenderDeliverableCategory(bool firstFundingYear, int monthStart, Worksheet worksheet, IDeliverableCategory category, int row, int? column = null)
        {
            foreach (var subCategory in category.DeliverableSubCategories)
            {
                if (subCategory.DisplayTitle)
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = column.HasValue ? NextRow(row) : NextMaxRow(worksheet);

                        BuildRow(firstFundingYear, monthStart, worksheet, value, row, column ?? 0);
                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }

                    row = column.HasValue ? NextRow(row) : NextMaxRow(worksheet);

                    BuildRow(firstFundingYear, monthStart, worksheet, subCategory.Totals, row, column ?? 0);
                    ApplyStyleToRow(worksheet, row, _deliverableSubGroupStyle);
                }
                else
                {
                    foreach (var value in subCategory.ReportValues)
                    {
                        row = column.HasValue ? NextRow(row) : NextMaxRow(worksheet);

                        BuildRow(firstFundingYear, monthStart, worksheet, value, row, column ?? 0);

                        ApplyStyleToRow(worksheet, row, _deliverableLineStyle);
                    }
                }
            }

            row = NextRow(row);

            BuildRow(firstFundingYear, monthStart, worksheet, category.Totals, row, column ?? 0);

            ApplyStyleToRow(worksheet, row, _deliverableGroupStyle);

            return row;
        }

        private Worksheet RenderTotals(
        Worksheet worksheet,
        FundingSummaryReportEarnings[] models,
        int column,
        int row)
        {
            RenderColumnTotal(worksheet, models[0], string.Concat(models[0].AcademicYear, " Subtotal"), column, row);

            var modelLength = models.Length;
            for (int i = 1; i < modelLength; i++)
            {
                column = NextMaxColumn(worksheet);

                RenderColumnTotal(worksheet, models[i], string.Concat(models[i].AcademicYear, " Subtotal"), column, row);
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

        private Worksheet BuildRow(bool firstFundingYear, int monthStart, Worksheet worksheet, IPeriodisedReportValue periodisedValues, int row, int column)
        {
            var months = periodisedValues.MonthlyValues.Skip(monthStart - 1).Select(x => (object)x).ToList();

            if (firstFundingYear)
            {
                months.Insert(0, periodisedValues.Title);
            }

            worksheet.Cells.ImportObjectArray(
                 months.ToArray(),
                 row,
                 column,
                 false);

            return worksheet;
        }

        private Worksheet BuildCategoryHeaderRow(bool firstFundingYear, int monthStart, Worksheet worksheet, GroupHeader header, int row, int? column = null)
        {
            var months = header.Months.Skip(monthStart - 1).ToList();

            if (firstFundingYear)
            {
                months.Insert(0, header.Title);
            }

            worksheet.Cells.ImportObjectArray(
                   months.ToArray(),
                   row,
                   column ?? 0,
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

        private int GetCurrentPeriod(int currentPeriod)
        {
            return currentPeriod > 12 ? 12 : currentPeriod;
        }

        private void ConfigureStyles()
        {
            _defaultStyle.Font.Size = 10;
            _defaultStyle.Font.Name = "Arial";
            _defaultStyle.SetCustom(FundingSummaryReportConstants.DecimalFormat, false);

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
