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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration(Window owner)
        {
            Owner = owner;
            InitializeComponent();
        }

        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {
            string registerMessage = "";
            if (tbRegisterEmail.Text == "")
            {
                registerMessage += "Please provide your email address." + Environment.NewLine;
            }
            if (!Regex.IsMatch(tbRegisterEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)) //regex for email from google search
            {
                registerMessage += "The email address you entered is invalid." + Environment.NewLine;
            }
            if (tbRegisterPassword.Password == "")
            {
                registerMessage += "Please provide your password." + Environment.NewLine;
            }
            if (tbRegisterPasswordAgain.Password == "")
            {
                registerMessage += "Please retype your password." + Environment.NewLine;
            }
            if (tbRegisterPassword.Password != tbRegisterPasswordAgain.Password)
            {
                registerMessage += "The passwords you entered don't match." + Environment.NewLine;
            }
            string email = tbRegisterEmail.Text;
            try
            {
                if (!Globals.Db.UserExists(email))
                {
                    //Username does not exist we create his account in database.
                    if(Auth.Register(tbRegisterEmail.Text, tbRegisterPassword.Password))
                    {
                        MessageBox.Show("Bravo! you are now a registered user\nUsername: "+tbRegisterEmail.Text);
                        DialogResult = true;
                    }
                }
                else
                {
                    registerMessage += "A user with the specified email address already exists. Try a new email address" + Environment.NewLine;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error executing SQL query:\n" + ex.Message, "Portfolio Asset Management System", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (registerMessage != "")
            {
                MessageBox.Show(registerMessage, "Portfolio Asset Management System", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
