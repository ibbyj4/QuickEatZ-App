using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace assignment_2425
{
    public partial class SavedRecipesPage : ContentPage
    {
        public SavedRecipesPage()
        {
            InitializeComponent();
            LoadSavedRecipes();
        }

        private void LoadSavedRecipes()
        {
            // Load the saved recipes from RecipeStorage
            SavedRecipesListView.ItemsSource = RecipeStorage.SavedRecipes;
        }

        private void OnRemoveRecipeClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedRecipe = (Recipe)button.CommandParameter;

            if (RecipeStorage.SavedRecipes.Contains(selectedRecipe))
            {
                RecipeStorage.SavedRecipes.Remove(selectedRecipe);
                DisplayAlert("Removed", $"{selectedRecipe.Name} was removed from your saved recipes.", "OK");

                // Refresh the list
                SavedRecipesListView.ItemsSource = null;
                SavedRecipesListView.ItemsSource = RecipeStorage.SavedRecipes;
            }
        }

    }
}
