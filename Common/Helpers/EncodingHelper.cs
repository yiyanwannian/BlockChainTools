using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    /// <summary>
    /// Encoding Helper
    /// </summary>
    public static class EncodingHelper
    {
        /// <summary>
        /// Unicodes to GB2312.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string UnicodeToGb2312(this string content)
        {
            var matches = Regex.Matches(content, "\\\\u([\\w]{4})");

            if (matches == null || matches.Count == 0)
            {
                return content;
            }

            foreach (Match match in matches)
            {
                string word = match.Value.Substring(2);
                byte[] codes = new byte[2];
                int code = Convert.ToInt32(word.Substring(0, 2), 16);
                int code2 = Convert.ToInt32(word.Substring(2), 16);
                codes[0] = (byte)code2;
                codes[1] = (byte)code;
                content = content.Replace(match.Value, Encoding.Unicode.GetString(codes));
            }

            return content;
        }
    }
}
