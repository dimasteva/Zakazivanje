using Zakazivanje.Pages;

namespace Zakazivanje
{
    public partial class MainPage : ContentPage
    {
        public static string connectionString = "Server=sql7.freesqldatabase.com;Database=sql7716092;User ID=sql7716092;Password=PpY2jsqI5h;Port=3306;";

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
