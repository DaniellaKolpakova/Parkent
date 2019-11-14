using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class payPage : ContentPage
	{
        functions db = new functions();
        int userID = Convert.ToInt32(Preferences.Get("userID", "default_value"));
        int hours=1;
        string cardNumber = Preferences.Get("cardNumber", "default_value");
        string hLab = "час";
        Boolean isBank = false;
        Boolean isBalance = false;
        string plate;

        public payPage (int h, string p)
		{
			InitializeComponent ();
            double balance = functions.getBalance(userID);
            hours = h;
            plate = p;
            double tarif = 1;
            tarif = Convert.ToDouble(db.getTarif());
            double price = tarif * hours;
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
            if (balance < price)
            {
                bankBtn.IsEnabled = false;
                isBank = true;
                updatePay();
            }
            else if (balance >= price)
            {
                isBalance = true;
                updatePay();
            }
            else if (balance > 0)
            {
                isBalance = true;
                isBank = true;
                updatePay();
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
            double balance = functions.getBalance(userID);

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
                        if (isBalance==false){
                            isBank = true;
                            updatePay();
                        }
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
                        if (isBank==false && balance!=0)
                        {
                            isBalance = true;
                            updatePay();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public async void goCelebrate(object sender, EventArgs e)
        {
            double tarif = Convert.ToDouble(db.getTarif());
            double price = tarif * hours;
            double balance = functions.getBalance(userID);
            string cardNum = Preferences.Get("cardNumber", "default_value");
            double value = balance - price;


            if (isBalance)
            {
                double remaining = Convert.ToDouble(functions.payBalance(userID, price));
                if (remaining >= 0)
                {
                    functions.newBalance(userID, value);
                    functions.occupySlot(plate, hours);
                    functions.newPlate(plate, userID);
                    await Navigation.PushAsync(new celebratePage());
                }
                else
                {
                    if (functions.newBank(cardNum, remaining))
                    {
                        functions.newBalance(userID, 0);
                        functions.occupySlot(plate, hours);
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Упс, банк отклонил наш запрос, видимо у вас недостаточно средств на счету.", "Ой");
                    }
                }
            }
            else if (isBank)
            {
                if (functions.newBank(cardNum, price))
                {
                    functions.occupySlot(plate, hours);
                    functions.newPlate(plate, userID);
                    await Navigation.PushAsync(new celebratePage());
                }
                else
                {
                    await DisplayAlert("Ошибка", "Упс, банк отклонил наш запрос, видимо у вас недостаточно средств на счету.", "Ой");
                }
            }
        }
	}
}