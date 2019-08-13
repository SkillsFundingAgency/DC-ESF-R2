using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Utils
{
    public static class StringExtensions
    {
        public static bool CaseInsensitiveEquals(this string source, string data)
        {
            if (source == null && data == null)
            {
                return true;
            }

            return source?.Equals(data, StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public static bool CaseInsensitiveContains(this string source, string data)
        {
            if (source == null && data == null)
            {
                return true;
            }

            return source?.ToLower().Trim().Contains(data.ToLower().Trim()) ?? false;
        }

        public static bool ListCaseInsensitiveContains(this List<string> source, string data)
        {
            if (source == null && data == null)
            {
                return true;
            }

            foreach (var str in source)
            {
                return str?.CaseInsensitiveContains(data) == true;
            }

            return false;
        }
    }
}