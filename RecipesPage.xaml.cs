using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using System.Linq;

namespace assignment_2425
{
    public partial class RecipesPage : ContentPage
    {
        private List<Recipe> _allRecipes;
        private string _selectedCity;

        public RecipesPage(string? city = null)
        {
            InitializeComponent();

            _allRecipes = RecipeStorage.Recipes;

            // Safe fallback for city
            _selectedCity = city ?? "Any";

            // Load recipes based on provided city or all
            FilterRecipes(_selectedCity);

            // Optionally populate CityPicker
            CityPicker.ItemsSource = new List<string>
            {
                "Manchester",
                "Sheffield",
                "London",
                "Newcastle",
                "Birmingham",
                "Bradford"
            };

            // Try to automatically set city picker based on user's location
            LoadUserLocation();

            // Default category selection
            CategoryPicker.SelectedIndex = 0;

            MessagingCenter.Subscribe<AddRecipePage, Recipe>(this, "RecipeAdded", (sender, recipe) =>
            {
                _allRecipes.Add(recipe);
                FilterRecipes(_selectedCity);
            });
        }

        private async void LoadUserLocation()
        {
            var city = await GetCityFromLocation();
            if (!string.IsNullOrEmpty(city))
            {
                var match = CityPicker.ItemsSource
                    .Cast<string>()
                    .FirstOrDefault(item => item.Equals(city, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                {
                    CityPicker.SelectedItem = match;
                    _selectedCity = match;
                    FilterRecipes(_selectedCity);
                }
            }
        }

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

        private void OnSearchClicked(object sender, EventArgs e)
        {
            FilterRecipes();
        }

        private void OnCityChanged(object sender, EventArgs e)
        {
            _selectedCity = CityPicker.SelectedItem?.ToString() ?? "Any";
            FilterRecipes(_selectedCity);
        }

        private void OnCategoryChanged(object sender, EventArgs e)
        {
            FilterRecipes();
        }

        private void FilterRecipes(string selectedCityOverride = null)
        {
            string userInput = IngredientEntry.Text?.ToLower() ?? "";
            string selectedCategory = CategoryPicker.SelectedItem?.ToString() ?? "All";

            string selectedCity = selectedCityOverride ?? CityPicker.SelectedItem?.ToString();

            // Treat "Any" or null as "no filtering by city"
            var filteredRecipes = _allRecipes
                .Where(r =>
                    (selectedCategory == "All" || r.Category == selectedCategory) &&
                    (string.IsNullOrWhiteSpace(userInput) ||
                        userInput.Split(',')
                                 .Any(ingredient => r.Ingredients.Contains(ingredient.Trim(), StringComparison.OrdinalIgnoreCase))) &&
                    (string.IsNullOrWhiteSpace(selectedCity) || selectedCity == "Any" ||
                        r.City?.Equals(selectedCity, StringComparison.OrdinalIgnoreCase) == true)
                )
                .ToList();

            RecipesListView.ItemsSource = filteredRecipes;
        }

        private void OnSaveRecipeClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedRecipe = (Recipe)button.CommandParameter;

            bool wasSaved = RecipeStorage.SaveRecipe(selectedRecipe);

            if (wasSaved)
            {
                selectedRecipe.IsSaved = true;
                DisplayAlert("Saved!", $"{selectedRecipe.Name} has been saved.", "OK");
            }
            else
            {
                DisplayAlert("Already Saved", $"{selectedRecipe.Name} is already in your saved recipes.", "OK");
            }
        }

        private async void OnSavedRecipesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SavedRecipesPage));
        }

        private async void OnAddRecipeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRecipePage());
        }
    }
}
