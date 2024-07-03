using MySqlConnector;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Zakazivanje.Classes;
using static System.Net.Mime.MediaTypeNames;

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

        private async Task<bool> ValidateFormat()
        {
            if (string.IsNullOrEmpty(firstName) || !Regex.IsMatch(firstName, Credentials.namePattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter a valid first name", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(lastName) || !Regex.IsMatch(lastName, Credentials.namePattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter a valid last name", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, Credentials.emailPattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter a valid email", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, Credentials.idPattern))
            {
                await DisplayAlert("Invalid Credentials", "ID must be 13 digits long", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(phoneNumber) || !Regex.IsMatch(phoneNumber, Credentials.phonePattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter a valid phone number (+381xxxxxxxxx)", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(address) || !Regex.IsMatch(address, Credentials.addressPattern))
            {
                await DisplayAlert("Invalid Credentials", "Enter a valid address", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(password) || !Regex.IsMatch(password, Credentials.passwordPattern))
            {
                await DisplayAlert("Invalid Credentials", "Password must be at least 8 characters long, contain at least one uppercase letter, one digit, and one special character.", "OK");
                return false;
            }

            return true; // All validations passed
        }

        private async Task<bool> ValidateInfo()
        {
            string query = "SELECT email, person_id, phone_number FROM person " +
                           "WHERE email = @Email OR person_id = @Id OR phone_number = @PhoneNumber";

            using (MySqlConnection connection = new MySqlConnection(MainPage.connectionString))
            {
                await connection.OpenAsync();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        bool emailExists = false, idExists = false, phoneNumberExists = false;

                        while (await reader.ReadAsync())
                        {
                            if (reader["email"].ToString() == email)
                            {
                                emailExists = true;
                            }
                            if (reader["person_id"].ToString() == id)
                            {
                                idExists = true;
                            }
                            if (reader["phone_number"].ToString() == phoneNumber)
                            {
                                phoneNumberExists = true;
                            }
                        }
                        if (emailExists)
                        {
                            await DisplayAlert("Error", "This email is already associated with an account.", "OK");
                            return false;
                        }
                        if (idExists)
                        {
                            await DisplayAlert("Error", "This ID is already associated with an account.", "OK");
                            return false;
                        }
                        if (phoneNumberExists)
                        {
                            await DisplayAlert("Error", "This phone number is already associated with an account.", "OK");
                            return false;
                        }
                    }
                }
            }

            if (password != repeatPassword)
            {
                await DisplayAlert("Passwords do not match", "Check passwords", "OK");
                return false;
            }

            return true;
        }



        private async Task<bool> ValidateCredentials()
        {
            bool validFormat = await ValidateFormat();

            if (!validFormat)
                return false;


            bool validInfo = await ValidateInfo();

            if (!validInfo)
                return false;

            return true;
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
