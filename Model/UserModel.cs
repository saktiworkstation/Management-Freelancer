using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Model
{
    public class UserModel
    {
        public void Register(string username, string email, string role, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                // Handle password mismatch
                MessageBox.Show("Password mismatch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the email already exists
            if (IsEmailExists(email))
            {
                // Handle case when the email is already in use
                MessageBox.Show("Email is already in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO [user] (username, email, role, password) VALUES (@username, @email, @role, @password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@role", role);
                    command.Parameters.AddWithValue("@password", password);

                    command.ExecuteNonQuery();
                }

                if (role == "Freelancer")
                {
                    FreelancerModel freelancerModel = new FreelancerModel();
                    freelancerModel.Create(email);
                }
                else if (role == "Faunder")
                {
                    FaunderModel fraunderModel = new FaunderModel();
                    CompanyModel companyModel = new CompanyModel();
                    companyModel.Create(email);
                    fraunderModel.Create(email);
                }

                connection.Close();
            }
        }
        private bool IsEmailExists(string email)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM [user] WHERE email = @email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the email already exists
                    return count > 0;
                }
            }
        }

        public bool Login(string email, string password)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM [user] WHERE email = @email AND password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Set session data
                            Session.UserId = reader.GetInt32(reader.GetOrdinal("id"));
                            Session.Username = reader.GetString(reader.GetOrdinal("username"));
                            Session.Email = reader.GetString(reader.GetOrdinal("email"));
                            Session.Role = reader.GetString(reader.GetOrdinal("role"));

                            // Mendapatkan companyId dari user_email
                            string user_email = Session.Email;
                            int companyId = GetCompanyId(user_email);

                            // Set companyId ke dalam session
                            Session.CompanyId = companyId;

                            // Cek then input to Session
                            if (Session.Role == "Freelancer")
                            {
                                GetFreelancerByUser(user_email);
                            }
                            else if (Session.Role == "Faunder")
                            {
                                GetFaunderByUser(user_email);
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private int GetCompanyId(string user_email)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string companyIdQuery = "SELECT id FROM [company] WHERE user_email = @user_email";

                using (SqlCommand companyIdCommand = new SqlCommand(companyIdQuery, connection))
                {
                    companyIdCommand.Parameters.AddWithValue("@user_email", user_email);
                    return Convert.ToInt32(companyIdCommand.ExecuteScalar());
                }
            }
        }

        private void GetFaunderByUser(string user_email)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT phone, address, github, website, instagram FROM [user_faunder] WHERE user_email = @user_email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", user_email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Set session data
                                Session.phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? 0 : reader.GetInt32(reader.GetOrdinal("phone"));
                                Session.address = reader.IsDBNull(reader.GetOrdinal("address")) ? "kosong" : reader.GetString(reader.GetOrdinal("address"));
                                Session.github = reader.IsDBNull(reader.GetOrdinal("github")) ? "kosong" : reader.GetString(reader.GetOrdinal("github"));
                                Session.website = reader.IsDBNull(reader.GetOrdinal("website")) ? "kosong" : reader.GetString(reader.GetOrdinal("website"));
                                Session.instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "kosong" : reader.GetString(reader.GetOrdinal("instagram"));
                            }
                        }
                        else
                        {
                            // Set session data to "kosong" if no rows are found
                            SetSessionToKosong();
                        }

                        // Close the SqlDataReader before leaving the using block
                        reader.Close();
                    }
                }
                connection.Close();
            }
        }

        private void GetFreelancerByUser(string user_email)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT phone, address, github, website, instagram FROM [user_freelancer] WHERE user_email = @user_email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", user_email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Set session data
                                Session.phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? 0 : reader.GetInt32(reader.GetOrdinal("phone"));
                                Session.address = reader.IsDBNull(reader.GetOrdinal("address")) ? "kosong" : reader.GetString(reader.GetOrdinal("address"));
                                Session.github = reader.IsDBNull(reader.GetOrdinal("github")) ? "kosong" : reader.GetString(reader.GetOrdinal("github"));
                                Session.website = reader.IsDBNull(reader.GetOrdinal("website")) ? "kosong" : reader.GetString(reader.GetOrdinal("website"));
                                Session.instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "kosong" : reader.GetString(reader.GetOrdinal("instagram"));
                            }
                        }
                        else
                        {
                            // Set session data to "kosong" if no rows are found
                            SetSessionToKosong();
                        }

                        // Close the SqlDataReader before leaving the using block
                        reader.Close();
                    }
                }
                connection.Close();
            }
        }

        private void SetSessionToKosong()
        {
            // Set session data to "kosong"
            Session.phone = 0;
            Session.address = "kosong";
            Session.github = "kosong";
            Session.website = "kosong";
            Session.instagram = "kosong";
        }


        public void Logout()
        {
            // Reset session data
            Session.UserId = 0;
            Session.Username = null;
            Session.Email = null;
            Session.Role = null;
        }

        public void Update(string username, string password, string confirmPassword)
        {
            // Ensure that the password and confirmPassword match
            if (password != confirmPassword)
            {
                // Handle password mismatch
                return;
            }

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE [user] SET username = @username, password = @password WHERE id = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", Session.UserId);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    command.ExecuteNonQuery();
                }
                Session.Username = username;
                connection.Close();
            }
        }
    }
}
