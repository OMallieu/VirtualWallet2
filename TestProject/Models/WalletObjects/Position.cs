using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.WalletObjects;

namespace VirtualWallet
{
    class Position
    {
        [Key]
        public int PositionID { get; set; }
        public string companySymbol { get; set; }
        public int quantity { get; set; }
        /// <summary>
        /// Allows to keep track of stock performance when the position is modified
        /// </summary>
        public double averageBuyingPrice { get; set; }
        public DateTime openingDate { get; set; }
        public List<Order> orderHystory { get; set; }

        public Position(string pcompanySymbol,DateTime pdate)
        {
            companySymbol = pcompanySymbol;
            quantity = 0;
            averageBuyingPrice = 0;
            openingDate = pdate;
        }
       
        public void Update(int pquantity, double pprice)
        {
            if (pquantity >= 0)
            {
                averageBuyingPrice = (averageBuyingPrice * quantity + pprice * pquantity) / (quantity + pquantity);
                quantity += pquantity;
            }
            else
            {
                if (-pquantity > quantity)
                {
                    throw new Exception("sell amount>quantity in wallet");
                }
                else
                {
                    quantity += pquantity;
                }
            }
        }
    }
}
