using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public LoginDialog(Window owner)
        {
            Owner = owner;
            InitializeComponent();
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            string loginMessage = "";
            if(tbLoginEmail.Text == "")
            {
                loginMessage += "Please provide your email address." + Environment.NewLine;
            }
            if (!Regex.IsMatch(tbLoginEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)) //regex for email from google search
            {
                loginMessage += "The email address you entered is invalid." + Environment.NewLine;
            }
            if (tbLoginPassword.Password == "")
            {
                loginMessage += "Please provide your password." + Environment.NewLine;
            }
            if(loginMessage != "")
            {
                MessageBox.Show(loginMessage, "Portfolio Asset Management System", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //try to select email and passwrod for the email address provided by the user
            string email = tbLoginEmail.Text;
            string password = tbLoginPassword.Password;
            try
            {
                if (Auth.Login(email, password))
                {
                    MessageBox.Show("You are logged in successfully");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("The email or password you provided is wrong");
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error executing SQL query:\n" + ex.Message, "Portfolio Asset Management System", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
