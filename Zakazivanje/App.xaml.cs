using Zakazivanje.Classes;

namespace Zakazivanje
{
    public partial class App : Application
    {
        public Customer CurrentCustomer { get; private set; }
        public App()
        {
            InitializeComponent();
            CurrentCustomer = new Customer();

            MainPage = new NavigationPage(new MainPage()); 
        }
    }
}
