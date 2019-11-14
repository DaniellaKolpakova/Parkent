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


namespace Parkent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class checkLocation : ContentPage
    {
        public checkLocation()
        {
            InitializeComponent();
            getLoc();
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
                                $"{placemark.AdminArea}, " +
                                $"{placemark.Thoroughfare}";

                            Login_bar.Text = geocodeAddress.ToString();
                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        await DisplayAlert("Ограничение", "У приложения нет доступа к вашей геолокации, дайте доступ к ней в настройках вашего телефона для автоматического определения.", "Понятно");
                        await Navigation.PushAsync(new anotherLoc());
                    }
                    catch (Exception ex)
                    {
                        // Handle exception that may have occurred in geocoding
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Ограничение", "У приложения нет доступа к вашей геолокации, дайте доступ к ней в настройках вашего телефона для автоматического определения.", "Понятно");
                await Navigation.PushAsync(new anotherLoc());
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        }

        private void anotherLoc_Clicked(object sender, EventArgs e)
        {

        }
    }
}