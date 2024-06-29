using MySql.Data.MySqlClient;

namespace Zakazivanje.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void btnRegister_Clicked(object sender, EventArgs e)
    {
        string firstName = entFirstName.Text;
        string lastName = entLastName.Text;
        string email = entEmail.Text;
        string id = entID.Text;
        string phoneNumber = entPhoneNumber.Text;
        string address = entAddress.Text;
        string password = entPassword.Text;
        string repeatPassword = entRepeatPassword.Text;

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
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    labTitle.Text = "Bravo";
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                // Log the exception and update the label to show failure
                labTitle.Text = ex.Message;
            }
        }
    }

}