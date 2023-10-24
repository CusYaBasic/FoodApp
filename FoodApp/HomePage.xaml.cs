using FoodApp.API;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using FoodApp.Components;

namespace FoodApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            UpdateNews();
        }

        public async void UpdateNews()
        {
            Data.Data.news = await APIConnector.GetNews();
            foreach (NewsItemView news in Data.Data.news)
            {
                Stack.Children.Add(news);
            }
        }
    }
}