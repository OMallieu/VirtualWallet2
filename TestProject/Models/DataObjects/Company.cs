using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.DataObjects
{
    class Company
    {
        [Key]
        public int CompanyID { get; set; }
        //public List<DayOfTrade> HistoricalStockPrices { get; set; }
        public string symbol { get; set; }
        public DayOfTrade[] historical { get; set; }
        public ICollection<DayOfTrade> dayOfTrades { get; set; }

        public void LoadEntityReq()
        {
            dayOfTrades = historical;
        }
        ///<summary>Get the stock price (at opening) for a specific date, returns -1 if there is no price data for that date </summary>
        public double GetPrice(DateTime pdate)
        {

            var priceQuery = dayOfTrades.Where(dot => dot.date == pdate);
            if (priceQuery.Count() > 1)
            {
                throw new Exception("different prices found for same date");
            }
            else if (priceQuery.Count() == 1)
            {
                return priceQuery.First().open;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Get the stock price (at opening) for a date.
        /// If there is none, get the price of the closest next date available, within one week.
        /// If a price still cannot be found, throws an exception.
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public double GetPriceWithDayMargin(DateTime pdate)
        {
            double price = -1;
            int counter = 0;
            while (price == -1 & counter < 7)
            {
                price = GetPrice(pdate);
                pdate = pdate.AddDays(1);
                counter++;
            }
            if (price == -1)
            {
                throw new Exception("price not found");
            }
            return price;
        }
    }
}
