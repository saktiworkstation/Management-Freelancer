using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Model
{
    public class FaunderModel
    {
        public void Create(string userEmail)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO [user_faunder] (user_email) VALUES (@user_email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", userEmail);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(string userEmail, int phone, string address, string github, string website, string instagram)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE [user_faunder] SET phone = @phone, address = @address, github = @github, website = @website, instagram = @instagram WHERE user_email = @user_email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", userEmail);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@address", address ?? "Kosong");
                    command.Parameters.AddWithValue("@github", github ?? "Kosong");
                    command.Parameters.AddWithValue("@website", website ?? "Kosong");
                    command.Parameters.AddWithValue("@instagram", instagram ?? "Kosong");

                    command.ExecuteNonQuery();
                }

                Session.phone = phone; // Assuming phone is an int in Session
                Session.address = address ?? "Kosong";
                Session.github = github ?? "Kosong";
                Session.website = website ?? "Kosong";
                Session.instagram = instagram ?? "Kosong";


                connection.Close();
            }
        }
    }
}
