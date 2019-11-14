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
	public partial class settings : ContentPage
	{
		public settings ()
		{
			InitializeComponent ();
            button.IsEnabled = false;
            fill();
            update(null,null);
		}

        public void fill()
        {
            nameVal.Text = Preferences.Get("name", "default_value");
            surVal.Text = Preferences.Get("surname", "default_value");
            plateVal.Text = Preferences.Get("plate", "default_value");
            cardVal.Text = Preferences.Get("cardNumber", "default_value");
        }

        public void update(object sender, EventArgs e)
        {
            string name = nameVal.Text;
            string surname = surVal.Text;
            string card = cardVal.Text;
            string plate = plateVal.Text;

            if (name.Length > 0 && surname.Length > 0 && card.Length == 16 && functions.checkPlate(plate)==null)
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;
            }
        }

        public async void saveData(object sender, EventArgs e)
        {
            string name = nameVal.Text;
            string surname = surVal.Text;
            string plate = plateVal.Text;
            string card = cardVal.Text;
            string userID = Preferences.Get("userID", "default_value");
            string login = Preferences.Get("login", "default_value");
            string pass = Preferences.Get("password", "default_value");

            Database db = new Database("d75356.mysql.zonevs.eu", "d75356sa238834", "Databaza2711", "d75356_parkent");
            db.ExecuteQuery(String.Format("UPDATE user SET name = '{0}', surname = '{1}', carNum = '{2}', cardNumber = '{3}' WHERE userID = {4};", name, surname, plate, card, userID));

            fetch(login, pass);

            await Navigation.PopModalAsync();
        }

        public static void fetch(string login, string pass)
        {
            functions func = new functions();
            func.fetch(login, pass);
        }
	}
}