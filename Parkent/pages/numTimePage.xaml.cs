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
	public partial class numTimePage : ContentPage
	{
        public int hours = 1;

		public numTimePage ()
		{
			InitializeComponent ();
            defaultHour();
            globalCheck(null, null);
        }

        public void defaultHour()
        {
            clearBorder();
            hour1.BorderColor = Color.White;
            hours = 1;
        }

        public async void entryCheck(object sender, EventArgs args)
        {
            int value = Convert.ToInt32(Entry.Text);
            if(value > 168)
            {
                await DisplayAlert("Ограничение", "Нельзя оплатить парковку более чем на одну неделю (168 часов)", "Понятно");
                hours = 168;
                clearBorder();
                Entry.Text = "168";
            }
            else
            {
                hours = Convert.ToInt32(Entry.Text);
                clearBorder();
            }

            globalCheck(null, null);

        }

        public void radioButton(object sender, EventArgs args)
        {
            switch (((Button)sender).Text)
            {
                case "1 ч.":
                    defaultHour();
                    break;
                case "2 ч.":
                    hours = 2;
                    clearBorder();
                    hour2.BorderColor = Color.White;
                    break;
                case "3 ч.":
                    hours = 3;
                    clearBorder();
                    hour3.BorderColor = Color.White;
                    break;
                case "8 ч.":
                    hours = 8;
                    clearBorder();
                    hour8.BorderColor = Color.White;
                    break;
                case "12 ч.":
                    hours = 12;
                    clearBorder();
                    hour12.BorderColor = Color.White;
                    break;
                default:
                    hours = 1;
                    clearBorder();
                    hour1.BorderColor = Color.White;
                    break;
            }

            globalCheck(null, null);
        }

        public void globalCheck(object sender, EventArgs e)
        {
            if(hours>0 && hours <= 168 && plateEntry.Text.Length == 7)
            {
                if(plateEntry.Text.Length != 7)
                {
                    plateEntry.Text = null;
                }
                goButton.IsEnabled = true;
            }
            else
            {
                goButton.IsEnabled = false;
            }
        }

        public void clearBorder()
        {
            Entry.Text = null;
            hour1.BorderColor = Color.Transparent;
            hour2.BorderColor = Color.Transparent;
            hour3.BorderColor = Color.Transparent;
            hour8.BorderColor = Color.Transparent;
            hour12.BorderColor = Color.Transparent;
        }

        public async void goPay(object sender, EventArgs e)
        {
            Console.WriteLine("SEND-",hours);
            Console.WriteLine("SEND-", hours);
            Console.WriteLine("SEND-", hours);
            Console.WriteLine("SEND-", hours);
            Console.WriteLine("SEND-", hours);
            await Navigation.PushAsync(new payPage(hours, plateEntry.Text));
        }

        
	}
}