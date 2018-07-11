using System;

using Crawler.ICBit;

namespace Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            var tmp = ICBitCrawler.Instance.GetTransactionInfo("395e1f1b8d9e1c26bb61e0ac8b6bed498889ce4898d7fc3bd21fa8218f542fbf");
            Console.WriteLine("Hello World!");
        }
    }
}
