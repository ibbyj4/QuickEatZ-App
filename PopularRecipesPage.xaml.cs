namespace assignment_2425;

public partial class PopularRecipesPage : ContentPage
{
    public PopularRecipesPage()
    {
        InitializeComponent();

        // For now, we just display 2 recipes from the full list as "popular"
        var popular = RecipeStorage.Recipes
            .Where(r => r.Name.Contains("Pasta") || r.Name.Contains("Omelette"))
            .ToList();

        PopularRecipesListView.ItemsSource = popular;
    }
}
