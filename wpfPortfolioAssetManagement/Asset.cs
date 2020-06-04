using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPortfolioAssetManagement
{
    class Asset
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Type { get; set; }
        public string ExchangeDisp { get; set; }
        public string TypeDisp { get; set; }

        public Asset() { }
        public Asset(string symbol, string name, string exchange, string type, string exchangeDisp, string typeDisp)
        {
            this.Symbol = symbol;
            this.Name = name;
            this.Exchange = exchange;
            this.Type = type;
            this.ExchangeDisp = exchangeDisp;
            this.TypeDisp = typeDisp;
        }
    }
}
