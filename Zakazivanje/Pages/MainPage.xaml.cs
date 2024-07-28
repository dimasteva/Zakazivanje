using Zakazivanje.Pages;

namespace Zakazivanje
{
    public partial class MainPage : ContentPage
    {
        public static string connectionString = "Had_To_Change_This";

        public MainPage()
        {
            InitializeComponent();

        }

        private void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LogInPage());
        }

        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }

}
