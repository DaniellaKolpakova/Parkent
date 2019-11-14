using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent.pages.logReg
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
        functions db = new functions();
        public login(string login, string password)
        {
            InitializeComponent();
            loginVal.Text = login;
            passVal.Text = password;
        }

        public void update(object sender, EventArgs e)
        {
            try
            {
                if (loginVal.Text.Length > 0 && passVal.Text.Length > 0)
                {
                    goButton.IsEnabled = true;
                }
                else
                {
                    goButton.IsEnabled = false;
                }
            }
            catch (Exception)
            {

            }
        }

        public async void logIn(object sender, EventArgs e)
        {
            if (db.checkLogin(loginVal.Text))
            {
                if(db.checkAccount(loginVal.Text, passVal.Text))
                {
                    db.fetch(loginVal.Text, passVal.Text);
                    await Navigation.PushModalAsync(new NavigationPage(new checkLocation()));
                }
                else
                {
                    await DisplayAlert("Ошибка", "Пароль неверен", "Понятно");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Логин не найден", "Понятно");
            }
        }
    }
}