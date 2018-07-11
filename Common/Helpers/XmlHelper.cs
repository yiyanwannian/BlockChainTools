using System.Net;
using System.Web;

namespace Common.Helpers
{
    /// <summary>
    /// Xml helper
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// The formatter URL
        /// </summary>
        public const string FormatterUrl = "http://tool.oschina.net/action/format/";

        /// <summary>
        /// HTMLs the formatter.
        /// </summary>
        /// <param name="htmlContent">Content of the HTML.</param>
        /// <returns></returns>
        public static string HtmlFormatter(this string htmlContent)
        {
            var request = FormatterRequestCreator($"{FormatterUrl}html");
            var response = HttpHelper.PostWithGzip(request, $"html={HttpUtility.UrlEncode(htmlContent)}");

            var content = response.Replace("{\"fhtml\":\"", string.Empty).Replace("\"}", string.Empty);
            return content.UnicodeToGb2312().Replace("\\n", string.Empty).Replace("\\\"", "\"");
        }

        /// <summary>
        /// Formatters the request creator.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static WebRequest FormatterRequestCreator(string url)
        {
            var request = WebRequest.Create(url);

            request.Headers.Add("Connection: keep-alive");
            request.Headers.Add("Accept: application/json, text/javascript, */*; q=0.01");
            request.Headers.Add("X-Requested-With: XMLHttpRequest");
            request.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
            request.Headers.Add("Content-Type: application/x-www-form-urlencoded; charset=UTF-8");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.Headers.Add("Accept-Language: zh-CN,zh;q=0.9");

            return request;
        }
    }
}
