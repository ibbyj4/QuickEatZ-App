using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace assignment_2425
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(username))
            {
                Preferences.Set("Username", username);

                // Replace current page with AppShell
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Error", "Please enter a username", "OK");
            }
        }

    }
}
