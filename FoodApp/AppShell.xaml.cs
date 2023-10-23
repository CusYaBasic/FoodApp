namespace FoodApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
    }

	public void setTabsVisibility()
	{
        NewsTab.IsVisible = true;
		AccountTab.IsVisible = true;
		CartTab.IsVisible = true;
		MenuTab.IsVisible = true;
		TabBarApp.CurrentItem = NewsTab;
		LoginTab.IsEnabled = false;
		LoginTab.IsVisible = false;
    }
}
