using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace wpfPortfolioAssetManagement
{
    class Database
    {
        const string DbConnectionString = @"Server=tcp:crypto-dev2.database.windows.net,1433;Initial Catalog=db;Persist Security Info=False;User ID=decrypt;Password=!!Encrypt0!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(DbConnectionString);
            conn.Open();
        }

        //function to check if the username already exists in the database or not
        public bool UserExists(string email)
        {
            SqlCommand cmdGetUserName = new SqlCommand("SELECT * FROM Users WHERE Email='" + email + "'", conn);
            using (SqlDataReader reader = cmdGetUserName.ExecuteReader())
            {
                if (reader.HasRows)
                    return true;

                return false;

            }
        }


        public bool Register(string email, string password)
        {
            SqlCommand cmdInsert = new SqlCommand("INSERT INTO Users (Email, Password) OUTPUT INSERTED.ID VALUES (@Email, @Password)", conn);
            cmdInsert.Parameters.AddWithValue("Email", email);
            cmdInsert.Parameters.AddWithValue("Password", password);
            int insertId = (int)cmdInsert.ExecuteScalar();

            Auth.SetLogin(insertId, email);

            return true;
        }
       
        public bool Login(string email, string password)
        {
            List<Auth> list = new List<Auth>();
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM Users WHERE Email='"+email+"'", conn);
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                    int id = (int)reader["Id"];
                    string user = (string)reader["Email"];
                    bool isLogged = false;
                    string passwordHash = (string)reader["Password"];
                    if (BCrypt.Net.BCrypt.Verify(password, passwordHash, false, HashType.SHA384) == false)
                        break;
                    isLogged = true;
                    Auth.SetLogin(id, user);

                    return true;
                }
            }
            return false;
        }

        public List<Asset> getAssets()
        {
            List<Asset> tmpAsset = new List<Asset>();

            return new List<Asset>();
        }

        public List<Portfolio> GetPortfolio()
        {
            List<Portfolio> lstPortfolio = new List<Portfolio>();
            if (!Auth.IsLogged)
                return lstPortfolio;

            SqlCommand cmdGetPortfolio = new SqlCommand("SELECT * FROM PortfolioAssets as AW WHERE UserId = '" + Auth.Id + "'", conn); //"' AND convert(varchar, AW.Date, 23) = MH.Date", conn);
            using (SqlDataReader reader = cmdGetPortfolio.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Portfolio tmpPortfolio = new Portfolio();
                        tmpPortfolio.Id = (int)reader["Id"];
                        tmpPortfolio.Symbol = (string)reader["Symbol"];
                        tmpPortfolio.NumOfShares = (int)reader["NumOfShares"];
                        tmpPortfolio.PurchasePrice = double.Parse(reader["BuyPrice"].ToString());
                        tmpPortfolio.SellPrice = double.Parse(reader["SellPrice"].ToString());
                        tmpPortfolio.ClosePrice = double.Parse(reader["ClosePrice"].ToString());
                        //tmpPortfolio.Name = (string)reader["Name"];
                        //tmpPortfolio.Date = (DateTime)reader["Date"];

                        lstPortfolio.Add(tmpPortfolio);
                    }
                    return lstPortfolio;
                }
            }

            return null;

        }
        public List<WatchList> GetWatchlist()
        {
            List<WatchList> lstWatchlist = new List<WatchList>();
            if (!Auth.IsLogged)
                return null;

            SqlCommand cmdGetWatchlist = new SqlCommand("  SELECT * FROM AssetWatchList as AW " +
                                                      // " INNER JOIN MarketHistory as MH ON AW.Symbol = MH.Symbol "+
                                                      " WHERE AW.UserId = '" + Auth.Id + "'", conn); //"' AND convert(varchar, AW.Date, 23) = MH.Date", conn);
            using (SqlDataReader reader = cmdGetWatchlist.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        WatchList tmpWatchList = new WatchList();
                        tmpWatchList.Id = (int)reader["Id"];
                        tmpWatchList.Symbol = (string)reader["Symbol"];
                        tmpWatchList.Name = (string)reader["Name"];
                        tmpWatchList.Date = (DateTime)reader["Date"];
                        //tmpWatchList.OpenPrice = double.Parse(reader["OpenPrice"].ToString());
                       // tmpWatchList.HighPrice = double.Parse(reader["HighPrice"].ToString());
                       // tmpWatchList.LowPrice = double.Parse(reader["LowPrice"].ToString());
                       // tmpWatchList.ClosePrice = double.Parse(reader["ClosePrice"].ToString());
                       // tmpWatchList.Volume = (int)reader["Volume"];

                        lstWatchlist.Add(tmpWatchList);
                    }
                    return lstWatchlist;
                }
            }

            return null;
        }

        public bool AddWatchlist(Asset asset)
        {

            List<WatchList> globalList = Globals.WatchList;

            WatchList tmpWatchlist = new WatchList();

            tmpWatchlist.Symbol = asset.Symbol;
            tmpWatchlist.Name = asset.Name;

            SqlCommand cmdGetWatchlist = new SqlCommand("SELECT * FROM AssetWatchList WHERE Symbol='" + asset.Symbol + "' AND UserId='"+Auth.Id+"'", conn);
            using (SqlDataReader reader = cmdGetWatchlist.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    //
                    reader.Close();
                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO AssetWatchList (UserId, Symbol, Name, Date) VALUES (@UserId, @Symbol, @Name, @Date)", conn);
                    cmdInsert.Parameters.AddWithValue("UserId", Auth.Id);
                    cmdInsert.Parameters.AddWithValue("Symbol", tmpWatchlist.Symbol);
                    cmdInsert.Parameters.AddWithValue("Name", tmpWatchlist.Name);
                    DateTime curDate = DateTime.Now;
                    cmdInsert.Parameters.AddWithValue("Date", curDate);

                    cmdInsert.ExecuteNonQuery();

                    globalList.Add(tmpWatchlist);

                    return true;


                    
                }

                return false;
            }
        }

        public bool AddPortfolio(Portfolio portfolioItem)
        {

            List<Portfolio> globalList = Globals.Portfolio;

            Portfolio tmpPortfolio = new Portfolio();

            tmpPortfolio.Symbol = portfolioItem.Symbol;
            tmpPortfolio.NumOfShares = portfolioItem.NumOfShares;
            tmpPortfolio.SellPrice = portfolioItem.SellPrice;
            tmpPortfolio.PurchasePrice = portfolioItem.PurchasePrice;
            tmpPortfolio.ClosePrice = portfolioItem.ClosePrice;

            SqlCommand cmdGetWatchlist = new SqlCommand("SELECT * FROM PortfolioAssets WHERE Symbol='" + portfolioItem.Symbol + "' AND UserId='" + Auth.Id + "'", conn);
            using (SqlDataReader reader = cmdGetWatchlist.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    //
                    reader.Close();
                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO PortfolioAssets (UserId, Symbol, NumOfShares, BuyPrice, SellPrice, ClosePrice) VALUES (@UserId, @Symbol, @NumOfShares, @BuyPrice, @SellPrice, @ClosePrice)", conn);
                    cmdInsert.Parameters.AddWithValue("UserId", Auth.Id);
                    cmdInsert.Parameters.AddWithValue("Symbol", tmpPortfolio.Symbol);
                    cmdInsert.Parameters.AddWithValue("NumOfShares", tmpPortfolio.NumOfShares);
                    cmdInsert.Parameters.AddWithValue("BuyPrice", tmpPortfolio.PurchasePrice);
                    cmdInsert.Parameters.AddWithValue("SellPrice", tmpPortfolio.SellPrice);
                    cmdInsert.Parameters.AddWithValue("ClosePrice", tmpPortfolio.ClosePrice);

                    cmdInsert.ExecuteNonQuery();

                    globalList.Add(tmpPortfolio);

                    return true;

                }

                return false;
            }
        }


        public bool AddMarketAsset(List<MarketSummary> Asset)
        {
            DataTable dt = new DataTable();
            //Add columns  
            dt.Columns.Add(new DataColumn("Symbol", typeof(string)));
            dt.Columns.Add(new DataColumn("ExchangeName", typeof(string)));
            dt.Columns.Add(new DataColumn("ShortName", typeof(string)));
            dt.Columns.Add(new DataColumn("MarketPrice", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ChangePercent", typeof(decimal)));
            dt.Columns.Add(new DataColumn("QuoteType", typeof(string)));
            dt.Columns.Add(new DataColumn("Market", typeof(string)));
            dt.Columns.Add(new DataColumn("MarketTime", typeof(int)));
            dt.Columns.Add(new DataColumn("MarketState", typeof(string)));
            dt.Columns.Add(new DataColumn("Language", typeof(string)));
            //Add rows  

            foreach (MarketSummary ms in Asset)
            {
                if (!decimal.TryParse(ms.MarketPrice, out decimal marketPrice)) continue;
                if (!decimal.TryParse(ms.MarketChangePercent, out decimal marketChange)) continue;
                if (!int.TryParse(ms.MarketTime, out int marketTime)) continue;

                dt.Rows.Add(ms.Symbol, ms.FullExchangeName, ms.ShortExchangeName, marketPrice, marketChange, ms.QuoteType, ms.Market, marketTime, ms.MarketState, ms.Language);
            }
            //sqlcon as SqlConnection  
            SqlCommand sqlcom = new SqlCommand("usp_InsertMarketAsset", conn);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue("@marketassets", dt);
            sqlcom.ExecuteNonQuery();
            return true;
        }

        public void AddToMarketHistory(string symbol, List<MarketHistory> marketHistory)
        {
            SqlCommand cmdGetMarketHistory = new SqlCommand("SELECT TOP 1 * FROM MarketHistory WHERE Symbol = @Symbol ORDER BY Date DESC", conn);
            cmdGetMarketHistory.Parameters.AddWithValue("Symbol", symbol);
            DateTime maxDate = new DateTime();
            using (SqlDataReader reader = cmdGetMarketHistory.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maxDate = (DateTime)reader["Date"];
                    }
                }
            }
            foreach (MarketHistory dailyData in marketHistory)
            {
                if (dailyData.Date.Date == maxDate.Date)
                {
                    SqlCommand cmdUpdateLastRecord = new SqlCommand("UPDATE MarketHistory SET OpenPrice = @Open, HighPrice= @High, LowPrice= @Low, ClosePrice = @Close, Volume =@Volume WHERE Date = @Date", conn);
                    cmdUpdateLastRecord.Parameters.AddWithValue("Open", dailyData.Open);
                    cmdUpdateLastRecord.Parameters.AddWithValue("High", dailyData.High);
                    cmdUpdateLastRecord.Parameters.AddWithValue("Low", dailyData.Low);
                    cmdUpdateLastRecord.Parameters.AddWithValue("Close", dailyData.Close);
                    cmdUpdateLastRecord.Parameters.AddWithValue("Volume", dailyData.Volume);
                    cmdUpdateLastRecord.Parameters.AddWithValue("Date", dailyData.Date);
                    cmdUpdateLastRecord.ExecuteNonQuery();
                }
                else if (dailyData.Date.Date > maxDate.Date)
                {
                    SqlCommand cmdInsertData = new SqlCommand("INSERT INTO MarketHistory (Symbol,Date,OpenPrice,HighPrice,LowPrice,ClosePrice,Volume) VALUES(@Symbol,@Date,@Open,@High,@Low,@Close,@Volume)", conn);
                    cmdInsertData.Parameters.AddWithValue("Symbol", dailyData.Symbol);
                    cmdInsertData.Parameters.AddWithValue("Date", dailyData.Date);
                    cmdInsertData.Parameters.AddWithValue("Open", dailyData.Open);
                    cmdInsertData.Parameters.AddWithValue("High", dailyData.High);
                    cmdInsertData.Parameters.AddWithValue("Low", dailyData.Low);
                    cmdInsertData.Parameters.AddWithValue("Close", dailyData.Close);
                    cmdInsertData.Parameters.AddWithValue("Volume", dailyData.Volume);
                    cmdInsertData.ExecuteNonQuery();
                }
            }
        }

        public List<MarketHistory> GetMarketHistory( string symbol)
        {
            List<MarketHistory> marketHistoryList = new List<MarketHistory>();
            SqlCommand cmdSelectAllHistory = new SqlCommand("SELECT * FROM MarketHistory WHERE Symbol = @Symbol ORDER BY Date", conn);
            cmdSelectAllHistory.Parameters.AddWithValue("Symbol", symbol);
            using (SqlDataReader reader = cmdSelectAllHistory.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MarketHistory currentHistory = new MarketHistory();
                        currentHistory.Id = (int)reader["Id"];
                        currentHistory.Symbol = (string)reader["Symbol"];
                        currentHistory.Date = (DateTime)reader["Date"];
                        currentHistory.Open = double.Parse(reader["OpenPrice"].ToString());
                        currentHistory.High = double.Parse(reader["HighPrice"].ToString());
                        currentHistory.Low = double.Parse(reader["LowPrice"].ToString());
                        currentHistory.Close = double.Parse(reader["ClosePrice"].ToString());
                        currentHistory.Volume = (int)reader["Volume"];
                        marketHistoryList.Add(currentHistory);
                    }
                    return marketHistoryList;
                }
            }
            return marketHistoryList;
        }


        public List<string> GetWatchlistDistinctSymbols()
        {
            List<string> watchlistDistinctSymbols= new List<string>();

            SqlCommand cmdGetDistinctWatchlistSymbol = new SqlCommand("SELECT DISTINCT Symbol FROM AssetWatchList", conn);
            using (SqlDataReader reader = cmdGetDistinctWatchlistSymbol.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        watchlistDistinctSymbols.Add((string)reader["Symbol"]);
                    }
                    return watchlistDistinctSymbols;
                }
            }
            return null;
        }

        public List<DateTime> GetAllDatesFromMarketHistory()
        {
            List<DateTime> distinctDates = new List<DateTime>();
            SqlCommand cmdSelectDistinctDates = new SqlCommand("SELECT DISTINCT Date FROM MarketHistory ORDER BY DATE DESC", conn);
            using (SqlDataReader reader = cmdSelectDistinctDates.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        distinctDates.Add((DateTime)reader["Date"]);
                    }
                    return distinctDates;
                }
            }
            return null;
        }

        //get last date's closing value of a symbol
        public double GetLastDateClosingValue(string symbol)
        {
            SqlCommand selectDateClosingValue = new SqlCommand("SELECT TOP 1 * FROM MarketHistory WHERE Symbol = @Symbol ORDER BY Date DESC", conn);
            selectDateClosingValue.Parameters.AddWithValue("Symbol", symbol);
            double closingValue = 0.00;
            using (SqlDataReader reader = selectDateClosingValue.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        closingValue = double.Parse(reader["ClosePrice"].ToString());
                    }
                }
            }
            return closingValue;
        }
    }


    
}
