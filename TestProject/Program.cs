using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.DataObjects;
using VirtualWallet.Predictions;

namespace VirtualWallet
{
    class Program
    {
        static void Main(string[] args)
        {
            FmpApiHandler.LoadAllCompaniesInDB(0,10);
            //Wallet myWallet = new Wallet();
            //myWallet.currentCash = 10000;
            //DateTime testdate = new DateTime(2014,6,17);
            //myWallet.BuyStock("SPi", 10, testdate);
            //myWallet.BuyStock("KMI", 5, testdate);
            //myWallet.BuyStock("SPY", -2, testdate);
            //myWallet.BuyStock("SPY", 10000, testdate);
            //foreach (var pos in myWallet.positions)
            //{
            //    Console.WriteLine(pos.companySymbol+"   "+pos.quantity+"  "+pos.averageBuyingPrice);
            //}
            //Console.WriteLine(myWallet.currentCash);
            //Model1Tester myTester = new Model1Tester();
            //myTester.TestModel();
            //Console.WriteLine(myTester.wallet.currentCash);
            Console.Read();

        }
    }
}
