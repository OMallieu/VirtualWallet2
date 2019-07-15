using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.DataObjects;

namespace VirtualWallet
{
    class Wallet
    {
        [Key]
        public int WalletID { get; set; }
        public string name { get; set; }
        public double startingCash { get; set; }
        public double currentCash { get; set; }
        public List<Position> positions { get; set; }
        public Wallet(double pstartingCash)
        {
            startingCash = pstartingCash;
            currentCash = pstartingCash;
            positions = new List<Position>();
        }
        /// <summary>
        /// Returns current cash + sum of the value of all positions
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public double GetValue(DateTime pdate)
        {
            double value = currentCash;
            foreach (var pos in positions)
            {
                Company myComp = Repo.GetCompany(pos.companySymbol);
                value += pos.quantity * myComp.GetPriceWithDayMargin(pdate);
            }
            return value;
        }
        /// <summary>
        /// Gets the maximum number of stocks that can be bought with an amount of money.
        /// </summary>
        /// <param name="psymbol"></param>
        /// <param name="pcash"></param>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public int GetQuantityFromCash(string psymbol, double pcash, DateTime pdate)
        {
            Company myComp = Repo.GetCompany(psymbol);
            double price = myComp.GetPriceWithDayMargin(pdate);
            int quantity = Convert.ToInt32(Math.Floor(pcash / price));
            return quantity;
        }
        public void SellAllPositions(DateTime pdate)
        {
            while (positions.Count() > 0)
            {
                Company myComp = Repo.GetCompany(positions.First().companySymbol);
                currentCash += positions.First().quantity * myComp.GetPriceWithDayMargin(pdate);
                positions.RemoveAt(0);
            }
        }
        /// <summary>
        /// Enter a negative quantity to sell stocks.
        /// </summary>
        /// <param name="psymbol"></param>
        /// <param name="pquantity"></param>
        /// <param name="pdate"></param>
        public void BuyStock(string psymbol, int pquantity, DateTime pdate)
        {
            Company myComp = Repo.GetCompany(psymbol);
            double price = myComp.GetPriceWithDayMargin(pdate);
            double orderBill = price * pquantity;
            var positionQuery = positions.Where(p => p.companySymbol == psymbol);
            if (positionQuery.Count() > 0)
            {
                if (true)
                {

                }
                positions.Where(p => p.companySymbol == psymbol).First().Update(pquantity, price);
                if (positions.Where(p => p.companySymbol == psymbol).First().quantity == 0)
                {
                    positions.RemoveAll(p => p.companySymbol == psymbol);
                }
            }
            else
            {
                if (pquantity >= 0)
                {
                    Position myPosition = new Position(psymbol, pdate);
                    myPosition.Update(pquantity, price);
                    positions.Add(myPosition);
                }
                else
                {
                    throw new Exception("Stocks not found in wallet");
                }

            }
            currentCash -= orderBill;

        }
    }
}
