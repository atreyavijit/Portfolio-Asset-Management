using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPortfolioAssetManagement
{
    class WatchList
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double OpenPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double ClosePrice { get; set; }
        public int Volume { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Symbol, Name);
        }
    }
}
