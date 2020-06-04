using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace Wpf.CartesianChart.PointShapeLine
{
    public partial class AssetsChart : UserControl
    {

        public AssetsChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            //XAxisLabels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };
            //YFormatter = value => value.ToString("C");
            DataContext = this;
        }

        public static SeriesCollection SeriesCollection { get; set; }
        public static string[] XAxisLabels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public static void addChartToSeriesColllection(LineSeries series)
        {
            SeriesCollection.Add(series);
        }
        public static void removeChartFromSeriesCollection(LineSeries series)
        {
            SeriesCollection.Remove(series);
        }
        public static void clearChart()
        {
            SeriesCollection.Clear();
        }
    }
}