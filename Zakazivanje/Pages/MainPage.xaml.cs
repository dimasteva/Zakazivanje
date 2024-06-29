using Zakazivanje.Pages;

namespace Zakazivanje
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

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

        }
    }

}
