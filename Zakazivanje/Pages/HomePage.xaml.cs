namespace Zakazivanje.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		
		var app =  (Microsoft.Maui.Controls.Application.Current as App);

		if (app != null)
		{
			test.Text = app.CurrentCustomer.Email;
		}
	}
	
}