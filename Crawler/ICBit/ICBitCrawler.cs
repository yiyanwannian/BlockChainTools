using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

using Common.Helpers;
using Crawler.Models.ICBit;
using System.Xml;

namespace Crawler.ICBit
{
    public class ICBitCrawler
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static ICBitCrawler instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="ICBitCrawler"/> class from being created.
        /// </summary>
        private ICBitCrawler()
        {

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ICBitCrawler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ICBitCrawler();
                }

                return instance;
            }
        }

        /// <summary>
        /// Ics the bit HTTP request creator.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private WebRequest HttpRequestCreator(string url)
        {
            var request = WebRequest.Create(url);

            request.Headers.Add("Connection: keep-alive");
            request.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            request.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.Headers.Add("Accept-Language: zh-CN,zh;q=0.9");

            return request;
        }

        /// <summary>
        /// Gets the block information by height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public string GetBlockContentByheight(int height)
        {
            var url = $"{Constants.ICBit.Host}/{Constants.ICBit.Search}";
            return HttpHelper.Post(HttpRequestCreator(url), $"search={height}");
        }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public List<string> GetTransactionIds(int height)
        {
            var transactionIds = new List<string>();

            var matchs = Regex.Matches(
                GetBlockContentByheight(height),
                "<a href=\"/tx/[0-9a-z]*\">");

            foreach (Match match in matchs)
            {
                var transactionId = match.Value.Replace("<a href=\"/tx/", string.Empty)
                    .Replace("\">", string.Empty);

                transactionIds.Add(transactionId);
            }

            return transactionIds.Distinct().ToList();
        }

        /// <summary>
        /// Gets the transaction information.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        public Transaction GetTransactionInfo(string transactionId)
        {
            var transaction = new Transaction();
            var url = $"{Constants.ICBit.Host}/{string.Format(Constants.ICBit.Tx, transactionId)}";
            var content = HttpHelper.Get(HttpRequestCreator(url)).HtmlFormatter();

            var document = new XmlDocument();
            document.LoadXml(content);


            return transaction;
        }

        /// <summary>
        /// Gets the last TXS.
        /// </summary>
        /// <returns></returns>
        public List<RequestTxInfo> GetLastTxs()
        {
            var timestamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            var url = $"{Constants.ICBit.Host}/{string.Format(Constants.ICBit.GetLastTxs, timestamp)}";
            var content = HttpHelper.Get(HttpRequestCreator(url));

            var requestTxInfos = SerializeHelper.JsonDeserialize<RequestTxInfos>(content) ?? new RequestTxInfos();

            return requestTxInfos.data;
        }
    }
}
