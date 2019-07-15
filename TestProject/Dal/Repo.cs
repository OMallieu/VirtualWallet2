using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.DataObjects;

namespace VirtualWallet
{
    class Repo
    {
        public static Company GetCompany(string psymbol)
        {
            using (var ctx = new VirtualWalletContext())
            {
                var companyQuery = ctx.Companies.Where(c => c.symbol == psymbol).Include("dayOfTrades");
                if (companyQuery.Count() > 1)
                {
                    throw new Exception("Different companies found for the same symbol");
                }
                else if (companyQuery.Count() == 1)
                {
                    return companyQuery.First();
                }
                else
                {
                    throw new Exception("Company not found");
                }
            }
        }
        /// <summary>
        /// Get all companies from db without history loaded
        /// </summary>
        /// <returns></returns>
        public static List<Company> GetAllCompanies()
        {
            using (var ctx = new VirtualWalletContext())
            {
                return ctx.Companies.ToList();
            }
        }
        /// <summary>
        /// Loads a list of companies into DB
        /// </summary>
        /// <param name="pcompanies"></param>
        public static void LoadCompaniesHystoryInDB(List<Company> pcompanies)
        {
            using (var ctx = new VirtualWalletContext())
            {
                foreach (var comp in pcompanies)
                {
                    if (comp != null)
                    {
                        comp.LoadEntityReq();
                        ctx.Companies.Add(comp);
                    }
                }
                //Stopwatch watch2 = new Stopwatch();
                //watch2.Start();
                ctx.SaveChanges();
                //Console.WriteLine(watch2.Elapsed);
            }
        }
    }
}
