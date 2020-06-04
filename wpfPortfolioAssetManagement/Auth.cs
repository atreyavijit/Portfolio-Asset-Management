using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using BCrypt;

namespace wpfPortfolioAssetManagement
{
    public class Auth
    {
        public static int Id { get; set; }
        public static bool IsLogged { get; set; }
        public static string Username { get; set; }
        public static string Email { get; set; }

        public static void SetLogin(int id, string email)
        {
            Id = id;
            Email = email;
            IsLogged = true;
        }
         //logout function
         public static void ResetLogin()
        {
            Id = 0;
            Email = "";
            IsLogged = false;
        }
        public static bool Login(string email, string password)
        {
            if (Globals.Db.Login(email, password))
                return true;

            return false;
        }

        public static bool Register(string email, string password)
        {
            string passHash = BCrypt.Net.BCrypt.HashPassword(password, 12, false);

            if (Globals.Db.Register(email, passHash))
                return true;

            return false;
        }

    }
}
