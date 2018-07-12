using System;
using System.Collections.Generic;
using System.Xml;

using Common.Helpers;
using Crawler.Models.ICBit;

namespace Crawler.ICBit
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionPraser
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        private string Content { get; set; }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        private XmlDocument Document { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionPraser"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public TransactionPraser(string content)
        {
            Content = content;
            Document = new XmlDocument();
            Document.LoadXml(content);
        }

        /// <summary>
        /// Prases the txid.
        /// </summary>
        /// <returns></returns>
        public string PraseTxid()
        {
            var txid = string.Empty;
            var node = Document.DocumentElement.SelectSingleNode("//div[@class='panel-heading hidden-xs']");
            var text = node.FirstChild.InnerText;

            if (string.IsNullOrEmpty(text))
            {
                return txid;
            }

            return text.Split(':')[1].Replace(" ", string.Empty);
        }

        /// <summary>
        /// Prases the confirmations.
        /// </summary>
        /// <returns></returns>
        public int PraseConfirmations()
        {
            var text = GetTableNodes()[0].ChildNodes[1].FirstChild.FirstChild.InnerText;

            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            return int.Parse(text);
        }

        /// <summary>
        /// Prases the block hash.
        /// </summary>
        /// <returns></returns>
        public string PraseBlockHash()
        {
            return GetTableNodes()[0].ChildNodes[1].FirstChild.ChildNodes[1].FirstChild.InnerText ?? string.Empty;
        }

        /// <summary>
        /// Prases the timestamp.
        /// </summary>
        /// <returns></returns>
        public long PraseTimestamp()
        {
            var text = GetTableNodes()[0].ChildNodes[1].FirstChild.ChildNodes[2].InnerText;

            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            return text.ICBitToDateTime().ToTimeStamp();
        }

        /// <summary>
        /// Prases the input addresses.
        /// </summary>
        /// <returns></returns>
        public List<Transfer> PraseInputAddresses()
        {
            var transfers = new List<Transfer>();
            var node = GetTableNodes()[1];
            return node.InnerText.Contains(Constants.ICBit.NewCoins) ? PraseNewCoins(node) : PraseTransfers(node);
        }

        /// <summary>
        /// Prases the recipients.
        /// </summary>
        /// <returns></returns>
        public List<Transfer> PraseRecipients()
        {
            return PraseTransfers(GetTableNodes()[2]);
        }

        /// <summary>
        /// Gets the table node.
        /// </summary>
        /// <returns></returns>
        private XmlNodeList GetTableNodes()
        {
            return Document.DocumentElement.SelectNodes("//table[@class='table table-bordered table-striped summary-table']");
        }

        /// <summary>
        /// Prases the new coins.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private List<Transfer> PraseNewCoins(XmlNode node)
        {
            var transfers = new List<Transfer>();

            var text = node.ChildNodes[1].FirstChild.FirstChild.InnerText;
            if (string.IsNullOrEmpty(text))
            {
                return transfers;
            }

            transfers.Add(new Transfer
            {
                Addresses = Constants.ICBit.NewCoins,
                Amount = 0
            });

            return transfers;
        }

        /// <summary>
        /// Prases the transfers.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private List<Transfer> PraseTransfers(XmlNode node)
        {
            var transfers = new List<Transfer>();
            var childNodes = node.ChildNodes[1].ChildNodes;

            for (var i = 0; i < childNodes.Count; i += 2)
            {
                var transfer = new Transfer
                {
                    Addresses = childNodes[i].ChildNodes[0].FirstChild.InnerText,
                    Amount = decimal.Parse(childNodes[i].ChildNodes[1].InnerText)
                };

                transfers.Add(transfer);
            }

            return transfers;
        }
    }
}
