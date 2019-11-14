using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Parkent.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class anotherLoc : ContentPage
	{
		public anotherLoc ()
		{
			InitializeComponent ();
        }
        public async void numTimePage_open()
        {
            await Navigation.PushAsync(new numTimePage());
        }
        public void CityArea(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                //Debug.Print(((Button)sender).Text);
                case "Lasnamae":
                    Preferences.Set("zone", "Lasnamae");                   
                    break;

                case "Mustamae":
                    Preferences.Set("zone", "Mustamae");
                    break;

                case "Pirita":
                    Preferences.Set("zone", "Pirita");
                    break;

                case "Kesklinn":
                    Preferences.Set("zone", "Kesklinn");
                    break;

                case "Kristiine":
                    Preferences.Set("zone", "Kristiine");
                    break;

                case "Nomme":
                    Preferences.Set("zone", "Nomme");
                    break;

                case "Haabersti":
                    Preferences.Set("zone", "Haabersti");
                    break;

                case "Pohja-Tallinna":
                    Preferences.Set("zone", "Pohja-Tallinna");
                    break;
            }
            numTimePage_open();
        }
    }
}