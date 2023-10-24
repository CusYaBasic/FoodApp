namespace FoodApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
    }

	public void setTabsVisibility()
	{
		AccountTab.IsVisible = true;
		CartTab.IsVisible = true;
		MenuTab.IsVisible = true;
		LoginTab.IsVisible = false;
        TabBarApp.CurrentItem = NewsTab;
        TabBarApp.Items.Remove(LoginTab);

    }
}
