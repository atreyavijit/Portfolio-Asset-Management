using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPortfolioAssetManagement
{
    class Portfolio
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public int NumOfShares { get; set; }

        public double PurchasePrice { get; set; }

        public double SellPrice { get; set; }

        public double ClosePrice { get; set; }
        public override string ToString()
        {
            return String.Format("[{1}] {0}", Symbol, NumOfShares);
        }

    }
}
