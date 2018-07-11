using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Models.ICBit
{
    /// <summary>
    /// Input Addresses class
    /// </summary>
    public class Transfer
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Addresses { get; set; }

        /// <summary>
        /// Gets or sets the (ICB) amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }
    }
}
