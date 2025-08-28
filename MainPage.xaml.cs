using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Media;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_2425
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private const double ShakeThreshold = 15.0;
        private DateTime _lastShakeTime = DateTime.MinValue;
        private const int ShakeInterval = 1000;

        public MainPage()
        {
            InitializeComponent();

            CityPicker.ItemsSource = new List<string>
            {
                "Manchester",
                "Sheffield",
                "London",
                "Newcastle",
                "Birmingham",
                "Bradford"
            };

            string username = Preferences.Get("Username", string.Empty);

            if (!string.IsNullOrEmpty(username))
            {
                WelcomeLabel.Text = $"Welcome, {username}!";
            }
            else
            {
                WelcomeLabel.Text = "Welcome, Guest!";
            }

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.UI);
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var reading = e.Reading;
            double acceleration = Math.Sqrt(
                Math.Pow(reading.Acceleration.X, 2) +
                Math.Pow(reading.Acceleration.Y, 2) +
                Math.Pow(reading.Acceleration.Z, 2));

            if (acceleration > ShakeThreshold &&
                (DateTime.Now - _lastShakeTime).TotalMilliseconds > ShakeInterval)
            {
                _lastShakeTime = DateTime.Now;
                OnShakeDetected();
            }
        }

        private async void OnShakeDetected()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(RecipeStorage.Recipes.Count);
            var randomRecipe = RecipeStorage.Recipes[randomIndex];

            await DisplayAlert("Shake Detected!", $"Try this recipe: {randomRecipe.Name}", "OK");
            Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(500));
        }

        private void OnThemeSwitchClicked(object sender, EventArgs e)
        {
            // Toggle the theme when the button is clicked
            var app = (App)Application.Current;
            app.ToggleAppTheme();
        }

        private async void OnSpeakClicked(object sender, EventArgs e)
        {
            string message = "Welcome to QuickEatZ! Let's get cooking!";
            await TextToSpeech.SpeakAsync(message);
        }

        private async void OnFindRecipesClicked(object sender, EventArgs e)
        {
            string selectedCity = CityPicker.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedCity))
            {
                // Optional: assign a default city, or pass null
                selectedCity = "Any"; // or just pass null
            }

            // Navigate to the recipe page and pass the selected city
            await Navigation.PushAsync(new RecipesPage(selectedCity));
        }



        private async void OnPopularRecipesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PopularRecipesPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");

            if (confirm)
            {
                Preferences.Clear();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        private async void OnGetLocationClicked(object sender, EventArgs e)
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(10)
                    });
                }

                if (location != null)
                {
                    await DisplayAlert("Your Location", $"Latitude: {location.Latitude}\nLongitude: {location.Longitude}", "OK");
                }
                else
                {
                    await DisplayAlert("Location Error", "Unable to detect location.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Location not available: {ex.Message}", "OK");
            }
        }

        // Method supports OnFindRecipesClicked
        private async Task<string> GetCityFromLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(10)
                    });
                }

                if (location != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location);
                    var placemark = placemarks?.FirstOrDefault();
                    return placemark?.Locality;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get location: {ex.Message}");
            }

            return null;
        }

        private async void LoadUserLocation()
        {
            try
            {
                // Attempt to get user's city based on location
                string city = await GetCityFromLocation();

                if (!string.IsNullOrEmpty(city))
                {
                    Console.WriteLine($"Detected city: {city}");

                    // Make sure CityPicker.ItemsSource is not null
                    if (CityPicker.ItemsSource != null)
                    {
                        var match = CityPicker.ItemsSource
                            .Cast<string>()
                            .FirstOrDefault(item => item.Equals(city, StringComparison.OrdinalIgnoreCase));

                        if (match != null)
                        {
                            CityPicker.SelectedItem = match;
                        }
                        else
                        {
                            Console.WriteLine("City not found in CityPicker.ItemsSource.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("CityPicker.ItemsSource is null.");
                    }
                }
                else
                {
                    Console.WriteLine("Could not determine city from location.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadUserLocation: {ex.Message}");
            }
        }



        private async void OnLocationRecipesClicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    string city = placemark?.Locality;

                    if (!string.IsNullOrEmpty(city) && LocationRecipeStorage.CityRecipes.TryGetValue(city, out var recipes))
                    {
                        string recipeList = string.Join("\n", recipes);
                        await DisplayAlert($"Recipes for {city}", recipeList, "OK");
                    }
                    else
                    {
                        await DisplayAlert("No Recipes", $"Sorry, we have no recipes for {city ?? "your location"} yet.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to fetch location: {ex.Message}", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Accelerometer.Stop();
        }
    }
}
