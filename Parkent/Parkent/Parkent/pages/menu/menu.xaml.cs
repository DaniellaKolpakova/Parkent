using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent.pages.menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class menu : ContentPage
	{
		public menu ()
		{
			InitializeComponent ();
		}

        public async void parkingPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new parkingPage());
        }

        public async void addBalance(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new addBalace());
        }

        public async void passwordRecovery(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new passwordRecovery());
        }


        public async void settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new settings());
        }

        public async void leave(object sender, EventArgs e)
        {
            if(await DisplayAlert("Уточнение", "Вы уверены, что хотите выйти?", "Да", "Нет"))
            {
                functions.clearPreferences();
                try
                {
                    await Navigation.PopModalAsync();
                }
                catch (Exception)
                {
                    await Navigation.PushModalAsync(new MainPage());
                }
            }          
        }
	}
}