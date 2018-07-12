using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using Crawler.ICBit;
using Crawler.Models.ICBit;

namespace Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = new List<Transaction>();

            var heighest = ICBitCrawler.Instance.GetLastTxs().Max(tx => tx.blockindex);

            var size = Environment.ProcessorCount;
            var times = heighest / size + (heighest % size == 0 ? 0 : 1);

            for (int i = 0; i < times; i++)
            {
                var tasks = new List<Task>();
                for (int j = 1 + i * size; j <= (i + 1) * size; j++)
                {
                    if (j > heighest)
                    {
                        break;
                    }

                    var index = j;
                    tasks.Add(Task.Factory.StartNew(() => ICBitCrawler.Instance.LoadBatchTransactions(index, transactions)));
                }

                Task.WaitAll(tasks.ToArray());

                SerializeHelper.XmlSerialize(transactions, $"TransactionDatas/TransactionInfos_{i * size}~{(i + 1) * size}.xml");
            }

            //var tasks = new List<Task>();
            //for (int i = 1; i <= heighest; i++)
            //{
            //    var index = i;
            //    tasks.Add(Task.Factory.StartNew(() =>
            //    {
            //        ICBitCrawler.Instance.GetTransactionIds(index).ForEach(transactionId =>
            //        {
            //            transactions.Add(ICBitCrawler.Instance.GetTransactionInfo(transactionId));
            //            LogHelper.Info($"Loaded {index} transaction {transactionId} data");
            //        });
            //    }));
            //}

            //Task.WaitAll(tasks.ToArray());

            //SerializeHelper.XmlSerialize(transactions, "TransactionInfos.xml");

            //var tmp = ICBitCrawler.Instance.GetTransactionInfo("3eb232bb61c8c109aaadaf1b23c4476158b3e9b7563b32791b6e559ab7755759");
            //var tmp = ICBitCrawler.Instance.GetTransactionInfo("395e1f1b8d9e1c26bb61e0ac8b6bed498889ce4898d7fc3bd21fa8218f542fbf");
            Console.WriteLine("Hello World!");
        }
    }
}
