using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parkent.pages.menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class passwordRecovery : ContentPage
	{
		public passwordRecovery ()
		{
			InitializeComponent ();
		}

        public void update(object sender, EventArgs e)
        {
            string pass = passVal.Text;
            string newPass = newVal.Text;
            string checkPass = checkVal.Text;
            string placeholder = "";

            if (newVal.Text != null)
            {
                for (int i = 0; i < newPass.Length; i++)
                {
                    placeholder = placeholder + "*";
                }
            }
            

            checkVal.Placeholder = placeholder;

            if (newPass == checkPass)
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;
            }
        }

        public async void checkPassword(object sender, EventArgs e)
        {
            functions func = new functions();
            string login = Preferences.Get("login", "default_value");

            if (func.checkAccount(login, passVal.Text))
            {
                Console.WriteLine("ПАСВАРД: "+newVal.Text); Console.WriteLine("ПАСВАРД: "+ newVal.Text); Console.WriteLine("ПАСВАРД: "+ newVal.Text);
                Console.WriteLine("ЛОгИн: "+ login); Console.WriteLine("ЛОгИн: ", login); Console.WriteLine("ЛОгИн: "+ login); Console.WriteLine("ЛОгИн: "+ login);
                func.updatePassword(login, newVal.Text);
                await DisplayAlert("Смена пароля", "Пароль успешно изменен", "Понимаю");
                await Navigation.PopToRootAsync();
            }
            else
            {
                passVal.Focus();
                await DisplayAlert("Ошибка", "Вы ввели не верный пароль", "Ой");
            }
        }
	}
}