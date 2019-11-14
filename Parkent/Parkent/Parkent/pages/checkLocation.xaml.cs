    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Parkent.pages;
using Android;
using Android.Content.PM;
using Parkent.pages.menu;

namespace Parkent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class checkLocation : ContentPage
    {
        public checkLocation()
        {
            InitializeComponent();
            getLoc();
            yesButton.IsEnabled = false;
        }

        public async void anotherLoc_open(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new anotherLoc());
        }

        public async void numTimePage_open(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new numTimePage());
        }

        public async void getLoc()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    var lat = location.Latitude;
                    var lon = location.Longitude;

                    try
                    {
                        var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                        var placemark = placemarks?.FirstOrDefault();
                        if (placemark != null)
                        {
                            var geocodeAddress =
                                $"{placemark.CountryName}, " +
                                $"{placemark.Locality}, " +
                                $"{placemark.Thoroughfare}";
                            //await DisplayAlert("МЕСТО", String.Format("Страна {0}, АдминАрея {1}, Локалити {2}, Локейшн {3}, СубАдминАрея {4}, СубТруфар {5}, Труфар {6}", 
                            //    placemark.CountryName, placemark.AdminArea, placemark.Locality, placemark.Location, placemark.SubAdminArea,
                            //    placemark.SubThoroughfare, placemark.Thoroughfare), "МДА");
                            if (functions.isLoc(placemark.Thoroughfare))
                            {
                                Login_bar.Text = geocodeAddress.ToString();
                                string zone = functions.whichZone(placemark.Thoroughfare);
                                Preferences.Set("zone", zone);
                                yesButton.IsEnabled = true;
                            }
                            else
                            {
                                Login_bar.Text = geocodeAddress.ToString()+"\n(Мы не знаем эту улицу)";
                                Preferences.Clear("zone");
                                yesButton.IsEnabled = false;
                            }                          
                        }
                    }
                    catch (FeatureNotSupportedException)
                    {
                        yesButton.IsEnabled = false;
                        await DisplayAlert("Ограничение", "Ваше устройство не поддерживает GPS", "Понятно");
                    }
                    catch (Exception)
                    {
                        yesButton.IsEnabled = false;
                        await DisplayAlert("Ограничение", "У приложения нет доступа к вашей геолокации, дайте доступ к ней в настройках вашего телефона для автоматического определения.", "Понятно");
                        await Navigation.PushAsync(new anotherLoc());
                    }
                }
            }
            catch (FeatureNotSupportedException)
            {
                yesButton.IsEnabled = false;
                await DisplayAlert("Ограничение", "Ваше устройство не поддерживает GPS", "Понятно");
            }
            catch (Exception)
            {
                yesButton.IsEnabled = false;
                await DisplayAlert("Ограничение", "У приложения нет доступа к вашей геолокации, дайте доступ к ней в настройках вашего телефона для автоматического определения.", "Понятно");
                await Navigation.PushAsync(new anotherLoc());
            }
        }

        public async void menu(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menu());
        }
    }
}