using Microsoft.Maui.Controls;

namespace assignment_2425
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Save the user data (using Preferences for this example)
                Preferences.Set("Username", username);
                Preferences.Set("Password", password);

                // Navigate to Login page after successful registration
                await DisplayAlert("Success", "You have registered successfully.", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
            }
        }
    }
}
