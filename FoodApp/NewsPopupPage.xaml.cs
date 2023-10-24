using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using FoodApp.Components;

namespace FoodApp;

public partial class NewsPopupPage : ContentPage
{

    public string NewsTitleString { get; set; }
    public string DescriptionString { get; set; }
    public string NewsImageString { get; set; }

    public NewsPopupPage()
	{
		InitializeComponent();
	}

    public void UpdateNewsItem()
    {
        NewsImage.Source = NewsImageString;
        NewsTitle.Text = NewsTitleString;
        Description.Text = DescriptionString;
    }
}

