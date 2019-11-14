using Android.Views;
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
    public partial class celebratePage : ContentPage
    {
        public celebratePage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        public async void finish(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopToRootAsync();
            }
            catch (Exception)
            {
                await Navigation.PopModalAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}