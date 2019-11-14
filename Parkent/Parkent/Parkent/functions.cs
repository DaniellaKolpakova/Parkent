using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using XamarinMySql;

namespace Parkent
{
    class functions
    {
        public bool addUser(string login, string name, string surname, string password, string plate, string card)
        {
            plate = plate.ToUpper();
            password = hashPassword(password);
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "userID","login","name","surname","password","carNum","balance","cardNumber"
            };
            db.AddTable("user", fields);
            Row[] rows = db.Tables["user"].SelectQuery(String.Format("login='{0}'", login));

            if (rows.Length == 0)
            {
                rows = db.Tables["user"].Insert(new object[] { "NULL", login, name, surname, password, plate, 0, card });

                return true;
            }
            else
            {
                return false;
            }
        }


        public static void occupySlot(string plate, int hours)
        {
            int userID = Convert.ToInt32(Preferences.Get("userID", "default_value"));
            string zone = Convert.ToString(Preferences.Get("zone", "default_value"));
            DateTime s = DateTime.Now;
            string start_date = s.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime e = s.AddHours(Convert.ToDouble(hours));
            string end_date = e.ToString("yyyy-MM-dd HH:mm:ss");

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");
            db.ExecuteQuery(String.Format("INSERT INTO zone_user VALUES(null, {0}, '{1}', '{2}', '{3}', '{4}');", userID, plate, zone, start_date, end_date));
        }

        public static int getZoneID()
        {
            string zone = Preferences.Get("zone", "default_value");

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "zoneID","zoneLoc","zonePrice"
            };
            db.AddTable("zone", fields);
            Row[] rows = db.Tables["zone"].SelectQuery(String.Format("zoneLoc='{0}'", zone));

            foreach (Row row in rows)
            {
                return Convert.ToInt32(Convert.ToString(row["zoneID"]));               
            }

            return 0;
        }

        public static double payBalance(int userID, double price)
        {
            double balance = getBalance(userID);
            Console.WriteLine("БАЛАНС"); Console.WriteLine(balance); Console.WriteLine(balance); Console.WriteLine(balance);
            if (balance < price)
            {
                balance = balance - price;
                double remained = balance * -1;
                Console.WriteLine("РЕМАЙНЕД"); Console.WriteLine(remained); Console.WriteLine(remained); Console.WriteLine(remained);
                return remained;
            }
            else
            {
                balance = balance - price;
                Console.WriteLine("БАЛАНСЕ"); Console.WriteLine(balance); Console.WriteLine(balance); Console.WriteLine(balance); Console.WriteLine(balance);
                return balance;
            }
        }

        public static bool newBank(string cardNum, double value)
        {
            string v = value.ToString();
            try
            {
                v = v.Replace(",", ".");
            }
            catch (Exception) { }
            if (isBank(cardNum))
            {
                if (value <= getBank(cardNum))
                {
                    Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");
                    db.ExecuteQuery(String.Format("UPDATE bankAccount SET balance = balance - {0} WHERE cardNum = {1};", v, cardNum));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static double getBank(string cardNum)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
            "bankID","cardNum","balance"
            };
            db.AddTable("bankAccount", fields);
            Row[] rows = db.Tables["bankAccount"].SelectQuery(String.Format("cardNum='{0}'", cardNum));

            foreach (Row row in rows)
            {
                string v = Convert.ToString(row["balance"]);
                v = v.Replace(".", ",");
                return Convert.ToDouble(v);
            }

            return 0;
        }

        public static bool isBank(string cardNum)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
            "bankID","cardNum","balance"
            };
            db.AddTable("bankAccount", fields);
            Row[] rows = db.Tables["bankAccount"].SelectQuery(String.Format("cardNum='{0}'", cardNum));

            if (rows.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void newBalance(int userID, double value)
        {
            string v = value.ToString();
            v = v.Replace(",", ".");

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "userID","login","name","surname","password","carNum","balance","cardNumber"
            };
            db.AddTable("user", fields);
            db.ExecuteQuery(String.Format("UPDATE user SET balance = {0} WHERE userID = {1};", v, userID));
        }

        public static double getBalance(int userID)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "userID","login","name","surname","password","carNum","balance","cardNumber"
            };
            db.AddTable("user", fields);
            Row[] rows = db.Tables["user"].SelectQuery(String.Format("userID='{0}'", userID));

            foreach (Row row in rows)
            {
                string v = Convert.ToString(row["balance"]);
                //v = v.Replace(".", ",");
                return Convert.ToDouble(v);
            }

            return 0;
        }

        public static string checkPlate(string plate)
        {
            if(Regex.IsMatch(plate, "\\d"))
            {
                if(Regex.IsMatch(plate, "[a-zA-Z]"))
                {
                    return null;
                }
                return "В номере должна быть хотя бы одна буква";
            }
            else
            {
                return "В номере должна быть хотя бы одна цифра";
            }
            
        }

        public bool checkLogin(string login)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "login"
            };
            db.AddTable("user", fields);
            Row[] rows = db.Tables["user"].SelectQuery(String.Format("login='{0}'", login));

            if (rows.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string hashPassword(string password)
        {
            SHA512 shaM = new SHA512Managed();
            byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            password = sBuilder.ToString();

            return (password);

        }

        public static void newPlate(string plate, int userID)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");
            db.ExecuteQuery(String.Format("INSERT INTO plates VALUES(null, {0}, '{1}');", userID, plate));
        }

        public static List<string> getPlates(int userID)
        {
            List<string> plates = new List<string>();

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "plateID", "userID", "plate"
            };
            db.AddTable("plates", fields);
            Row[] rows = db.Tables["plates"].SelectQuery(String.Format("userID='{0}' ORDER BY plateID DESC", userID));

            int max = 2;

            foreach (Row row in rows)
            {
                plates.Add(Convert.ToString(row["plate"]));
                Console.WriteLine(Convert.ToString(row["plate"])); Console.WriteLine(Convert.ToString(row["plate"])); Console.WriteLine(Convert.ToString(row["plate"]));
                max--;
                if (max == 0) return plates;
            }

            return plates;
        }

        public bool checkAccount(string login, string password)
        {
            password = hashPassword(password);
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "userID","login","name","surname","password","carNum","balance","cardNumber"
            };
            db.AddTable("user", fields);
            Row[] rows = db.Tables["user"].SelectQuery(String.Format("login='{0}'", login));

            foreach (Row row in rows)
            {
                if (Convert.ToString(row["password"]) == password)
                {
                    return true;
                }  
            } 

            return false;
        }

        public void updatePassword(string login, string password)
        {
            string newPassword = hashPassword(password);
            Console.WriteLine("НЕВ ПАСВАРД: "+newPassword); Console.WriteLine("НЕВ ПАСВАРД: ", newPassword); Console.WriteLine("НЕВ ПАСВАРД: ", newPassword);
            Console.WriteLine("ПАСВАРД: "+password); Console.WriteLine("ПАСВАРД: ", password); Console.WriteLine("ПАСВАРД: ", password);
            Console.WriteLine("ЛОгИн: "+login); Console.WriteLine("ЛОгИн: ", login); Console.WriteLine("ЛОгИн: ", login); Console.WriteLine("ЛОгИн: ", login);

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");
            db.ExecuteQuery(String.Format("UPDATE user SET password = '{0}' WHERE login = '{1}';", newPassword, login));
        }

        public void fetch(string login, string password)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "userID","login","name","surname","password","carNum","balance","cardNumber"
            };
            db.AddTable("user", fields);
            Row[] rows = db.Tables["user"].SelectQuery(String.Format("login='{0}'", login));

            foreach (Row row in rows)
            {
                Preferences.Set("userID", Convert.ToString(row["userID"]));
                Preferences.Set("login", login);
                Preferences.Set("name", Convert.ToString(row["name"]));
                Preferences.Set("surname", Convert.ToString(row["surname"]));
                Preferences.Set("password", password);
                Preferences.Set("plate", Convert.ToString(row["carNum"]));
                string v = Convert.ToString(row["balance"]).Replace(".",",");
                Preferences.Set("balance", Convert.ToDouble(v));
                Preferences.Set("cardNumber", Convert.ToString(row["cardNumber"]));
            }
        }

        public static void clearPreferences()
        {
            Preferences.Remove("userID");
            Preferences.Remove("login");
            Preferences.Remove("name");
            Preferences.Remove("surname");
            Preferences.Remove("password");
            Preferences.Remove("plate");
            Preferences.Remove("balance");
            Preferences.Remove("cardNumber");
        }

        public double getTarif()
        {
            string zone = Preferences.Get("zone", "default_value");
            double value = 1;
            try {
                zone = zone.Replace("ä", "a");
            }
            catch (Exception)
            {

            }
            try
            {
                zone = zone.Replace("õ", "o");
            }
            catch (Exception)
            {

            }

            Console.WriteLine("ЗОНА"+zone); Console.WriteLine("ЗОНА" + zone); Console.WriteLine("ЗОНА" + zone); Console.WriteLine("ЗОНА" + zone);

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "zoneID","zoneLoc","zonePrice"
            };
            db.AddTable("zone", fields);
            Row[] rows = db.Tables["zone"].SelectQuery(String.Format("zoneLoc='{0}'", zone));

            foreach (Row row in rows)
            {
                string v = Convert.ToString(row["zonePrice"]);
                Console.WriteLine("В:" + v); Console.WriteLine("В:" + v); Console.WriteLine("В:" + v); Console.WriteLine("В:" + v);
                //value = Convert.ToDouble(v.Replace(".", ","));
                value = Convert.ToDouble(v);
                Console.WriteLine("Валуе:" + value); Console.WriteLine("Валуе:" + value); Console.WriteLine("Валуе:" + value); Console.WriteLine("Валуе:" + value);
                return value;
            }

            return value;
        }

        public static bool isLoc(string place)
        {
            try
            {
                place = place.Replace("ä", "a");
            }
            catch (Exception)
            {

            }
            try
            {
                place = place.Replace("õ", "o");
            }
            catch (Exception)
            {

            }

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "districtID","districtZone","districtName"
            };
            db.AddTable("district", fields);
            Row[] rows = db.Tables["district"].SelectQuery(String.Format("districtName='{0}'", place));

            if (rows.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string whichZone(string place)
        {
            try
            {
                place = place.Replace("ä", "a");
            }
            catch (Exception)
            {

            }
            try
            {
                place = place.Replace("õ", "o");
            }
            catch (Exception)
            {

            }

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "districtID","districtZone","districtName"
            };
            db.AddTable("district", fields);
            Row[] rows = db.Tables["district"].SelectQuery(String.Format("districtName='{0}'", place));

            foreach (Row row in rows)
            {
                string v = Convert.ToString(row["districtZone"]);
                return v;
            }

            return null;
        }

        public bool getID(string login)
        {
            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");

            string[] fields = new string[]
            {
                "login"
            };
            db.AddTable("user", fields);
            Row[] rows = db.Tables["user"].SelectQuery(String.Format("login='{0}'", login));

            if (rows.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
