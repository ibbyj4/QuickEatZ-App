using System;
using Microsoft.Maui.Controls;
using System.Linq; // for the ToList() extension method

namespace assignment_2425
{
    public partial class AddRecipePage : ContentPage
    {
        public AddRecipePage()
        {
            InitializeComponent();
        }

        private async void OnAddRecipeClicked(object sender, EventArgs e)
        {
            string name = nameEntry.Text;
            string ingredients = ingredientsEditor.Text;
            string category = categoryEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ingredients) || string.IsNullOrWhiteSpace(category))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Split the ingredients string into a list and trim extra spaces
            var ingredientsList = ingredients.Split(',')
                                             .Select(ingredient => ingredient.Trim())
                                             .ToList();

            var newRecipe = new Recipe
            {
                Name = name,
                Ingredients = string.Join(", ", ingredientsList), // ✅ Fix: convert List<string> to a comma-separated string
                Category = category
            };

            // Send the new recipe back to the previous page
            MessagingCenter.Send(this, "RecipeAdded", newRecipe);

            await Navigation.PopAsync();
        }
    }
}
