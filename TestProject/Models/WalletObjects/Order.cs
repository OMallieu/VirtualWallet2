using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.WalletObjects
{
    /// <summary>
    /// Not used yet. Will be useful when an order hystory will be needed.
    /// </summary>
    class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime date { get; set; }
        public string companySymbol { get; set; }
        public int quantity { get; set; }
        public bool isABuyOrder { get; set; }
        public Order(string pSymbol, int pquantity, bool pisABuyOrder, DateTime pdate)
        {
            date = pdate;
            companySymbol = pSymbol;
            quantity = pquantity;
            date = pdate;
        }
        public Order(string pSymbol, int pquantity, bool pisABuyOrder):this(pSymbol,pquantity,pisABuyOrder,DateTime.Now)
        {
        }
    }
}
