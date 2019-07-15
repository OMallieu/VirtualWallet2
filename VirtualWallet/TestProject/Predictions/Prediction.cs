using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Predictions
{
    class Prediction
    {
        public string companySymbol { get; set; }
        public double score { get; set; }
        public Prediction(string psymbol, double pscore)
        {
            companySymbol = psymbol;
            score = pscore;

        }
    }
}
