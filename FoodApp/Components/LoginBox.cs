

using Microsoft.Maui.Controls;

namespace FoodApp.Components
{
    public class LoginBox : ContentView
    {
        public Label loginLabel { get; private set; }
        public Entry usernameEntry { get; private set; }
        public Entry passwordEntry { get; private set; }
        public CheckBox rememberMeCheckbox { get; private set; }

        public LoginBox()
        {
            var stackLayout = new VerticalStackLayout
            {
                Spacing = 25,
                Padding = new Thickness(30, 0),
                VerticalOptions = LayoutOptions.Center
            };

            var logoImage = new Image
            {
                Source = "dotnet_bot.png",
                HeightRequest = 200,
                HorizontalOptions = LayoutOptions.Center
            };

            loginLabel = new Label
            {
                Text = "Login:",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center
            };

            usernameEntry = new Entry
            {
                Placeholder = "Username"
            };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            var rememberMeLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 10
            };

            rememberMeCheckbox = new CheckBox();
            rememberMeCheckbox.CheckedChanged += ToggleCheckbox;

            var rememberMeLabel = new Label
            {
                Text = "Remember Me",
                VerticalOptions = LayoutOptions.Center
            };

            rememberMeLayout.Children.Add(rememberMeCheckbox);
            rememberMeLayout.Children.Add(rememberMeLabel);

            var buttonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var loginButton = new Button
            {
                Text = "Login"
            };
            loginButton.Clicked += OnLoginClicked;

            var registerButton = new Button
            {
                Text = "Register"
            };
            registerButton.Clicked += OnRegisterClicked;

            buttonsLayout.Children.Add(loginButton);
            buttonsLayout.Children.Add(registerButton);

            stackLayout.Children.Add(logoImage);
            stackLayout.Children.Add(loginLabel);
            stackLayout.Children.Add(usernameEntry);
            stackLayout.Children.Add(passwordEntry);
            stackLayout.Children.Add(rememberMeLayout);
            stackLayout.Children.Add(buttonsLayout);

            Content = stackLayout;

            CheckRememberMe();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {

            loginLabel.Text = "Logging in...";

            string response = await API.APIConnector.LoginAccount(usernameEntry.Text, passwordEntry.Text);

            if (response == "Login successful")
            {
                string data = await API.APIConnector.GetUserData(usernameEntry.Text);
                Data.Data.loggedIn = true;
                loginLabel.Text = "Login successful";

                var appShell = App.Current.MainPage as AppShell;
                if (appShell != null)
                {
                    appShell.setTabsVisibility();
                }

            }
            else
            {
                loginLabel.Text = "Login Failed, please check your username and password";
            }
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            this.IsVisible = false;
            var RegisterBox = (RegisterBox)FindByName("RegisterBox");
            RegisterBox.IsVisible = true;
        }

        private void ToggleCheckbox(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Preferences.Set("RememberMe", true);
                Preferences.Set("User", usernameEntry.Text);
                Preferences.Set("Pass", passwordEntry.Text);
            }
            else
            {
                Preferences.Set("RememberMe", false);
                Preferences.Set("User", "");
                Preferences.Set("Pass", "");
            }
        }

        private void CheckRememberMe()
        {
            if (Preferences.ContainsKey("RememberMe"))
            {
                usernameEntry.Text = Preferences.Get("User", "");
                passwordEntry.Text = Preferences.Get("Pass", "");
                rememberMeCheckbox.IsChecked = true;
            }
            else
            {
                Preferences.Set("RememberMe", false);
                Preferences.Set("User", "");
                Preferences.Set("Pass", "");
            }
        }
    }
}
