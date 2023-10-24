using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Components
{
    public  class NewsItemView : ContentView
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsImage { get; set; }

        public static readonly BindableProperty SourceProperty =
           BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(NewsItemView), default(Image));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("LabelText", typeof(string), typeof(NewsItemView), default(string));

        public string LabelText
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public NewsItemView()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                //Aspect = Aspect.AspectFill,
            };
            image.SetBinding(Image.SourceProperty, new Binding(nameof(ImageSource), source: this));

            var label = new Label
            {
                HeightRequest = 75,
                WidthRequest = 200,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 0, 10, 0)
            };
            label.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));

            var button = new Button
            {
                Text = "View",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 0, 10, 0),
                BackgroundColor = Color.FromArgb("#487eb0"),
                WidthRequest = 100
            };

            var flexLayout = new FlexLayout
            {
                Direction = FlexDirection.Row,
                JustifyContent = FlexJustify.SpaceBetween,
                Padding = new Thickness(20, 10),
                Children = { label, button }
            };

            Content = new Frame
            {
                CornerRadius = 10,
                HasShadow = true,
                BorderColor = Color.FromRgba(0,0,0,0),
                BackgroundColor = Colors.Azure,
                Content = new StackLayout
                {
                    Spacing = 10,
                    Children = { new Frame { Padding = 0, Content = image }, flexLayout }
                }
            };

            button.Clicked += Button_Clicked;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            NewsPopupPage news = new NewsPopupPage();
            news.NewsTitleString = Title;
            news.DescriptionString = Description;
            news.NewsImageString = NewsImage;
            news.UpdateNewsItem();
            await Navigation.PushAsync(news);
        }
    }
}
