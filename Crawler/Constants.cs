using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler
{
    /// <summary>
    /// constant strings
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// ICBit constant strings
        /// </summary>
        public class ICBit
        {
            /// <summary>
            /// The host
            /// </summary>
            public const string Host = "http://47.90.127.4:3001";

            /// <summary>
            /// The search
            /// </summary>
            public const string Search = "search";

            /// <summary>
            /// The get last TXS
            /// </summary>
            public const string GetLastTxs = "ext/getlasttxs/0.00000001?_={0}";

            /// <summary>
            /// The tx
            /// </summary>
            public const string Tx = "tx/{0}";

            /// <summary>
            /// The new coins
            /// </summary>
            public const string NewCoins = "New Coins";
        }
    }
}
