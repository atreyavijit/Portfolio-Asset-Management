using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfPortfolioAssetManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Globals.Db = new Database();
            RefreshMenu();
            LoadList();
            FetchMarketHistory();
        }

        List<MarketSummary> lstAssets = new List<MarketSummary>();

        private void RefreshMenu()
        {
            if (Auth.IsLogged == false)
            {
                lbWelcome.Content = string.Format("Welcome! You are viewing as Guest User!!!");
                lbWelcome.Foreground = Brushes.Red;
                menuCompareAssets.IsEnabled = false;
                Portfolio_Menu.IsEnabled = false;
                Watchlist_Menu.IsEnabled = false;
                Login_Menu.IsEnabled = true;
                Logout_Menu.IsEnabled = false;
                Register_Menu.IsEnabled = true;
            }
            else
            {
                lbWelcome.Content = string.Format("Welcome "+Auth.Email+"!!!");
                lbWelcome.Foreground = Brushes.Green;
                menuCompareAssets.IsEnabled = true;
                Portfolio_Menu.IsEnabled = true;
                Watchlist_Menu.IsEnabled = true;
                Login_Menu.IsEnabled = false;
                Logout_Menu.IsEnabled = true;
                Register_Menu.IsEnabled = false;
            }
        }

        private void LoadList()
        {
            lstAssets = API.MarketSummary("US", "en");
            lvAssets.ItemsSource = lstAssets;
            lvAssets.Items.Refresh();
        }

        private void FetchMarketHistory()
        {
            List <string> distinctWatchListSysmbols = Globals.Db.GetWatchlistDistinctSymbols();
            List<MarketHistory> marketHistory = new List<MarketHistory>();
            if(distinctWatchListSysmbols.Count > 0)
            {
                foreach(string symbol in distinctWatchListSysmbols)
                {
                    marketHistory = API.SaveMarketHistory(symbol);
                }
            }
        }

        private void ReloadList()
        {
            try
            {
                List<MarketSummary> list = new List<MarketSummary>();

                //List<Todo> list = Globals.Db.GetAllTodos(TaskSortOrder.ToString());
                foreach (MarketSummary market in lstAssets)
                {
                    list.Add(market);
                }
                //TODO integrate getting sort types?

                //TODO -- FINISH INTEGRATION WITH XAML TO SORT BY HEADER TAG

                switch (AssetSortOrder)
                {
                    case EAssetSortOrder.Id:
                        list = list.OrderBy(t => t.Id).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.FullExchangeName:
                        list = list.OrderBy(t => t.FullExchangeName).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.ExchangeTimeZone:
                        list = list.OrderBy(t => t.ExchangeTimeZone).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.Language:
                        list = list.OrderBy(t => t.Language).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.Market:
                        list = list.OrderBy(t => t.Market).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.MarketChangePercent:
                        list = list.OrderBy(t => t.MarketChangePercent).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.MarketPrice:
                        list = list.OrderBy(t => t.MarketPrice).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.MarketTime:
                        list = list.OrderBy(t => t.MarketTime).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.PreviousClose:
                        list = list.OrderBy(t => t.PreviousClose).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.QuoteType:
                        list = list.OrderBy(t => t.QuoteType).ToList<MarketSummary>();
                        break;
                    case EAssetSortOrder.Symbol:
                        list = list.OrderBy(t => t.Symbol).ToList<MarketSummary>();
                        break;

                    default:
                        MessageBox.Show("Internal error: unknown sort column",
                        "Portfolio Asset Management Database", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }

                lstAssets.Clear();
                foreach (MarketSummary p in list)
                {
                    lstAssets.Add(p);
                }
                lvAssets.Items.Refresh();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error executing SQL query:\n" + ex.Message,
                    "Portfolio Asset Management Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Search_MenuClick(object sender, RoutedEventArgs e)
        {
            AssetSearch tmpSearchWindow = new AssetSearch();

            tmpSearchWindow.Show();
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void CompareAssets_MenuClick(object sender, RoutedEventArgs e)
        {
            if (Globals.Db.GetWatchlist() == null)
            {
                MessageBox.Show("There are no items in your watchlist to compare yet.");
                return;
            }
            AssetCompareWindow tmpAssetWindow = new AssetCompareWindow();
            tmpAssetWindow.Show();

        }

        private void TbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {


        }

        private void TbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                var list = from a in lstAssets
                           where a.FullExchangeName.ToLower().StartsWith(tbSearch.Text.ToLower()) || a.Symbol.ToLower().StartsWith(tbSearch.Text.ToLower())
                           select a;

                if (list.Count() > 0)
                {
                    lvAssets.ItemsSource = list;
                    lvAssets.Items.Refresh();
                    return;
                }
            }
            else
            {
                lvAssets.ItemsSource = lstAssets;
                lvAssets.Items.Refresh();

            }
        }

        enum EAssetSortOrder { Id, FullExchangeName, ExchangeTimeZone, Language, Market, MarketChangePercent, MarketPrice, MarketTime, PreviousClose, QuoteType, Symbol };

        EAssetSortOrder AssetSortOrder = EAssetSortOrder.Id;

        private void LvAssets_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked == null) { return; }
            switch (headerClicked.Tag)
            {
                case "Id":
                    AssetSortOrder = EAssetSortOrder.Id;
                    break;
                case "FullExchangeName":
                    AssetSortOrder = EAssetSortOrder.FullExchangeName;
                    break;
                case "ExchangeTimeZone":
                    AssetSortOrder = EAssetSortOrder.ExchangeTimeZone;
                    break;
                case "Language":
                    AssetSortOrder = EAssetSortOrder.Language;
                    break;
                case "Market":
                    AssetSortOrder = EAssetSortOrder.Market;
                    break;
                case "MarketChangePercent":
                    AssetSortOrder = EAssetSortOrder.MarketChangePercent;
                    break;
                case "MarketPrice":
                    AssetSortOrder = EAssetSortOrder.MarketPrice;
                    break;
                case "MarketTime":
                    AssetSortOrder = EAssetSortOrder.MarketTime;
                    break;
                case "PreviousClose":
                    AssetSortOrder = EAssetSortOrder.PreviousClose;
                    break;
                case "QuoteType":
                    AssetSortOrder = EAssetSortOrder.QuoteType;
                    break;
                case "Symbol":
                    AssetSortOrder = EAssetSortOrder.Symbol;
                    break;

                default:
                    MessageBox.Show("Internal error: unknown sort column",
                        "Portfolio Asset Management Database", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            ReloadList();
        }

        private void Login_MenuClick(object sender, RoutedEventArgs e)
        {
            LoginDialog loginForm = new LoginDialog(this);
            if(loginForm.ShowDialog() ==true)
            {
                RefreshMenu();
            }
        }
        private void Register_MenuClick(object sender, RoutedEventArgs e)
        {
            Registration registrationForm = new Registration(this);
            registrationForm.ShowDialog();
        }

        private void Logout_MenuClick(object sender, RoutedEventArgs e)
        {
            Auth.ResetLogin();
            RefreshMenu();
        }

        private void Portfolio_MenuClick(object sender, RoutedEventArgs e)
        {
            if(Globals.Db.GetPortfolio() == null)
            {
                MessageBox.Show("There are no items in your portfolio yet.");
                return;
            }
            MyPortfolio tmpPort = new MyPortfolio();
            tmpPort.Show();
        }

        private void Watchlist_MenuClick(object sender, RoutedEventArgs e)
        {
            if (Globals.Db.GetWatchlist() == null)
            {
                MessageBox.Show("There are no items in your watchlist to yet.");
                return;
            }
            Watchlist tmpWatchlist = new Watchlist();
            tmpWatchlist.Show();
        }

        private void CtxMenu_AddToWatchlist_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
