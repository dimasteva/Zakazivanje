using MySqlConnector;
using System.Text.RegularExpressions;
using Zakazivanje.Classes;

namespace Zakazivanje.Pages
{
    public partial class RegisterPage : ContentPage
    {
        string firstName = string.Empty;
        string lastName = string.Empty;
        string email = string.Empty;
        string id = string.Empty;
        string phoneNumber = string.Empty;
        string address = string.Empty;
        string password = string.Empty;
        string repeatPassword = string.Empty;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async Task<bool> ValidateCredentials()
        {
            if (!Regex.IsMatch(email, Credentials.emailPattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter valid email", "OK");
                return false;
            }

            if (!Regex.IsMatch(password, Credentials.passwordPattern))
            {
                await DisplayAlert("Invalid Credentials", "Password must be at least 8 characters long, contain at least one uppercase letter, one digit, and one special character.", "OK");
                return false;
            }

            if (!Regex.IsMatch(firstName, Credentials.namePattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter valid first name", "OK");
                return false;
            }

            if (!Regex.IsMatch(lastName, Credentials.namePattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter valid last name", "OK");
                return false;
            }

            if (!Regex.IsMatch(address, Credentials.addressPattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter valid address", "OK");
                return false;
            }

            if (!Regex.IsMatch(phoneNumber, Credentials.phonePattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter valid phone number (+381xxxxxxxxx)", "OK");
                return false;
            }

            if (!Regex.IsMatch(id, Credentials.idPattern))
            {
                await DisplayAlert("Invalid Credentials", "ID must be 13 digits long", "OK");
                return false;
            }

            return true; // All validations passed
        }

        private async void CreateAccount() //you should check for already existing credentials
        {
            string query = "INSERT INTO person " +
                           "VALUES (@id, @firstName, @lastName, @address, @email, @phoneNumber, @password);";

            using (MySqlConnection connection = new MySqlConnection(MainPage.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    await connection.OpenAsync();
                    int result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        await DisplayAlert("Registration Succesfull", "You registered succesfully!", "OK");
                    }
                    else
                    {
                        //TODO
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error occured", ex.Message, "OK");
                }
            }
        }

        private void RedirectToHomePage()
        {
            Navigation.PushAsync(new HomePage());
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            firstName = entFirstName.Text;
            lastName = entLastName.Text;
            email = entEmail.Text;
            id = entID.Text;
            phoneNumber = entPhoneNumber.Text;
            address = entAddress.Text;
            password = entPassword.Text;
            repeatPassword = entRepeatPassword.Text;
            
            bool valid = await ValidateCredentials();

            if (!valid)
                return;

            CreateAccount();

            RedirectToHomePage();
        }
    }
}
