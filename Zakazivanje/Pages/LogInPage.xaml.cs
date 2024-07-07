using System;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.Maui.Controls;

namespace Zakazivanje.Pages
{
    public partial class LogInPage : ContentPage
    {
        string firstName = string.Empty;
        string lastName = string.Empty;
        string email = string.Empty;
        string id = string.Empty;
        string phoneNumber = string.Empty;
        string address = string.Empty;
        string password = string.Empty;
        string repeatPassword = string.Empty;

        public LogInPage()
        {
            InitializeComponent();
        }

        private async Task<bool> LogIn()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MainPage.connectionString))
                {
                    try
                    {
                        await conn.OpenAsync();

                        string query = "SELECT person_id, first_name, last_name, email, phone_number, street_address, secret_key " +
                                       "FROM person " +
                                       "WHERE email = @Email " +
                                       "AND secret_key = @Password";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                await reader.ReadAsync();

                                id = reader["person_id"].ToString();
                                firstName = reader["first_name"].ToString();
                                lastName = reader["last_name"].ToString();
                                email = reader["email"].ToString();
                                phoneNumber = reader["phone_number"].ToString();
                                address = reader["street_address"].ToString();
                                password = reader["secret_key"].ToString();

                                var app = (Microsoft.Maui.Controls.Application.Current as App);
                                if (app != null)
                                {
                                    app.CurrentCustomer.AssignValues(id, firstName, lastName, email, phoneNumber, address, password);
                                }

                                await DisplayAlert("Login Successful", "You have logged in successfully", "OK");
                                return true;
                            }
                            else
                            {
                                await DisplayAlert("Invalid Credentials", "You entered either wrong email or wrong password", "OK");
                                return false;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        labTitle.Text = $"MySQL Error: {ex.Message}";
                        Console.WriteLine($"MySQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        labTitle.Text = $"Error: {ex.Message}";
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                labTitle.Text = $"MySQL Error: {mysqlEx.Message}";
                Console.WriteLine($"MySQL Error: {mysqlEx.Message}");
            }
            catch (TypeInitializationException tieEx)
            {
                labTitle.Text = $"Type Initialization Error: {tieEx.Message}";
                Console.WriteLine($"Type Initialization Error: {tieEx.Message}");
            }
            catch (Exception ex)
            {
                labTitle.Text = $"General Error: {ex.Message}";
                Console.WriteLine($"General Error: {ex.Message}");
            }
            return false;
        }

        private void RedirectToHomePage()
        {
            if (Navigation != null)
            {
                // Clear the navigation stack and set HomePage as the root
                Navigation.InsertPageBefore(new HomePage(), Navigation.NavigationStack.First());
                Navigation.PopToRootAsync();
                Application.Current.MainPage = new HomeShell();
            }
            else
            {
                // Handle the case where Navigation is null
                DisplayAlert("Navigation Error", "Navigation service is not available.", "OK");
            }
        }

        private async void btnLogIn_Clicked(object sender, EventArgs e)
        {
            email = entEmail.Text;
            password = entPassword.Text;

            bool isLogged = await LogIn();

            if (isLogged)
            {
                RedirectToHomePage();
            }
        }
    }
}
