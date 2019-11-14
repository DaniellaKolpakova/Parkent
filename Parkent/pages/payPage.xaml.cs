using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class payPage : ContentPage
	{
        int hours=1;
        int balance = 228;
        string cardNumber = "5165 3710 2585 3981";
        float tarif = 2;
        string hLab = "час";
        string plate;
        Boolean isBank = false;
        Boolean isBalance = false;

        public payPage (int h, string p)
		{
			InitializeComponent ();

            hours = h;

            float price = tarif * hours;
            priceLabel.Text = "Стоимость: " + price + "€";
            balanceBtn.Text = "Баланс| " + balance.ToString()+"€";
            bankBtn.Text = "Банковская карта| **** " + cardNumber.Substring(cardNumber.Length - 4);
            if (hours == 1)
            {
                hLab = "час";
            }
            else if(hours <= 2 && hours >= 4){
                hLab = "часа";
            }
            else
            {
                hLab = "часов";
            }

            tarifLabel.Text = String.Format("Парковка в вашей зоне стоит {0}€/час. Вы оплачиваете парковку на {1} {2} для номера {3}", tarif, hours.ToString(), hLab, plate);

            if (balance >= price)
            {
                isBalance = true;
                updatePay();
            }
            else if (balance > 0)
            {
                balanceBtn.BorderColor = Color.White;
                bankBtn.BorderColor = Color.White;
            }
            else
            {
                bankBtn.BorderColor = Color.White;
                balanceBtn.IsEnabled = false;
                isBalance = false;
            }
		}

        public void updatePay()
        {
            balanceBtn.BorderColor = Color.Transparent;
            bankBtn.BorderColor = Color.Transparent;

            if (isBalance)
            {
                balanceBtn.BorderColor = Color.White;
            }
            if (isBank)
            {
                bankBtn.BorderColor = Color.White;
            }

            if(isBank || isBalance)
            {
                pay.IsEnabled = true;
            }
            else
            {
                pay.IsEnabled = false;
            }
        }

        public void checkPay(object sender, EventArgs e)
        {
            switch (((Button)sender).CommandParameter)
            {
                case "bank":
                    if (isBank)
                    {
                        isBank = false;
                        updatePay();
                    }
                    else
                    {
                        isBank = true;
                        updatePay();
                    }
                    break;
                case "balance":
                    if (isBalance)
                    {
                        isBalance = false;
                        updatePay();
                    }
                    else
                    {
                        isBalance = true;
                        updatePay();
                    }
                    break;
                default:
                    break;
            }
        }
	}
}