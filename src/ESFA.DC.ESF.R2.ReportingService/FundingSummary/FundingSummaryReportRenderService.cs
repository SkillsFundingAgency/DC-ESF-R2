using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private readonly Style _fundingCategoryStyle;
        private readonly Style _fundingSubCategoryStyle;
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

        public FundingSummaryReportRenderService()
        {
            var cellsFactory = new CellsFactory();

            _defaultStyle = cellsFactory.CreateStyle();
            _textWrappedStyle = cellsFactory.CreateStyle();
            _futureMonthStyle = cellsFactory.CreateStyle();
            _fundingCategoryStyle = cellsFactory.CreateStyle();
            _fundingSubCategoryStyle = cellsFactory.CreateStyle();
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

            RenderHeader(worksheet, NextRow(worksheet), fundingSummaryReportTab.Header);

            RenderBody(worksheet, fundingSummaryReportTab);

            RenderFooter(worksheet, NextRow(worksheet) + 1, fundingSummaryReportTab.Footer);

            worksheet.AutoFitColumn(0);
        }

        private int CalculateColumns(int bodyCount)
        {
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

            foreach (var entry in header.IlrFileDetails)
            {
                //worksheet.Cells.ImportTwoDimensionArray(new object[,]
                //{
                //    { entry.Key, entry.Value }
                //}, row, 0);

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

            ApplyStyleToRow(worksheet, row, _headerAndFooterStyle);

            row++;

            return worksheet;
        }

        private Worksheet RenderBody(Worksheet worksheet, IFundingSummaryReportTab fundingSummaryReportTab)
        {
            var row = NextRow(worksheet) + 1;

             // Title
            worksheet.Cells.ImportObjectArray(new object[] { fundingSummaryReportTab.Title }, row, 0, false);
            ApplyStyleToRow(worksheet, row, _fundingCategoryStyle);

            row = NextRow(worksheet) + 1;

            // Categories
            var baseYear = fundingSummaryReportTab.Body.Min(x => x.Year);
            var baseYearData = fundingSummaryReportTab.Body.Where(x => x.Year == baseYear).FirstOrDefault();
            var subsequentYearsData = fundingSummaryReportTab.Body.Where(x => x.Year > baseYear).OrderBy(x => x).ToList();

            RenderCategories(worksheet, baseYearData, subsequentYearsData, fundingSummaryReportTab.FundingSummaryReportTabTotals, row);

            //foreach (var fundingSubCategory in fundingCategory.FundingSubCategories)
            //{
            //    RenderFundingSubCategory(worksheet, fundingSubCategory);
            //}

            //row = NextRow(worksheet) + 1;
            //RenderFundingSummaryReportRow(worksheet, row, fundingCategory);
            //ApplyStyleToRow(worksheet, row, _fundingCategoryStyle);
            //ApplyFutureMonthStyleToRow(worksheet, row, fundingCategory.CurrentPeriod);

            //row = NextRow(worksheet);
            //worksheet.Cells.ImportObjectArray(
            //    new object[]
            //    {
            //        fundingCategory.CumulativeFundingCategoryTitle,
            //        fundingCategory.CumulativePeriod1,
            //        fundingCategory.CumulativePeriod2,
            //        fundingCategory.CumulativePeriod3,
            //        fundingCategory.CumulativePeriod4,
            //        fundingCategory.CumulativePeriod5,
            //        fundingCategory.CumulativePeriod6,
            //        fundingCategory.CumulativePeriod7,
            //        fundingCategory.CumulativePeriod8,
            //        fundingCategory.CumulativePeriod9,
            //        fundingCategory.CumulativePeriod10,
            //        fundingCategory.CumulativePeriod11,
            //        fundingCategory.CumulativePeriod12,
            //        NotApplicable,
            //        NotApplicable,
            //        NotApplicable,
            //        NotApplicable,
            //    },
            //    row,
            //    0,
            //    false);
            //ApplyStyleToRow(worksheet, row, _fundingCategoryStyle);
            //ApplyFutureMonthStyleToRow(worksheet, row, fundingCategory.CurrentPeriod);

            //if (!string.IsNullOrWhiteSpace(fundingCategory.Note))
            //{
            //    row = NextRow(worksheet);
            //    worksheet.Cells[row, 0].PutValue(fundingCategory.Note);
            //    ApplyStyleToRow(worksheet, row, _textWrappedStyle);
            //}

            return worksheet;
        }

        private Worksheet RenderFundingSummaryReportRow(Worksheet worksheet, int row, IFundingSummaryReportRow fundingSummaryReportRow)
        {
            //worksheet.Cells.ImportObjectArray(new object[]
            //{
            //    fundingSummaryReportRow.Title,
            //    fundingSummaryReportRow.Period1,
            //    fundingSummaryReportRow.Period2,
            //    fundingSummaryReportRow.Period3,
            //    fundingSummaryReportRow.Period4,
            //    fundingSummaryReportRow.Period5,
            //    fundingSummaryReportRow.Period6,
            //    fundingSummaryReportRow.Period7,
            //    fundingSummaryReportRow.Period8,
            //    fundingSummaryReportRow.Period9,
            //    fundingSummaryReportRow.Period10,
            //    fundingSummaryReportRow.Period11,
            //    fundingSummaryReportRow.Period12,
            //    fundingSummaryReportRow.Period1To8,
            //    fundingSummaryReportRow.Period9To12,
            //    fundingSummaryReportRow.YearToDate,
            //    fundingSummaryReportRow.Total,
            //}, row, 0, false);

            return worksheet;
        }

        private Worksheet RenderCategories(
            Worksheet worksheet,
            FundingSummaryModel baseModel,
            ICollection<FundingSummaryModel> subsequentModels,
            FundingSummaryReportTabTotal fundingSummaryReportTabTotal,
            int row)
        {
            RenderBaseFundingYear(worksheet, baseModel, row);

            foreach (var model in subsequentModels)
            {
                RenderSubsequentFundingYear(worksheet, model, row);
            }

            RenderTotals(worksheet, baseModel, subsequentModels, fundingSummaryReportTabTotal, row);

            ApplyStyleToRow(worksheet, row, _fundingSubCategoryStyle);

            //var renderFundLineGroupTotals = fundingSubCategory.FundLineGroups.Count > 1;

            //foreach (var fundLineGroup in fundingSubCategory.FundLineGroups)
            //{
            //    RenderFundLineGroup(worksheet, fundLineGroup, renderFundLineGroupTotals);
            //}

            //row = NextRow(worksheet);
            //RenderFundingSummaryReportRow(worksheet, row, fundingSubCategory);
            //ApplyStyleToRow(worksheet, row, _fundingSubCategoryStyle);
            //ApplyFutureMonthStyleToRow(worksheet, row, fundingSubCategory.CurrentPeriod);

            return worksheet;
        }

        private Worksheet RenderTotals(
            Worksheet worksheet,
            FundingSummaryModel baseModel,
            ICollection<FundingSummaryModel> subsequentModels,
            FundingSummaryReportTabTotal fundingSummaryReportTabTotal,
            int row)
        {
            var column = NextColumn(worksheet);
            var totalColumnsCount = subsequentModels.Count + 2;

            var totals = new object[totalColumnsCount];

            totals[0] = string.Concat(baseModel.AcademicYear, " Subtotal");

            var index = 1;

            while (index < totalColumnsCount - 1)
            {
                totals[index] = string.Concat(subsequentModels.Skip(index - 1).Take(index).Select(x => x.AcademicYear).FirstOrDefault(), " Subtotal");
                // totals[index] = subsequentModels.Skip(index - 1).Take(index).Select(x => x.CommunityGrants.Totals.Title);
                index++;
            }

            totals[totalColumnsCount - 1] = "Grand Total";

            worksheet.Cells.ImportObjectArray(
               totals,
               row,
               column,
               false);

            return worksheet;
        }

        private Worksheet RenderBaseFundingYear(Worksheet worksheet, FundingSummaryModel fundingSummaryModel, int row)
        {
            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    fundingSummaryModel.CommunityGrants.GroupHeader.Title,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderApril,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderMay,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderJune,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderJuly,
                },
                row,
                0,
                false);

            return worksheet;
        }

        private Worksheet RenderSubsequentFundingYear(Worksheet worksheet, FundingSummaryModel fundingSummaryModel, int row)
        {
            var column = NextColumn(worksheet);

            worksheet.Cells.ImportObjectArray(
                new object[]
                {
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderAugust,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderSeptember,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderOctober,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderNovember,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderDecember,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderJanuary,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderFebruary,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderMarch,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderApril,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderMay,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderJune,
                    fundingSummaryModel.CommunityGrants.GroupHeader.HeaderJuly,
                },
                row,
                column,
                false);

            return worksheet;
        }

        private Worksheet RenderFundingSubCategory(Worksheet worksheet, IFundingSubCategory fundingSubCategory)
        {
            var row = NextRow(worksheet) + 1;

            //worksheet.Cells.ImportObjectArray(new object[]
            //{
            //    fundingSubCategory.FundingSubCategoryTitle,
            //    $"Aug-{ReportingConstants.ShortYearStart}",
            //    $"Sep-{ReportingConstants.ShortYearStart}",
            //    $"Oct-{ReportingConstants.ShortYearStart}",
            //    $"Nov-{ReportingConstants.ShortYearStart}",
            //    $"Dec-{ReportingConstants.ShortYearStart}",
            //    $"Jan-{ReportingConstants.ShortYearEnd}",
            //    $"Feb-{ReportingConstants.ShortYearEnd}",
            //    $"Mar-{ReportingConstants.ShortYearEnd}",
            //    $"Apr-{ReportingConstants.ShortYearEnd}",
            //    $"May-{ReportingConstants.ShortYearEnd}",
            //    $"Jun-{ReportingConstants.ShortYearEnd}",
            //    $"Jul-{ReportingConstants.ShortYearEnd}",
            //    "Aug - Mar",
            //    "Apr - Jul",
            //    "Year To Date",
            //    "Total",
            //},
            //row,
            //0,
            //false);

            ApplyStyleToRow(worksheet, row, _fundingSubCategoryStyle);

            var renderFundLineGroupTotals = fundingSubCategory.FundLineGroups.Count > 1;

            foreach (var fundLineGroup in fundingSubCategory.FundLineGroups)
            {
                RenderFundLineGroup(worksheet, fundLineGroup, renderFundLineGroupTotals);
            }

            row = NextRow(worksheet);
            RenderFundingSummaryReportRow(worksheet, row, fundingSubCategory);
            ApplyStyleToRow(worksheet, row, _fundingSubCategoryStyle);
            ApplyFutureMonthStyleToRow(worksheet, row, fundingSubCategory.CurrentPeriod);

            return worksheet;
        }

        private Worksheet RenderFundLineGroup(Worksheet worksheet, IFundLineGroup fundLineGroup, bool renderFundLineGroupTotal)
        {
            foreach (var fundLine in fundLineGroup.FundLines)
            {
                RenderFundLine(worksheet, fundLine);
            }

            if (renderFundLineGroupTotal)
            {
                var row = NextRow(worksheet);
                RenderFundingSummaryReportRow(worksheet, row, fundLineGroup);
                ApplyStyleToRow(worksheet, row, _fundLineGroupStyle);
                ApplyFutureMonthStyleToRow(worksheet, row, fundLineGroup.CurrentPeriod);
            }

            return worksheet;
        }

        private Worksheet RenderFundLine(Worksheet worksheet, IFundLine fundLine)
        {
            var row = NextRow(worksheet);

            RenderFundingSummaryReportRow(worksheet, row, fundLine);
            ApplyFutureMonthStyleToRow(worksheet, row, fundLine.CurrentPeriod);

            return worksheet;
        }

        private int NextRow(Worksheet worksheet)
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

        private void ConfigureStyles()
        {
            _defaultStyle.Font.Size = 10;
            _defaultStyle.Font.Name = "Arial";
            _defaultStyle.SetCustom(DecimalFormat, false);

            _textWrappedStyle.Font.Size = 10;
            _textWrappedStyle.Font.Name = "Arial";
            _textWrappedStyle.IsTextWrapped = true;

            _futureMonthStyle.Font.IsItalic = true;

            _fundingCategoryStyle.ForegroundColor = Color.FromArgb(191, 191, 191);
            _fundingCategoryStyle.Pattern = BackgroundType.Solid;
            _fundingCategoryStyle.Font.Size = 13;
            _fundingCategoryStyle.Font.IsBold = true;
            _fundingCategoryStyle.Font.Name = "Arial";
            _fundingCategoryStyle.SetCustom(DecimalFormat, false);

            _fundingSubCategoryStyle.ForegroundColor = Color.FromArgb(242, 242, 242);
            _fundingSubCategoryStyle.Pattern = BackgroundType.Solid;
            _fundingSubCategoryStyle.Font.Size = 11;
            _fundingSubCategoryStyle.Font.IsBold = true;
            _fundingSubCategoryStyle.Font.Name = "Arial";
            _fundingSubCategoryStyle.SetCustom(DecimalFormat, false);

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
