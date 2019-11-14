using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            plateEntry.Text = Preferences.Get("plate", "default_value");
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
            if(Entry.Text!=null && Entry.Text != "")
            {
                int value = Convert.ToInt32(Entry.Text);
                if (value > 168)
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
            if(hours>0 && hours <= 168)
            {
                if (functions.checkPlate(plateEntry.Text) == null)
                {
                    goButton.IsEnabled = true;
                }
                else
                {
                    DisplayAlert("Ошибка", functions.checkPlate(plateEntry.Text), "Понятно");
                }
            }
            else
            {
                goButton.IsEnabled = false;
            }           
        }

        public void clearBorder()
        {
            hour1.BorderColor = Color.Transparent;
            hour2.BorderColor = Color.Transparent;
            hour3.BorderColor = Color.Transparent;
            hour8.BorderColor = Color.Transparent;
            hour12.BorderColor = Color.Transparent;
        }

        public async void numberClick(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Preferences.Get("userID", "default_value"));
            List<string> plates = functions.getPlates(userID);
            Console.WriteLine(plates); Console.WriteLine(plates); Console.WriteLine(plates);
            string plate2 = "";
            string plate3 = "";
            string answer;

            string plate = Preferences.Get("plate", "default_value");
            if (plates.Count == 1)
            {
                plate2 = plates[0];
                answer = await DisplayActionSheet("Что вы хотите сделать?", "Ничего", "Изменить", plate, plate2);
            }
            else if (plates.Count == 2)
            {
                plate2 = plates[0];
                plate3 = plates[1];
                answer = await DisplayActionSheet("Что вы хотите сделать?", "Ничего", "Изменить", plate, plate2, plate3);
            }
            else
            {
                return;
            }

            Debug.WriteLine("Answer: " + answer);
            if (answer == "Изменить")
            {
                plateEntry.Focus();
            }
            else if (answer==plate)
            {
                plateEntry.Text = plate;
                plateEntry.Unfocus();
            }
            else if (answer == plate2)
            {
                plateEntry.Text = plate2;
                plateEntry.Unfocus();
            }
            else if (answer == plate3)
            {
                plateEntry.Text = plate3;
                plateEntry.Unfocus();
            }
            else
            {
                plateEntry.Unfocus();
            }
        }

        public async void goPay(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new payPage(hours, plateEntry.Text));
        }

        
	}
}