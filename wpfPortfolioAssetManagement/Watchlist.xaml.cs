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

namespace wpfPortfolioAssetManagement
{
    /// <summary>
    /// Interaction logic for Watchlist.xaml
    /// </summary>
    public partial class Watchlist : Window
    {
        public Watchlist()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<WatchList> tmpWatchlist = Globals.Db.GetWatchlist();


            lvWatchlist.ItemsSource = tmpWatchlist;
            lvWatchlist.Items.Refresh();
            

        }

        private void LvWatchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WatchList item = lvWatchlist.SelectedItem as WatchList;
            if (item == null)
                return;

            lblSymbolAssign.Content = item.Symbol;
            lblNameAssign.Content = item.Name;
            lblDateAssign.Content = item.Date;
            lblOpenPrice.Content = item.OpenPrice;
            lblHighPrice.Content = item.HighPrice;
            lblLowPrice.Content = item.LowPrice;
        }
    }
}
