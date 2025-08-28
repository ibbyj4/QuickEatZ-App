using Microsoft.Maui.Storage;

namespace assignment_2425
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Retrieve and apply the theme based on the user's preference
            ApplySavedTheme();

            string username = Preferences.Get("Username", string.Empty);

            if (!string.IsNullOrEmpty(username))
            {
                MainPage = new AppShell(); // User is already logged in
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage()); // Force login
            }
        }

        // Apply the saved theme or default to light theme
        private void ApplySavedTheme()
        {
            // Get the saved theme preference, defaulting to "Light" if no preference is saved
            var savedTheme = Preferences.Get("AppTheme", AppTheme.Light.ToString());
            Application.Current.UserAppTheme = (AppTheme)Enum.Parse(typeof(AppTheme), savedTheme);
        }

        // Method to toggle between light and dark modes
        public void ToggleAppTheme()
        {
            // Toggle the app theme between Light and Dark
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                Preferences.Set("AppTheme", AppTheme.Light.ToString()); // Save the new theme
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                Preferences.Set("AppTheme", AppTheme.Dark.ToString()); // Save the new theme
            }
        }
    }
}
