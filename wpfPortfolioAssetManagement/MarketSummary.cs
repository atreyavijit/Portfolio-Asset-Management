using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPortfolioAssetManagement
{
    class MarketSummary
    {
        public int Id { get; set; }
        public string Language { get; set; } // "en-US"
        public string MarketPrice { get; set; } // 2932.47  
        public string Market { get; set; } // "us_market"
        public string Symbol { get; set; } // "^GSPC"
        public string FullExchangeName { get; set; } // "SNP"
        public string ShortExchangeName { get; set; } // "S&P 500"
        public string ExchangeTimeZone { get; set; } // "America/New_York"
        public string PreviousClose { get; set; } // "America/New_York"
        public string MarketTime { get; set; } // 1557176563
        public string MarketState { get; set; } // "POST"
        public string MarketChangePercent { get; set; } // "America/New_York"
        public string QuoteType { get; set; }
        public string FormattedPrice { get; set; }
        public string FormattedChangePercent { get; set; }
        public string FormattedTime { get; set; }



        public MarketSummary() { }
        public MarketSummary(string lang, string name, string marketPrice, string market, string symbol, string fullExName, string ShortExchangeName, string exTimeZone, string previousClose, string marketTime, string marketChangePercent, string formattedPrice, string formattedChangePercent, string formattedTime, string quoteType)
        {
            //exchangeTimezoneName

            this.Language = lang;
            this.MarketPrice = marketPrice;
            this.Market = market;
            this.Symbol = symbol;
            this.FullExchangeName = fullExName;
            this.ExchangeTimeZone = exTimeZone;
            this.PreviousClose = previousClose;
            this.MarketTime = marketTime;
            this.MarketChangePercent = marketChangePercent;
            this.FormattedPrice = formattedPrice;
            this.FormattedChangePercent = formattedChangePercent;
            this.FormattedTime = formattedTime;
            this.QuoteType = quoteType;
        }


    }
}
