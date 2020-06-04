using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using unirest_net.http;

namespace wpfPortfolioAssetManagement
{
    class API
    {

        public static List<MarketSummary> MarketSummary(string regionStr, string langStr)
        {
            List<MarketSummary> tmpAssets = new List<MarketSummary>();

            try
            {
                HttpResponse<string> response = Unirest.get(String.Format("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-summary?region={0}&lang={1}", langStr, regionStr))
                .header("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com")
                .header("X-RapidAPI-Key", "c29139a8a8msh6deca5f94d63208p106998jsnd1cc07420b07")
                .asJson<string>();
                if (response.Code != 200) return null;

                JObject jo = (JObject)JsonConvert.DeserializeObject(response.Body.ToString());
                JArray marketArray = (JArray)jo["marketSummaryResponse"]["result"];

                for (int i = 0; i < marketArray.Count(); i++)
                {
                    MarketSummary tmpMarket = new MarketSummary();
                    tmpMarket.Id = i;
                    tmpMarket.ExchangeTimeZone = jo["marketSummaryResponse"]["result"][i]["exchangeTimezoneName"].ToString();
                    tmpMarket.FullExchangeName = jo["marketSummaryResponse"]["result"][i]["fullExchangeName"].ToString();
                    tmpMarket.ShortExchangeName = (jo["marketSummaryResponse"]["result"][i]["shortName"] == null) ? "" : jo["marketSummaryResponse"]["result"][i]["shortName"].ToString();
                    tmpMarket.Symbol = jo["marketSummaryResponse"]["result"][i]["symbol"].ToString();
                    tmpMarket.Language = jo["marketSummaryResponse"]["result"][i]["language"].ToString();
                    tmpMarket.MarketChangePercent = jo["marketSummaryResponse"]["result"][i]["regularMarketChangePercent"]["raw"].ToString();
                    tmpMarket.FormattedChangePercent = jo["marketSummaryResponse"]["result"][i]["regularMarketChangePercent"]["fmt"].ToString();

                    tmpMarket.Market = jo["marketSummaryResponse"]["result"][i]["market"].ToString();
                    tmpMarket.MarketPrice = jo["marketSummaryResponse"]["result"][i]["regularMarketPrice"]["raw"].ToString();
                    tmpMarket.MarketState = (jo["marketSummaryResponse"]["result"][i]["marketState"] == null) ? "" : jo["marketSummaryResponse"]["result"][i]["marketState"].ToString();
                    tmpMarket.FormattedPrice = jo["marketSummaryResponse"]["result"][i]["regularMarketPrice"]["fmt"].ToString();
                    tmpMarket.MarketTime = jo["marketSummaryResponse"]["result"][i]["regularMarketTime"]["raw"].ToString();
                    tmpMarket.FormattedTime = jo["marketSummaryResponse"]["result"][i]["regularMarketTime"]["fmt"].ToString();

                    tmpMarket.PreviousClose = jo["marketSummaryResponse"]["result"][i]["regularMarketPreviousClose"]["raw"].ToString();

                    tmpMarket.QuoteType = jo["marketSummaryResponse"]["result"][i]["quoteType"].ToString();

                    tmpAssets.Add(tmpMarket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                Globals.Db.AddMarketAsset(tmpAssets);
            }
            return tmpAssets;

        }

        public static List<Asset> SearchSymbol(string langStr, string regionStr, string searchStr)
        {
            HttpResponse<string> response = Unirest.get(String.Format("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/auto-complete?lang={0}&region={1}&query={2}", langStr, regionStr, searchStr))
            .header("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com")
            .header("X-RapidAPI-Key", "c29139a8a8msh6deca5f94d63208p106998jsnd1cc07420b07")
            .asJson<string>();
            if (response.Code != 200) return null;

            JObject jo = (JObject)JsonConvert.DeserializeObject(response.Body.ToString());
            JArray dataArray = (JArray)jo["ResultSet"]["Result"];

            List<Asset> lstAssets = new List<Asset>();

            for (int i = 0; i < dataArray.Count(); i++)
            {
                string symbol = jo["ResultSet"]["Result"][i]["symbol"].ToString();
                string name = jo["ResultSet"]["Result"][i]["name"].ToString();
                string exchange = jo["ResultSet"]["Result"][i]["exch"].ToString();
                string type = jo["ResultSet"]["Result"][i]["type"].ToString();
                string exchangeDisp = jo["ResultSet"]["Result"][i]["exchDisp"].ToString();
                string typeDisp = jo["ResultSet"]["Result"][i]["typeDisp"].ToString();
                Asset tmpSearch = new Asset(symbol, name, exchange, type, exchangeDisp, typeDisp);
                lstAssets.Add(tmpSearch);
            }

            return lstAssets;
        }

        //for market history
        public static List<MarketHistory> SaveMarketHistory(string symbol)
        {
            const string API_KEY = "7I217NA0556LS5X8";
            List<MarketHistory> marketHistory = new List<MarketHistory>();
            try
            {
                HttpResponse<string> response = Unirest.get(String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={0}&apikey={1}", symbol, API_KEY)).asJson<string>();
                if (response.Code != 200) return null;

                JObject jMarketHistory = (JObject)JsonConvert.DeserializeObject(response.Body.ToString());
                DateTime dailyDate;
                dailyDate = DateTime.Now; //initialize today's date
                for (int i = 0; i < jMarketHistory["Time Series (Daily)"].Count(); )
                {
                    //Console.WriteLine(dailyDate.ToString("yyyy-MM-dd"));
                    if (jMarketHistory["Time Series (Daily)"][dailyDate.ToString("yyyy-MM-dd")] == null)
                    {
                        dailyDate = dailyDate.AddDays(-1);
                        continue;
                    }
                    MarketHistory dailyData = new MarketHistory();
                    dailyData.Open = double.Parse(jMarketHistory["Time Series (Daily)"][dailyDate.ToString("yyyy-MM-dd")]["1. open"].ToString());
                    dailyData.High = double.Parse(jMarketHistory["Time Series (Daily)"][dailyDate.ToString("yyyy-MM-dd")]["2. high"].ToString());
                    dailyData.Low = double.Parse(jMarketHistory["Time Series (Daily)"][dailyDate.ToString("yyyy-MM-dd")]["3. low"].ToString());
                    dailyData.Close = double.Parse(jMarketHistory["Time Series (Daily)"][dailyDate.ToString("yyyy-MM-dd")]["4. close"].ToString());
                    dailyData.Volume = int.Parse(jMarketHistory["Time Series (Daily)"][dailyDate.ToString("yyyy-MM-dd")]["5. volume"].ToString());
                    dailyData.Date = dailyDate;
                    dailyData.Symbol = symbol;
                    marketHistory.Add(dailyData);
                    dailyDate = dailyDate.AddDays(-1); //step back by one day after processing every record
                    i++;
                }
                marketHistory = marketHistory.OrderBy(t => t.Date).ToList<MarketHistory>();
                Globals.Db.AddToMarketHistory(symbol,marketHistory);
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return marketHistory;
        }
    }
}
