using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportRenderService : IFundingSummaryReportRenderService
    {
        private const int StartYear = 19;
        private const int EndYear = 20;

        private const string NotApplicable = "N/A";
        private const string DecimalFormat = "#,##0.00";

        private const int StartColumn = 0;
        private const int DefaultBodyColumnCount = 7;

        private readonly Style _defaultStyle;
        private readonly Style _textWrappedStyle;
        private readonly Style _futureMonthStyle;
        private readonly Style _reportTitleStyle;
        private readonly Style _deliverableGroupStyle;
        private readonly Style _fundLineGroupStyle;
        private readonly Style _headerAndFooterStyle;

        private readonly StyleFlag _styleFlag = new StyleFlag()
        {
            All = true,
        };

        private readonly StyleFlag _italicStyleFlag = new StyleFlag()
        {
            FontItalic = true
        };

        private readonly StyleFlag _leftAlignStyleFlag = new StyleFlag()
        {
            HorizontalAlignment = true,
        };

        public FundingSummaryReportRenderService()
        {
            var cellsFactory = new CellsFactory();

            _defaultStyle = cellsFactory.CreateStyle();
            _textWrappedStyle = cellsFactory.CreateStyle();
            _futureMonthStyle = cellsFactory.CreateStyle();
            _reportTitleStyle = cellsFactory.CreateStyle();
            _deliverableGroupStyle = cellsFactory.CreateStyle();
            _fundLineGroupStyle = cellsFactory.CreateStyle();
            _headerAndFooterStyle = cellsFactory.CreateStyle();

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

            var columnStyle = worksheet.Cells.Columns[0].Style;
            columnStyle.HorizontalAlignment = TextAlignmentType.Left;
            worksheet.Cells.Columns[0].ApplyStyle(columnStyle, _leftAlignStyleFlag);
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
            FundingSummaryModel baseModel,
            ICollection<FundingSummaryModel> subsequentModels,
            int row)
        {
            RenderBaseFundingYear(worksheet, baseModel, row);

            var column = NextColumn(worksheet);

            foreach (var model in subsequentModels)
            {
                column = RenderSubsequentFundingYear(worksheet, model, column, row);
            }

            column = NextColumn(worksheet);

            RenderTotals(worksheet, baseModel, subsequentModels, column, row);

            return worksheet;
        }

        private Worksheet RenderBaseFundingYear(Worksheet worksheet, FundingSummaryModel fundingSummaryModel, int row)
        {
            foreach (var category in fundingSummaryModel.DeliverableCategories)
            {
                row = NextMaxRow(worksheet) + 1;

                BuildBaseHeaderRow(worksheet, category.GroupHeader, row);

                ApplyStyleToRow(worksheet, row, _deliverableGroupStyle);

                RenderBaseDeliverables(worksheet, category, row);
            }

            RenderBaseMonthlyTotals(worksheet, fundingSummaryModel, row);

            return worksheet;
        }

        private Worksheet RenderBaseMonthlyTotals(Worksheet worksheet, FundingSummaryModel fundingSummaryModel, int row)
        {
            row = NextMaxRow(worksheet) + 1;

            BuildBaseRow(worksheet, fundingSummaryModel.MonthlyTotals, row);

            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            row = NextRow(row);

            BuildBaseRow(worksheet, fundingSummaryModel.CumulativeMonthlyTotals, row);

            ApplyStyleToRow(worksheet, row, _reportTitleStyle);

            return worksheet;
        }

        private Worksheet RenderBaseDeliverables(Worksheet worksheet, IDeliverableCategory category, int row)
        {
            var deliverableRow = row;
            foreach (var value in category.ReportValues)
            {
                deliverableRow = NextMaxRow(worksheet);

                BuildBaseRow(worksheet, value, deliverableRow);
            }

            deliverableRow = NextMaxRow(worksheet);

            //totals
            BuildBaseRow(worksheet, category.Totals, deliverableRow);

            ApplyStyleToRow(worksheet, deliverableRow, _deliverableGroupStyle);

            return worksheet;
        }

        private int RenderSubsequentFundingYear(Worksheet worksheet, FundingSummaryModel fundingSummaryModel, int column, int row)
        {
            column = NextColumn(worksheet);

            foreach (var category in fundingSummaryModel.DeliverableCategories)
            {
                row = NextRowWithBreak(row);

                BuildHeaderRow(worksheet, category.GroupHeader, row, column);

                row = RenderSubsequentDeliverables(worksheet, category, column, row);
            }

            RenderSubsequentMonthlyTotals(worksheet, fundingSummaryModel, column, row);

            return column;
        }

        private Worksheet RenderSubsequentMonthlyTotals(Worksheet worksheet, FundingSummaryModel fundingSummaryModel, int column, int row)
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
            foreach (var value in category.ReportValues)
            {
                row = NextRow(row);

                BuildRow(worksheet, value, row, column);
            }

            row = NextRow(row);

            //totals
            BuildRow(worksheet, category.Totals, row, column);

            return row;
        }

        private Worksheet RenderSubsequentDeliverableTotals(Worksheet worksheet, IDeliverableCategory category, int row)
        {
            var deliverableRow = NextMaxRow(worksheet);
            var column = NextColumn(worksheet);

            BuildRow(worksheet, category.Totals, deliverableRow, column);

            return worksheet;
        }

        private Worksheet RenderTotals(
          Worksheet worksheet,
          FundingSummaryModel baseModel,
          ICollection<FundingSummaryModel> subsequentModels,
          int column,
          int row)
        {
            RenderColumnTotal(worksheet, baseModel, string.Concat(baseModel.AcademicYear, " Subtotal"), column, row);

            foreach (var model in subsequentModels)
            {
                column = NextColumn(worksheet);

                RenderColumnTotal(worksheet, model, string.Concat(model.AcademicYear, " Subtotal"), column, row);
            }

            RenderColumnTotal(worksheet, baseModel, "Grand Total", column, row);

            return worksheet;
        }

        private Worksheet RenderColumnTotal(
          Worksheet worksheet,
          FundingSummaryModel model,
          string header,
          int column,
          int row)
        {
            foreach (var category in model.DeliverableCategories)
            {
                row = NextRowWithBreak(row);
                worksheet.Cells.ImportObjectArray(
                        new object[]
                        {
                        header
                        },
                        row,
                        column,
                        false);

                row = RenderDeliverableColumnTotals(worksheet, category, column, row);
            }

            row = NextRowWithBreak(row);

            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    model.YearTotal
                },
                row,
                column,
                false);

            row = NextRow(row);

            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    model.CumulativeYearTotal
                },
                row,
                column,
                false);

            return worksheet;
        }

        private int RenderDeliverableColumnTotals(Worksheet worksheet, IDeliverableCategory category, int column, int row)
        {
            foreach (var model in category.ReportValues)
            {
                row = NextRow(row);

                worksheet.Cells.ImportObjectArray(
                   new object[]
                   {
                       model.Total
                   },
                   row,
                   column,
                   false);
            }

            row = NextRow(row);

            worksheet.Cells.ImportObjectArray(
                  new object[]
                  {
                       category.Totals.Total
                  },
                  row,
                  column,
                  false);

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

        private int NextColumn(Worksheet worksheet)
        {
            return worksheet.Cells.MaxDataColumn + 1;
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
           // worksheet.Cells.SetRowHeight(0, 27); // Adjust the image row height

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
            _defaultStyle.SetCustom(DecimalFormat, false);

            _textWrappedStyle.Font.Size = 10;
            _textWrappedStyle.Font.Name = "Arial";
            _textWrappedStyle.IsTextWrapped = true;

            _futureMonthStyle.Font.IsItalic = true;

            _reportTitleStyle.ForegroundColor = Color.FromArgb(191, 191, 191);
            _reportTitleStyle.Pattern = BackgroundType.Solid;
            _reportTitleStyle.Font.Size = 13;
            _reportTitleStyle.Font.IsBold = true;
            _reportTitleStyle.Font.Name = "Arial";
            _reportTitleStyle.SetCustom(DecimalFormat, false);

            _deliverableGroupStyle.ForegroundColor = Color.FromArgb(242, 242, 242);
            _deliverableGroupStyle.Pattern = BackgroundType.Solid;
            _deliverableGroupStyle.Font.Size = 11;
            _deliverableGroupStyle.Font.IsBold = true;
            _deliverableGroupStyle.Font.Name = "Arial";
            _deliverableGroupStyle.SetCustom(DecimalFormat, false);
            _deliverableGroupStyle.HorizontalAlignment = TextAlignmentType.Right;

            _fundLineGroupStyle.Font.Size = 11;
            _fundLineGroupStyle.Font.IsBold = true;
            _fundLineGroupStyle.Font.Name = "Arial";
            _fundLineGroupStyle.SetCustom(DecimalFormat, false);

            _headerAndFooterStyle.Font.Size = 10;
            _headerAndFooterStyle.Font.Name = "Arial";
            _headerAndFooterStyle.Font.IsBold = true;
        }
    }
}
