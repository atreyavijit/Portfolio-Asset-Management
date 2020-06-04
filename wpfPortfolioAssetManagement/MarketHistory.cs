using System;

namespace wpfPortfolioAssetManagement
{
    public class MarketHistory
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }




        public MarketHistory() { }
        public MarketHistory(DateTime date, double open, double high, double low,double close, int volume)
        {
            this.Date = date;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
            this.Volume = volume;
        }
    }
}