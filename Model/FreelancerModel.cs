using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Model
{
    public class FreelancerModel
    {
        public void Create(string userEmail)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO [user_freelancer] (user_email) VALUES (@user_email)";

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
                string query = "UPDATE [user_freelancer] SET phone = @phone, address = @address, github = @github, website = @website, instagram = @instagram WHERE user_email = @user_email";

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

        public List<UserFreelancer> GetAllFreelancer()
        {
            List<UserFreelancer> freelancers = new List<UserFreelancer>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT u.id, u.username, u.email, u.role, uf.phone, uf.address, uf.github, uf.website, uf.instagram " +
                               "FROM [user] u " +
                               "INNER JOIN [user_freelancer] uf ON u.email = uf.user_email " +
                               "WHERE u.role = 'Freelancer'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            freelancers.Add(new UserFreelancer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Role = reader.GetString(reader.GetOrdinal("role")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? 00 : reader.GetInt32(reader.GetOrdinal("phone")),
                                Address = reader.IsDBNull(reader.GetOrdinal("address")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("address")),
                                Github = reader.IsDBNull(reader.GetOrdinal("github")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("github")),
                                Website = reader.IsDBNull(reader.GetOrdinal("website")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("website")),
                                Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("instagram"))
                            });
                        }
                    }
                }
            }

            return freelancers;
        }

        public List<UserFreelancer> SearchFreelancersByUsername(string username)
        {
            List<UserFreelancer> freelancers = new List<UserFreelancer>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT u.id, u.username, u.email, u.role, uf.phone, uf.address, uf.github, uf.website, uf.instagram " +
                               "FROM [user] u " +
                               "INNER JOIN [user_freelancer] uf ON u.email = uf.user_email " +
                               "WHERE u.role = 'Freelancer' AND u.username LIKE @username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", $"%{username}%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            freelancers.Add(new UserFreelancer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Role = reader.GetString(reader.GetOrdinal("role")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? 00 : reader.GetInt32(reader.GetOrdinal("phone")),
                                Address = reader.IsDBNull(reader.GetOrdinal("address")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("address")),
                                Github = reader.IsDBNull(reader.GetOrdinal("github")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("github")),
                                Website = reader.IsDBNull(reader.GetOrdinal("website")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("website")),
                                Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "Masih Kosong" : reader.GetString(reader.GetOrdinal("instagram"))
                            });
                        }
                    }
                }
            }

            return freelancers;
        }


    }
}
