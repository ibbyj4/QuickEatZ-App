using System.Collections.Generic;
using System.Linq;

namespace assignment_2425
{
    public static class RecipeStorage
    {
        // This holds the list of all available recipes (e.g., used for searching)
        public static List<Recipe> Recipes { get; } = new List<Recipe>
        {
            new Recipe { Name = "Pasta with Tomato Sauce", Ingredients = "pasta, tomato, garlic, basil", Category = "Lunch", City = "Manchester" },
            new Recipe { Name = "Omelette", Ingredients = "egg, cheese, milk", Category = "Breakfast", City = "London" },
            new Recipe { Name = "Fruit Salad", Ingredients = "apple, banana, orange, grapes", Category = "Dessert", City = "Birmingham" }
        };

        // This holds recipes saved by the user
        public static List<Recipe> SavedRecipes { get; } = new List<Recipe>();

        // This method handles adding a recipe to SavedRecipes, checking for duplicates
        public static bool SaveRecipe(Recipe recipe)
        {
            if (!SavedRecipes.Any(r => r.Name == recipe.Name))
            {
                SavedRecipes.Add(recipe);
                return true; // recipe was newly added
            }
            return false; // recipe was already saved
        }
    }
}

