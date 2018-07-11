using System.Collections.Generic;

namespace Crawler.Models.ICBit
{
    /// <summary>
    /// Request tx info for
    /// GET /ext/getlasttxs/0.00000001?_=1531302405938 HTTP/1.1
    /// </summary>
    public class RequestTxInfos
    {
        public RequestTxInfos()
        {
            data = new List<RequestTxInfo>();
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<RequestTxInfo> data { get; set; }
    }

    /// <summary>
    /// Request tx info
    /// </summary>
    public class RequestTxInfo
    {
        /// <summary>
        /// Gets or sets the v.
        /// </summary>
        /// <value>
        /// The v.
        /// </value>
        public int __v { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string _id { get; set; }

        /// <summary>
        /// Gets or sets the blockhash.
        /// </summary>
        /// <value>
        /// The blockhash.
        /// </value>
        public string blockhash { get; set; }

        /// <summary>
        /// Gets or sets the blockindex.
        /// </summary>
        /// <value>
        /// The blockindex.
        /// </value>
        public int blockindex { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public long timestamp { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal total { get; set; }

        /// <summary>
        /// Gets or sets the txid.
        /// </summary>
        /// <value>
        /// The txid.
        /// </value>
        public string txid { get; set; }

        /// <summary>
        /// Gets or sets the vin.
        /// </summary>
        /// <value>
        /// The vin.
        /// </value>
        public List<Transfer> vin { get; set; }

        /// <summary>
        /// Gets or sets the vout.
        /// </summary>
        /// <value>
        /// The vout.
        /// </value>
        public List<Transfer> vout { get; set; }

    }
}
