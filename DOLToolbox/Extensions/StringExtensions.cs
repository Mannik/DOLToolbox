using System.Text.RegularExpressions;

namespace DOLToolbox.Extensions
{
    public static class StringExtensions
    {
        public static string ToWildcardRegex(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            return Regex.Escape(input)
                .Replace(@"\*", ".*")
                .Replace(@"\?", ".");
        }
    }
}