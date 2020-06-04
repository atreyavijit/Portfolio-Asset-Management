using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using unirest_net.http;

namespace wpfPortfolioAssetManagement
{
    /// <summary>
    /// Interaction logic for AssetSearch.xaml
    /// </summary>
    public partial class AssetSearch : Window
    {
        public AssetSearch()
        {
            InitializeComponent();
            RefreshContextMenu();
        }

        private void RefreshContextMenu()
        {
            if (Auth.IsLogged == false)
            {
                Menu_WatchlistAdd.IsEnabled = false;
            }
            else
            {
                Menu_WatchlistAdd.IsEnabled = true;
            }
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            //MessageBox.Show(String.Format("Searching for string '{0}'", tbSearch.Text));

            ClearListView();
            List<Asset> tmpList = API.SearchSymbol(cmbLang.Text, cmbRegion.Text, tbSearch.Text);
            lvAssets.ItemsSource = tmpList;
            lvAssets.Items.Refresh();

            e.Handled = true;
        }


        private void ClearListView()
        {
            lstAssets.Clear();
            lvAssets.Items.Refresh();
        }

        private List<Asset> lstAssets = new List<Asset>();


        private void Menu_WatchlistAdd_Click(object sender, RoutedEventArgs e)
        {
            Asset tmpAsset = lvAssets.SelectedItem as Asset;
            if (tmpAsset != null && Auth.IsLogged)
            {
                Globals.Db.AddWatchlist(tmpAsset);
                API.SaveMarketHistory(tmpAsset.Symbol);
            }

        }
    }
}
