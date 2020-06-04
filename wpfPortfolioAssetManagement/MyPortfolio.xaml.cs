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
    /// Interaction logic for MyPortfolio.xaml
    /// </summary>
    public partial class MyPortfolio : Window
    {
        public MyPortfolio()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWatchlist();
            LoadPortfolio();
        }

        private void RefreshWatchlist()
        {
            List<WatchList> tmpWatchlist = Globals.Db.GetWatchlist();

            lvWatchlist.ItemsSource = tmpWatchlist;
            lvWatchlist.Items.Refresh();
        }

        private void LoadPortfolio()
        {
            List<Portfolio> tmpPortfolio = Globals.Db.GetPortfolio();

            lvPortfolio.ItemsSource = tmpPortfolio;
            lvPortfolio.Items.Refresh();
        }

        private void RefreshPortfolio()
        {
            List<Portfolio> tmpPortfolio = Globals.Portfolio;

            lvPortfolio.ItemsSource = tmpPortfolio;
            lvPortfolio.Items.Refresh();
        }

        private void LvWatchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WatchList item = lvWatchlist.SelectedItem as WatchList;

            if (item == null)
                return;

            tbMarketPrice.Text = Globals.Db.GetLastDateClosingValue(item.Symbol).ToString();
            tbSymbol.Text = item.Symbol;
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            WatchList item = lvWatchlist.SelectedItem as WatchList;

            if (item == null)
                return;

            Portfolio tmpPortfolio = new Portfolio();
            if (!int.TryParse(tbNumOfShares.Text, out int NumOfShares))
            {
                MessageBox.Show("Please input a valid number for shares", "Portfolio Asset Management System");
                return;
            }
            if (!double.TryParse(tbPurchasePrice.Text, out double PurchasePrice))
            {
                MessageBox.Show("Please input a valid number for Purchase Price", "Portfolio Asset Management System");
                return;
            }
            if (!double.TryParse(tbSellPrice.Text, out double SellPrice))
            {
                MessageBox.Show("Please input a valid number for Sell Price", "Portfolio Asset Management System");
                return;
            }
            if (!double.TryParse(tbMarketPrice.Text, out double ClosePrice))
            {
                ClosePrice = 0.00;
                //return;
            }
            tmpPortfolio.Symbol = tbSymbol.Text;
            tmpPortfolio.NumOfShares = NumOfShares;
            tmpPortfolio.PurchasePrice = PurchasePrice;
            tmpPortfolio.SellPrice = 0.00;
            tmpPortfolio.ClosePrice = ClosePrice;
            Globals.Db.AddPortfolio(tmpPortfolio);

            RefreshPortfolio();

            //tbMarketPrice.Text = Globals.Db.GetLastDateClosingValue(tmpPortfolio.Symbol).ToString();
            // tbSymbol.Text = item.Symbol;

        }
    }
}
