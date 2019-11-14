using Parkent.pages;
using Parkent.pages.logReg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parkent
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        public async void login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new login(null, null));
        }

        public async void register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new registration());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
