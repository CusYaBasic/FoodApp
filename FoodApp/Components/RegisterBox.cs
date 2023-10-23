using Microsoft.Maui.Controls;
using System.Security.Cryptography.X509Certificates;

namespace FoodApp.Components
{
    public class RegisterBox : ContentView
    {

        public Entry emailEntry { get; private set; }
        public Entry firstNameEntry { get; private set; }
        public Entry lastNameEntry { get; private set; }
        public Entry telephoneEntry { get; private set; }
        public Entry addressEntry { get; private set; }
        public Entry postcodeEntry { get; private set; }
        public Entry passwordEntry { get; private set; }
        public Entry passwordConfirmEntry { get; private set; }

        public Label errorLabel { get; private set; }


        public RegisterBox()
        {
            var stackLayout = new VerticalStackLayout
            {
                Spacing = 10,
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            emailEntry = new Entry
            {
                Placeholder = "Email",
                Keyboard = Keyboard.Email
            };

            firstNameEntry = new Entry
            {
                Placeholder = "First Name"
            };

            lastNameEntry = new Entry
            {
                Placeholder = "Last Name"
            };

            telephoneEntry = new Entry
            {
                Placeholder = "Telephone",
                Keyboard = Keyboard.Telephone
            };

            addressEntry = new Entry
            {
                Placeholder = "Address Line"
            };

            postcodeEntry = new Entry
            {
                Placeholder = "Postcode"
            };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            passwordConfirmEntry = new Entry
            {
                Placeholder = "Confirm Password",
                IsPassword = true
            };

            errorLabel = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.Center
            };

            var buttonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 10
            };

            var registerButton = new Button
            {
                Text = "Register"
            };
            registerButton.Clicked += OnRegisterClicked;

            var cancelButton = new Button
            {
                Text = "Cancel"
            };
            cancelButton.Clicked += OnCancelClicked;

            buttonsLayout.Children.Add(registerButton);
            buttonsLayout.Children.Add(cancelButton);

            stackLayout.Children.Add(emailEntry);
            stackLayout.Children.Add(firstNameEntry);
            stackLayout.Children.Add(lastNameEntry);
            stackLayout.Children.Add(telephoneEntry);
            stackLayout.Children.Add(addressEntry);
            stackLayout.Children.Add(postcodeEntry);
            stackLayout.Children.Add(passwordEntry);
            stackLayout.Children.Add(passwordConfirmEntry);
            stackLayout.Children.Add(errorLabel);
            stackLayout.Children.Add(buttonsLayout);

            Content = stackLayout;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Clear any previous error messages
            errorLabel.Text = "";

            if (string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                errorLabel.Text = "Email is required.";
                return;
            }

            if (emailEntry.Text.Contains("@") == false || emailEntry.Text.Contains(".") == false)
            {
                errorLabel.Text = "Email is invalid.";
                return;
            }

            if (string.IsNullOrWhiteSpace(firstNameEntry.Text))
            {
                errorLabel.Text = "First name is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(lastNameEntry.Text))
            {
                errorLabel.Text = "Last name is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(telephoneEntry.Text))
            {
                errorLabel.Text = "Telephone is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(addressEntry.Text))
            {
                errorLabel.Text = "Address is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(postcodeEntry.Text))
            {
                errorLabel.Text = "Postcode is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                errorLabel.Text = "Password is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordConfirmEntry.Text))
            {
                errorLabel.Text = "Password confirmation is required.";
                return;
            }

            if (passwordEntry.Text != passwordConfirmEntry.Text)
            {
                errorLabel.Text = "Passwords do not match.";
                return;
            }

            // If all checks pass, attempt to register
             await API.APIConnector.RegisterAccount(emailEntry.Text, firstNameEntry.Text, lastNameEntry.Text, telephoneEntry.Text, addressEntry.Text, postcodeEntry.Text, passwordEntry.Text);
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            this.IsVisible = false;
            var LoginBox = (LoginBox)FindByName("LoginBox");
            LoginBox.IsVisible = true;
        }
    }
}