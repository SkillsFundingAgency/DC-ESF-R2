using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models.Generation;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public sealed class ValueProvider : IValueProvider
    {
        private const string Zero = "0";
        private const string NotApplicable = "n/a";
        private static readonly string DateTimeMin = DateTime.MinValue.ToString("dd/MM/yyyy");

        public void GetFormattedValue(List<object> values, object value, ClassMap mapper, ModelProperty modelProperty)
        {
            Type propertyType = modelProperty.MethodInfo.PropertyType;

            if (value == null)
            {
                HandleNull(values, propertyType, mapper, modelProperty);
                return;
            }

            if (value is bool b)
            {
                values.Add(b ? "Yes" : "No");
                return;
            }

            if (value is decimal d1)
            {
                int decimalPoints = GetDecimalPoints(mapper, modelProperty);
                decimal rounded = decimal.Round(d1, decimalPoints);
                values.Add(rounded.ToString($"N{decimalPoints:D}"));
                return;
            }

            if (IsOfNullableType<decimal>(propertyType))
            {
                decimal? d = (decimal?)value;
                int decimalPoints = GetDecimalPoints(mapper, modelProperty);
                decimal rounded = decimal.Round(d.GetValueOrDefault(0), decimalPoints);
                values.Add(rounded);
                return;
            }

            if (value is int i)
            {
                if (i == 0)
                {
                    if (!CanAddZeroInt(mapper, modelProperty))
                    {
                        values.Add(string.Empty);
                        return;
                    }
                }
            }

            if (value is string str)
            {
                if (str == DateTimeMin)
                {
                    values.Add(string.Empty);
                    return;
                }
            }

            if (value is List<decimal> listOfDecimals)
            {
                foreach (decimal dec in listOfDecimals)
                {
                    int decimalPoints = GetDecimalPoints(mapper, modelProperty);
                    decimal rounded = decimal.Round(dec, decimalPoints);
                    values.Add(rounded);
                }

                return;
            }

            if (value is List<decimal?> listOfNullDecimals)
            {
                foreach (decimal? dec in listOfNullDecimals)
                {
                    if (dec == null)
                    {
                        HandleNull(values, typeof(decimal?), mapper, modelProperty);
                        continue;
                    }

                    int decimalPoints = GetDecimalPoints(mapper, modelProperty);
                    decimal rounded = decimal.Round(dec ?? 0, decimalPoints);
                    values.Add(rounded);
                }

                return;
            }

            if (value is string[] arrayOfStrings)
            {
                foreach (string s in arrayOfStrings)
                {
                    if (s == DateTimeMin)
                    {
                        values.Add(string.Empty);
                        break;
                    }

                    values.Add(s);
                }

                return;
            }

            if (value is List<FundingSummaryReportYearlyValueModel> totals)
            {
                foreach (FundingSummaryReportYearlyValueModel fundingSummaryReportYearlyValueModel in totals)
                {
                    foreach (decimal dec in fundingSummaryReportYearlyValueModel.Values)
                    {
                        int decimalPoints = GetDecimalPoints(mapper, modelProperty);
                        decimal rounded = decimal.Round(dec, decimalPoints);
                        values.Add(rounded);
                    }
                }

                return;
            }

            values.Add(value);
        }

        private void HandleNull(List<object> values, Type propertyType, ClassMap mapper, ModelProperty modelProperty)
        {
            if (IsNullable(propertyType) && propertyType == typeof(decimal?))
            {
                if (IsNullableMapper(mapper, modelProperty))
                {
                    values.Add(NotApplicable);
                    return;
                }

                int decimalPoints = GetDecimalPoints(mapper, modelProperty);
                values.Add(PadDecimal(0, decimalPoints));
                return;
            }

            values.Add(string.Empty);
        }

        private bool IsNullableMapper(ClassMap mapper, ModelProperty modelProperty)
        {
            MemberMap memberMap = mapper.MemberMaps.SingleOrDefault(x => x.Data.Names.Names.Intersect(modelProperty.Names).Any());
            return memberMap?.Data?.TypeConverterOptions?.NullValues?.Contains(NotApplicable) ?? false;
        }

        private bool CanAddZeroInt(ClassMap mapper, ModelProperty modelProperty)
        {
            MemberMap memberMap = mapper.MemberMaps.SingleOrDefault(x => x.Data.Names.Names.Intersect(modelProperty.Names).Any());
            return !(memberMap?.Data?.TypeConverterOptions?.NullValues?.Contains(Zero) ?? false);
        }

        private bool IsOfNullableType<T>(object o)
        {
            return Nullable.GetUnderlyingType(o.GetType()) != null && o is T;
        }

        private bool IsNullable(Type propertyType)
        {
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private int GetDecimalPoints(ClassMap mapper, ModelProperty modelProperty)
        {
            if (mapper == null || modelProperty == null)
            {
                return 2;
            }

            MemberMap memberMap = mapper.MemberMaps.SingleOrDefault(x => x.Data.Names.Names.Intersect(modelProperty.Names).Any());
            string[] format = memberMap?.Data?.TypeConverterOptions?.Formats ?? new[] { "0.00" };
            if (format.Length > 0)
            {
                string[] decimals = format[0].Split('.');
                if (decimals.Length == 2)
                {
                    return decimals[1].Length;
                }
            }

            return 2;
        }

        private string PadDecimal(decimal value, int decimalPoints)
        {
            string valueStr = value.ToString(CultureInfo.InvariantCulture);
            int decimalPointPos = valueStr.IndexOf('.');
            int actualDecimalPoints = 0;
            if (decimalPointPos > -1)
            {
                actualDecimalPoints = valueStr.Length - (decimalPointPos + 1);
            }
            else
            {
                valueStr += ".";
            }

            return valueStr + new string('0', decimalPoints - actualDecimalPoints);
        }
    }
}
