using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Predictions
{
    /// <summary>
    /// Base class for decision algorythms. To be improved when i'll add more of them.
    /// </summary>
    class DecisionAlgotyhm
    {
        public Wallet wallet { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime currentDate { get; set; }
        public DecisionAlgotyhm()
        {
            startDate = new DateTime(2014, 9, 13);
            endDate = new DateTime(2019, 7, 12);
            currentDate = startDate;
            wallet = new Wallet(100000);
        }
    }
}
