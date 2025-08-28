namespace assignment_2425
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for pages that are not declared directly in AppShell.xaml
            Routing.RegisterRoute(nameof(SavedRecipesPage), typeof(SavedRecipesPage));
            Routing.RegisterRoute(nameof(AddRecipePage), typeof(AddRecipePage));
            Routing.RegisterRoute(nameof(RecipesPage), typeof(RecipesPage));
        }
    }
}
