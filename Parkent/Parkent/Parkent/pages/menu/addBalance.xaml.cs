using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent.pages.menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class addBalace : ContentPage
	{
        int userID = Convert.ToInt32(Preferences.Get("userID", "default_value"));

        public addBalace ()
		{
			InitializeComponent ();
            cardLab.Text = Preferences.Get("cardNumber", "default_value");
            balanceLab.Text = "Баланс: "+Convert.ToString(functions.getBalance(userID));
		}

        public void update(object sender, EventArgs e)
        {
            if ((cardLab.Text).Length == 16 && Convert.ToInt32(sumVal.Text)>0)
            {
                addBtn.IsEnabled = true;
            }
            else
            {
                addBtn.IsEnabled = false;
            }
        }

        public async void AddBalance(object sender, EventArgs e)
        {
            string v = sumVal.Text;

            if (functions.newBank(cardLab.Text, Convert.ToDouble(v))){
                double balance = functions.getBalance(userID);
                balance = balance + Convert.ToDouble(sumVal.Text);
                functions.newBalance(userID, balance);
                await Navigation.PushAsync(new menu());
                //await Navigation.PushModalAsync(new celebratePage());
                await DisplayAlert("Спасибо", "Платеж успешно прошел!", "Понимаю");
            }
            else
            {
                await DisplayAlert("Упс", "Либо вы указали неверный номер карты, либо у вас не хватает средств на счету", "Ох...");
            }
        }
	}
}