using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aspose.Cells;
using CsvHelper;
using CsvHelper.Configuration;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models.Generation;
using ESFA.DC.ESF.R2.Models.Styling;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Abstract
{
    public abstract class AbstractReportService
    {
        protected string ReportFileName;

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IValueProvider _valueProvider;
        private readonly IFileService _fileService;

        private readonly Dictionary<Worksheet, int> _currentRow;

        protected AbstractReportService(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService fileService,
            string taskName)
        {
            _dateTimeProvider = dateTimeProvider;
            _valueProvider = valueProvider;
            _fileService = fileService;
            TaskName = taskName;

            _currentRow = new Dictionary<Worksheet, int>();
        }

        public string ReportName { get; set; }

        public string TaskName { get; }

        public bool IsMatch(string reportTaskName)
        {
            return reportTaskName == ReportName;
        }

        public string GetExternalFilename(int ukPrn, long jobId, DateTime submissionDateTime, string extension)
        {
            DateTime dateTime = _dateTimeProvider.ConvertUtcToUk(submissionDateTime);
            return $"{ukPrn}/{jobId}/{ReportFileName} {dateTime:yyyyMMdd-HHmmss}{extension}";
        }

        public string GetExternalFilename(string ukPrn, long jobId, DateTime submissionDateTime, string extension)
        {
            DateTime dateTime = _dateTimeProvider.ConvertUtcToUk(submissionDateTime);
            return $"{ukPrn}/{jobId}/{ReportFileName} {dateTime:yyyyMMdd-HHmmss}{extension}";
        }

        /// <summary>
        /// Builds a CSV report using the specified mapper as the list of column names.
        /// </summary>
        /// <typeparam name="TMapper">The mapper.</typeparam>
        /// <typeparam name="TModel">The model.</typeparam>
        /// <param name="csvWriter">The memory stream to write to.</param>
        /// <param name="records">The records to persist.</param>
        /// <param name="mapperOverride">Optional override of the TMapper, for example, when needing to specify constructor parameters.</param>
        protected void WriteCsvRecords<TMapper, TModel>(CsvWriter csvWriter, IEnumerable<TModel> records, TMapper mapperOverride = null)
            where TMapper : ClassMap
            where TModel : class
        {
            if (mapperOverride == null)
            {
                csvWriter.Configuration.RegisterClassMap<TMapper>();
            }
            else
            {
                csvWriter.Configuration.RegisterClassMap(mapperOverride);
            }

            csvWriter.WriteHeader<TModel>();

            csvWriter.NextRecord();
            csvWriter.WriteRecords(records);

            csvWriter.Configuration.UnregisterClassMap();
        }

        protected void WriteExcelRecords<TMapper, TModel>(Worksheet worksheet, TMapper classMap, IEnumerable<TModel> records, CellStyle headerStyle, CellStyle recordStyle, bool pivot = false)
            where TMapper : ClassMap
            where TModel : class
        {
            int currentRow = GetCurrentRow(worksheet);
            ModelProperty[] modelProperties = classMap.MemberMaps.OrderBy(x => x.Data.Index).Select(x => new ModelProperty(x.Data.Names.Names.ToArray(), (PropertyInfo)x.Data.Member)).ToArray();
            string[] names = modelProperties.SelectMany(x => x.Names).ToArray();

            worksheet.Cells.ImportObjectArray(names, currentRow, 0, pivot);
            if (headerStyle != null)
            {
                worksheet.Cells.CreateRange(currentRow, 0, pivot ? names.Length : 1, pivot ? 1 : names.Length).ApplyStyle(headerStyle.Style, headerStyle.StyleFlag);
            }

            int column = 0, localRow = currentRow;
            if (pivot)
            {
                // If we have pivoted then we need to move one column in, as the header is in column 1.
                column = 1;
            }
            else
            {
                currentRow++;
                localRow++;
            }

            foreach (TModel record in records)
            {
                int widestColumn = 1;

                foreach (var modelProperty in modelProperties)
                {
                    List<object> values = new List<object>();
                    _valueProvider.GetFormattedValue(values, modelProperty.MethodInfo.GetValue(record), classMap, modelProperty);

                    worksheet.Cells.ImportObjectArray(values.ToArray(), localRow, column, false);
                    if (recordStyle != null)
                    {
                        worksheet.Cells.CreateRange(localRow, column, 1, values.Count).ApplyStyle(recordStyle.Style, recordStyle.StyleFlag);
                    }

                    if (pivot)
                    {
                        localRow++;
                    }
                    else
                    {
                        column += values.Count;
                    }

                    if (values.Count > widestColumn)
                    {
                        widestColumn = values.Count;
                    }
                }

                if (pivot)
                {
                    column += widestColumn;
                    localRow = currentRow;
                }
            }

            if (pivot)
            {
                currentRow += names.Length;
            }
            else
            {
                currentRow += records.Count();
            }

            SetCurrentRow(worksheet, currentRow);
        }

        protected void WriteRecordsFromArray<TMapper>(Worksheet worksheet, TMapper classMap, object[] names, CellStyle headerStyle, bool pivot = false)
            where TMapper : ClassMap
        {
            int currentRow = GetCurrentRow(worksheet);

            worksheet.Cells.ImportObjectArray(names, currentRow, 0, pivot);
            if (headerStyle != null)
            {
                worksheet.Cells.CreateRange(currentRow, 0, pivot ? names.Length : 1, pivot ? 1 : names.Length).ApplyStyle(headerStyle.Style, headerStyle.StyleFlag);
            }

            if (pivot)
            {
                currentRow += names.Length;
            }
            else
            {
                currentRow++;
            }

            SetCurrentRow(worksheet, currentRow);
        }

        protected void WriteExcelRecordsFromModelProperty<TMapper, TModel>(Worksheet worksheet, TMapper classMap, ModelProperty[] modelProperties, TModel record, CellStyle recordStyle, bool pivot = false)
            where TMapper : ClassMap
            where TModel : class
        {
            int currentRow = GetCurrentRow(worksheet);

            int column = 0;
            if (pivot)
            {
                // If we have pivoted then we need to move one column in, as the header is in column 1.
                column = 1;
            }

            List<object> values = new List<object>();
            foreach (var modelProperty in modelProperties)
            {
                _valueProvider.GetFormattedValue(values, modelProperty.MethodInfo.GetValue(record), classMap, modelProperty);
            }

            worksheet.Cells.ImportObjectArray(values.ToArray(), currentRow, column, pivot);
            if (recordStyle != null)
            {
                worksheet.Cells.CreateRange(currentRow, column, pivot ? values.Count : 1, pivot ? 1 : values.Count).ApplyStyle(recordStyle.Style, recordStyle.StyleFlag);
            }

            if (pivot)
            {
                currentRow += values.Count;
            }
            else
            {
                currentRow++;
            }

            SetCurrentRow(worksheet, currentRow);
        }

        protected void WriteBlankRow(Worksheet worksheet, int numberOfBlankRows = 1)
        {
            int currentRow = GetCurrentRow(worksheet);
            currentRow += numberOfBlankRows;
            SetCurrentRow(worksheet, currentRow);
        }

        protected void WriteTitleRecord(Worksheet worksheet, string heading, CellStyle headerStyle = null, int numberOfColumns = 1)
        {
            int currentRow = GetCurrentRow(worksheet);
            worksheet.Cells[currentRow, 0].PutValue(heading);
            if (headerStyle != null)
            {
                worksheet.Cells.CreateRange(currentRow, 0, 1, numberOfColumns).ApplyStyle(headerStyle.Style, headerStyle.StyleFlag);
            }

            currentRow++;
            SetCurrentRow(worksheet, currentRow);
        }

        protected int GetCurrentRow(Worksheet worksheet)
        {
            if (!_currentRow.ContainsKey(worksheet))
            {
                _currentRow.Add(worksheet, 0);
            }

            return _currentRow[worksheet];
        }

        private void SetCurrentRow(Worksheet worksheet, int currentRow)
        {
            _currentRow[worksheet] = currentRow;
        }
    }
}
