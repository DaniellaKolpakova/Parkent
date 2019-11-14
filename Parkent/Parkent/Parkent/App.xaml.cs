using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent
{
    public partial class App : Application
    {
        functions db = new functions();

        public App()
        {
            InitializeComponent();       
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            //string login = Preferences.Get("login", "default_value");
            //string password = Preferences.Get("password", "default_value");
            //if (db.checkAccount(login, password))
            //{
            //    MainPage = new NavigationPage(new checkLocation());
            //}
            //else
            //{
                MainPage = new NavigationPage(new MainPage());
            //}
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            string login = Preferences.Get("login", "default_value");
            string password = Preferences.Get("password", "default_value");
            if (db.checkAccount(login, password))
            {
                MainPage = new NavigationPage(new checkLocation());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }
    }
}
