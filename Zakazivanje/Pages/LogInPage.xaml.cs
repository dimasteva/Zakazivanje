using System;
using MySqlConnector;
using Microsoft.Maui.Controls;

namespace Zakazivanje.Pages
{
    public partial class LogInPage : ContentPage
    {
        string email = string.Empty;
        string secretKey = String.Empty;
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
                       
                        string query = "SELECT COUNT(*) AS count_persons " +
                                       "FROM person " +
                                       "WHERE email = @Email " +
                                       "AND secret_key = @SecretKey";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SecretKey", secretKey);

                        object result = await cmd.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            int countPersons = Convert.ToInt32(result);
                            if (countPersons > 0)
                            {
                                await DisplayAlert("Login Successful", "You have logged in successfully", "OK");
                            }
                            else
                            {
                                await DisplayAlert("Invalid Credentials", "You entered either wrong email or wrong password", "OK");
                                return false;
                            }
                        }
                        else
                        {
                            labTitle.Text = "Query returned null or empty result.";
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
            return true;
        }

        private void LogIntoAccount()
        {
            Navigation.PushAsync(new HomePage());
        }
        private async void btnLogIn_Clicked(object sender, EventArgs e)
        {
            email = entEmail.Text;
            secretKey = entPassword.Text;

            bool isLogged = await LogIn();
            
            if (isLogged)
            {
                LogIntoAccount();
            }
        }
    }
}
