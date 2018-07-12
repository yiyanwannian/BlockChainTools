using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Common.Helpers
{
    public static class HttpHelper
    {
        /// <summary>
        /// Posts the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public static string Post(string url, string param, string contentType)
        {
            string responseContent = string.Empty;

            var request = WebRequest.Create(url);
            request.Timeout = 20000;
            request.Method = "POST";

            request.ContentType = contentType;
            var byteArray = Encoding.UTF8.GetBytes(param);
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream, Encoding.UTF8);
            responseContent = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseContent;
        }


        /// <summary>
        /// Posts the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="requestBody">The request body.</param>
        /// <returns></returns>
        public static string Post(WebRequest request, string requestBody)
        {
            string responseContent = string.Empty;

            request.Timeout = 20000;
            request.Method = "POST";
            var byteArray = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = byteArray.Length;

            var stream = request.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();

            var response = request.GetResponse();
            stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            responseContent = reader.ReadToEnd();
            reader.Close();

            stream.Close();
            response.Close();

            return responseContent;
        }

        /// <summary>
        /// Posts the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="requestBody">The request body.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string PostWithGzip(WebRequest request, string requestBody)
        {
            string responseContent = string.Empty;

            request.Timeout = 20000;
            request.Method = "POST";
            var byteArray = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = byteArray.Length;

            var stream = request.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();

            var response = request.GetResponse();
            using (var reponseStream = response.GetResponseStream())
            {
                GZipStream gzip = new GZipStream(reponseStream, CompressionMode.Decompress);
                using (StreamReader reader = new StreamReader(gzip, Encoding.Default))
                {
                    responseContent = reader.ReadToEnd();
                }
            }

            response.Close();

            return responseContent;
        }

        /// <summary>
        /// Gets the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string Get(WebRequest request)
        {
            var content = string.Empty;

            request.Timeout = 20000;
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                content = streamReader.ReadToEnd();
                //content = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                //int status = (int)response.StatusCode;
            }

            return content;
        }
    }
}
