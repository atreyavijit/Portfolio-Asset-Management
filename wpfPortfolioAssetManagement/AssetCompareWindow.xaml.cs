using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.CartesianChart.PointShapeLine;

namespace wpfPortfolioAssetManagement
{
    /// <summary>
    /// Interaction logic for AssetCompareWindow.xaml
    /// </summary>
    public partial class AssetCompareWindow : Window
    {
        List<LineSeries> selectedSeries = new List<LineSeries>();
        List<LineSeries> openSeries = new List<LineSeries>();
        List<LineSeries> highSeries = new List<LineSeries>();
        List<LineSeries> lowSeries = new List<LineSeries>();
        List<LineSeries> closeSeries = new List<LineSeries>();
        List<LineSeries> volumeSeries = new List<LineSeries>();
        List<string> graphNames = new List<string>();
        public AssetCompareWindow()
        {
            InitializeComponent();
            List<WatchList> userWatchList = Globals.Db.GetWatchlist();
            foreach(WatchList wlItem in userWatchList)
            {
                string symbol = wlItem.Symbol;
                LineSeries open = new LineSeries();
                LineSeries high = new LineSeries();
                LineSeries low = new LineSeries();
                LineSeries close = new LineSeries();
                LineSeries volume = new LineSeries();

                //set titles to each line
                open.Title = string.Format(symbol + " (Open)");
                high.Title = string.Format(symbol + " (high)");
                low.Title = string.Format(symbol + " (low)");
                close.Title = string.Format(symbol + " (close)");
                volume.Title = string.Format(symbol + " (volume)");

                // values for each line
                open.Values = new ChartValues<double>();
                high.Values = new ChartValues<double>();
                low.Values = new ChartValues<double>();
                close.Values = new ChartValues<double>();
                volume.Values = new ChartValues<int>();

                List<MarketHistory> marketHistory = Globals.Db.GetMarketHistory(symbol);
                foreach(MarketHistory dailyData in marketHistory)
                {
                    open.Values.Add(dailyData.Open);
                    high.Values.Add(dailyData.High);
                    low.Values.Add(dailyData.Low);
                    close.Values.Add(dailyData.Close);
                    volume.Values.Add(dailyData.Volume);
                }
                openSeries.Add(open);
                highSeries.Add(high);
                lowSeries.Add(low);
                closeSeries.Add(close);
                volumeSeries.Add(volume);
            }
            
            selectedSeries = openSeries;
            //assign different colors to the lines
            Random rnd = new Random();
            for (int i = 0; i < selectedSeries.Count; i++)
            {
                selectedSeries[i].Stroke = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
                selectedSeries[i].Fill = Brushes.Transparent;
            }
            foreach (LineSeries line in selectedSeries)
            {
                graphNames.Add(line.Title);
            }
            lvLines.ItemsSource = graphNames;
            //set x-axis labels for chart
            List<DateTime> distinctDatesFromHistory = Globals.Db.GetAllDatesFromMarketHistory();
            List<string> xAxisLabels = new List<string>();
            foreach(DateTime date in distinctDatesFromHistory)
            {
                xAxisLabels.Add(string.Format("{0:00}/{1:00}", date.Month, date.Day));
            }
            AssetsChart.XAxisLabels = xAxisLabels.ToArray();
        }


        private void LvLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssetsChart.clearChart();
            for (int i = 0; i < graphNames.Count; i++)
            {
                if (lvLines.SelectedItems.Contains(graphNames[i]))
                {
                    AssetsChart.addChartToSeriesColllection(selectedSeries[i]);
                }
            }
        }

        private void Rb_Click(object sender, RoutedEventArgs e)
        {
            graphNames.Clear();
            AssetsChart.clearChart();
            if (rbOpen.IsChecked == true)
            {
                selectedSeries = openSeries;
            }
            else if (rbHigh.IsChecked == true)
            {
                selectedSeries = highSeries;
            }
            else if (rbLow.IsChecked == true)
            {
                selectedSeries = lowSeries;
            }
            else if (rbClose.IsChecked == true)
            {
                selectedSeries = closeSeries;
            }
            else if (rbVolume.IsChecked == true)
            {
                selectedSeries = volumeSeries;
            }
            Random rnd = new Random();
            for (int i = 0; i < selectedSeries.Count; i++)
            {
                selectedSeries[i].Stroke = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
                selectedSeries[i].Fill = Brushes.Transparent;
            }
            foreach (LineSeries line in selectedSeries)
            {
                graphNames.Add(line.Title);
            }
            lvLines.Items.Refresh();
            for (int i = 0; i < graphNames.Count; i++)
            {
                if (lvLines.SelectedItems.Contains(graphNames[i]))
                {
                    AssetsChart.addChartToSeriesColllection(selectedSeries[i]);
                }
            }
        }
    }
}
