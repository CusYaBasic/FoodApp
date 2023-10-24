

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
                Spacing = 15,
                Padding = 50,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var logoImage = new Image
            {
                Source = "dotnet_bot.png",
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
            };

            loginLabel = new Label
            {
                Text = "Login:",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.FromArgb("#487eb0")
            };

            usernameEntry = new Entry
            {
                Placeholder = "Username",
                WidthRequest = 250
            };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                WidthRequest = 250,
                IsPassword = true
            };

            var rememberMeLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
            };

            rememberMeCheckbox = new CheckBox
            {
               Color = Color.FromArgb("#487eb0")
            };

            rememberMeCheckbox.CheckedChanged += ToggleCheckbox;

            var rememberMeLabel = new Label
            {
                Text = "Remember Me",
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromArgb("#487eb0")

            };

            rememberMeLayout.Children.Add(rememberMeCheckbox);
            rememberMeLayout.Children.Add(rememberMeLabel);

            var buttonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 10
            };

            var loginButton = new Button
            {
                Text = "Login",
                WidthRequest = 250,
                BackgroundColor = Color.FromArgb("#487eb0"),
                
            };

            loginButton.Clicked += OnLoginClicked;

            var registerButton = new Button
            {
                Text = "Register",
                WidthRequest = 250,
                BackgroundColor = Color.FromArgb("##487eb0")
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
            var appShell = App.Current.MainPage as AppShell;
            loginLabel.Text = "Logging in...";

            if(Data.Data.loggedIn)
            {
                IsVisible = false;
                return;
            }

            string response = await API.APIConnector.LoginAccount(usernameEntry.Text, passwordEntry.Text);

            if (response == "Login successful")
            {
                string data = await API.APIConnector.GetUserData(usernameEntry.Text);
                Data.Data.loggedIn = true;
                loginLabel.Text = "Login successful";


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
