using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMySql;

namespace Parkent.pages.logReg
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registration : ContentPage
    {
        Boolean focus = true;
        Boolean loginFocus = true;

        public registration()
        {
            InitializeComponent();
        }

        public void update(object sender, EventArgs e)
        {
            try
            {
                string login = loginVal.Text;
                string password = passVal.Text;
                string passTwo = pass2Val.Text;
                string name = nameVal.Text;
                string surname = surVal.Text;
                string plate = plateVal.Text;
                string card = cardVal.Text;

                string placeholder = "";
                for (int i = 0; i < password.Length; i++)
                {
                    placeholder = placeholder + "*";
                }

                pass2Val.Placeholder = placeholder;

                if (password == passTwo && card.Length == 16 && login.Length > 0 && name.Length > 0 && surname.Length > 0)
                {
                    if (functions.checkPlate(plate)==null)
                    {
                        goButton.IsEnabled = true;
                        plateVal.BackgroundColor = Color.Default;
                    }
                    else
                    {
                        plateVal.BackgroundColor = Color.FromHex("#d94141");
                    }
                }
                else
                {
                    goButton.IsEnabled = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex);
                Console.WriteLine(ex);
            }
        }

        public async void addUser(object sender, EventArgs e)
        {
            string login = loginVal.Text;
            string password = passVal.Text;
            string passTwo = pass2Val.Text;
            string name = nameVal.Text;
            string surname = surVal.Text;
            string plate = plateVal.Text;
            string card = cardVal.Text;
            functions db = new functions();

            if(db.addUser(login, name, surname, password, plate, card))
            {
                await Navigation.PushAsync(new login(login, password));
            }
            else
            {
                await DisplayAlert("Ошибка", "Логин уже занят", "Понятно");
                if (loginFocus)
                {
                    loginVal.Focus();
                }
            }
        }

        public void passCheck(object sender, EventArgs e)
        {
            if(passVal.Text != pass2Val.Text)
            {
                DisplayAlert("Предупреждение", "Пароли не совпадают", "Понятно");
                if (focus)
                {
                    pass2Val.Focus();
                    focus = false;
                }
            }
        }
	}
}