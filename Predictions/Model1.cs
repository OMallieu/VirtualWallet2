using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.DataObjects;

namespace VirtualWallet.Predictions
{
    /// <summary>
    /// Example for models. Rates companies according to the performance of their stock in the last three months.  
    /// </summary>
    class Model1
    {
        /// <summary>
        /// Returns a list of predictions for all companies at a specific date, ordered by descending company score.
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public static List<Prediction> MakepredictionsForOneDay(DateTime pdate)
        {
            List<Prediction> predictions = new List<Prediction>();
            var allCompNotLoaded = Repo.GetAllCompanies();
            foreach (var compNotLoaded in allCompNotLoaded)
            {
                Company comp = Repo.GetCompany(compNotLoaded.symbol);
                double currentPrice = comp.GetPriceWithDayMargin(pdate);
                double previousPrice = comp.GetPriceWithDayMargin(pdate.AddMonths(-3));
                double percentChange = (currentPrice + previousPrice) / previousPrice;
                predictions.Add(new Prediction(comp.symbol, percentChange));
            }
            predictions = predictions.OrderByDescending (p => p.score).ToList();
            return predictions;

        }

    }

}
