using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Predictions
{
    /// <summary>
    /// Example for decision algorythms. Each year, sells all stocks in the wallet, then invest 10% of the available capital in each of the 10 companies with the best score of Model1.
    /// </summary>
    class DecisionAlgorythm1 : DecisionAlgotyhm
    {
        /// <summary>
        /// Manages the wallet through history
        /// </summary>
        public void TestModel()
        {
            List<Prediction> bestPredictions;
            while (currentDate < endDate)
            {
                bestPredictions = Model1.MakepredictionsForOneDay(currentDate).Take(10).ToList(); //needs decoupling from the model (to do)
                Console.WriteLine(wallet.currentCash);
                wallet.SellAllPositions(currentDate);
                double cashAllocated = wallet.currentCash / 10;            
                foreach (var pred in bestPredictions)
                {
                    int quantity = wallet.GetQuantityFromCash(pred.companySymbol, cashAllocated, currentDate);
                    wallet.BuyStock(pred.companySymbol, quantity, currentDate);
                }
                currentDate= currentDate.AddYears(1);
            }
        }
    }
}
