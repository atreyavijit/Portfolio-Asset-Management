using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPortfolioAssetManagement
{
    class Globals
    {
        public static Database Db;
        public static List<Portfolio> Portfolio = new List<Portfolio>();
        public static List<WatchList> WatchList = new List<WatchList>();
        public string[] XAxisLabels;
    }
}
