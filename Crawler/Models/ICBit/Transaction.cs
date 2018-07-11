using System;
using System.Collections.Generic;

namespace Crawler.Models.ICBit
{
    /// <summary>
    /// Transaction calss
    /// </summary>
    public class Transaction
    {
        public Transaction()
        {
            InputAddresses = new List<Transfer>();
            Recipients = new List<Transfer>();
        }

        /// <summary>
        /// The txid
        /// </summary>
        public string Txid { get; set; }

        /// <summary>
        /// The confirmations
        /// </summary>
        public int Confirmations { get; set; }

        /// <summary>
        /// Gets or sets the block hash.
        /// </summary>
        /// <value>
        /// The block hash.
        /// </value>
        public string BlockHash { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public long Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the input addresses.
        /// </summary>
        /// <value>
        /// The input addresses.
        /// </value>
        public List<Transfer> InputAddresses { get; set; }

        /// <summary>
        /// Gets or sets the recipients.
        /// </summary>
        /// <value>
        /// The recipients.
        /// </value>
        public List<Transfer> Recipients { get; set; }
    }
}
