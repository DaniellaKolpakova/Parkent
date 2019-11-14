using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMySql;

namespace Parkent.pages.menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class parkingPage : ContentPage
	{
		public parkingPage ()
		{
			InitializeComponent ();
            readParking();
		}

        public void readParking()
        {
            DateTime now = DateTime.Now;
            string nowCompare = now.ToString("yyyy-MM-dd HH:mm:ss");

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");
            string[] fields = new string[]
            {
                "zone_userID","userID","plate","zone","start_date","end_date"
            };
            db.AddTable("zone_user", fields);
            Row[] rows = db.Tables["zone_user"].SelectQuery(String.Format("end_date>'{0}' AND userID={1}", nowCompare, Preferences.Get("userID","default_value")));

            if (rows.Length != 0)
            {
                foreach (Row row in rows)
                {
                    DateTime end = Convert.ToDateTime(row["end_date"]);
                    TimeSpan left = now - end;
                    string d = Convert.ToString(left.Days).Replace("-", "") + "д, ";
                    string h = Convert.ToString(left.Hours).Replace("-", "") + "ч, ";
                    string m = Convert.ToString(left.Minutes).Replace("-", "") + "м";
                    if (d == "0д, ") d = ""; if (h == "0ч, ") h = "";

                    string timeLeft = String.Format("{0}{1}{2}", d, h, m);
                    addParking(Convert.ToString(row["plate"]), Convert.ToString(row["zone"]), timeLeft);
                }
            }
            else
            {
                noParking();
            }
        }

        public void addParking(string plate, string zone, string time)
        {
            Frame frame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#145470")
            };
            StackLayout stack = new StackLayout {
                Margin = 0
            };
            Frame plateFrame = new Frame {
                CornerRadius = 10
            };
            Label plateLab = new Label {
                Text = plate,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                FontSize = 28,
                FontFamily = Device.RuntimePlatform == Device.Android ? "Comfortaa-Bold.ttf#Comfortaa" : null
            };
            Label zoneLab = new Label {
                Text = String.Format("Зона: {0}", zone),
                TextColor = Color.White,
                FontSize = 22,
                FontFamily = Device.RuntimePlatform == Device.Android ? "Comfortaa-Regular.ttf#Comfortaa" : null
            };
            Label timeLab = new Label {
                Text = String.Format("Осталось: {0}", time),
                TextColor = Color.White,
                FontSize = 22,
                FontFamily = Device.RuntimePlatform == Device.Android ? "Comfortaa-Regular.ttf#Comfortaa" : null
            };

            plateFrame.Content = plateLab;
            stack.Children.Add(plateFrame);
            stack.Children.Add(zoneLab);
            stack.Children.Add(timeLab);
            frame.Content = stack;
            body.Children.Add(frame);

        }

        public void noParking()
        {
            Label error = new Label
            {
                Text = "У вас нет оплаченых парковок на данный момент.",
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                FontSize = 28,
                FontFamily = Device.RuntimePlatform == Device.Android ? "Comfortaa-Regular.ttf#Comfortaa" : null
            };

            body.Children.Add(error);

        }

    }
}