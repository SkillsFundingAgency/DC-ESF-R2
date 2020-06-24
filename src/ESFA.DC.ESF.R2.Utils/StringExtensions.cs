using System;
using System.Text.RegularExpressions;

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

        public static string RemoveWhiteSpacesNonAlphaNumericCharacters(this string str)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9+-.]");
            str = rgx.Replace(str, $"");
            return str.Trim();
        }

        public static string[] SplitFileName(this string fileName, string extension)
        {
            const int lengthOfDateTimePart = 15;
            const int ukPrnLength = 8;

            fileName = Regex.Replace(fileName, extension, string.Empty, RegexOptions.IgnoreCase);

            if (fileName.Contains("/"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);
            }

            var parts = new string[5];
            parts[0] = fileName.Substring(0, fileName.IndexOf('-'));
            parts[1] = fileName.Substring(parts[0].Length + 1, ukPrnLength);

            var startOfConRefNumIndex = parts[0].Length + parts[1].Length + 2;
            var lengthOfConRefNum = (fileName.Length - lengthOfDateTimePart - 1) - startOfConRefNumIndex;
            parts[2] = fileName.Substring(startOfConRefNumIndex, lengthOfConRefNum);

            var dateTimePart = fileName.Substring(fileName.Length - lengthOfDateTimePart);
            parts[3] = dateTimePart.Substring(0, dateTimePart.IndexOf('-'));
            parts[4] = dateTimePart.Substring(parts[3].Length + 1);

            return parts;
        }
    }
}