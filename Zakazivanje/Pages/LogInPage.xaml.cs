using System;
using MySql.Data.MySqlClient;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel.Communication;

namespace Zakazivanje.Pages
{
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void btnLogIn_Clicked(object sender, EventArgs e)
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(MainPage.connectionString))
                {
                    try
                    {
                        conn.Open();
                        string email = entEmail.Text;
                        string secretKey = entPassword.Text;

                        string query = "SELECT COUNT(*) AS count_persons " +
                                       "FROM person " +
                                       "WHERE email = @Email " +
                                       "AND secret_key = @SecretKey";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SecretKey", secretKey);

                        // ExecuteScalar is used since you are expecting a single value (count_persons)
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int countPersons = Convert.ToInt32(result);
                            if (countPersons > 0)
                            {
                                labTitle.Text = "aga"; // Found matching person
                            }
                            else
                            {
                                labTitle.Text = "aha"; // No matching person
                            }
                        }
                        else
                        {
                            labTitle.Text = "Query returned null or empty result."; // Handle no result scenario
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., SQL errors, connection errors)
                        labTitle.Text = "Error: " + ex.Message;
                    }
                }


            }
            catch (MySqlException mysqlEx)
            {
                Console.WriteLine($"MySQL Error: {mysqlEx.Message}");
                //ludo.Text = $"MySQL Error: {mysqlEx.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                //ludo.Text = $"General Error: {ex.Message}";
            }
        }
    }
}
