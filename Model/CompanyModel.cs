using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Model
{
    public class CompanyModel
    {
        public void Create(string userEmail)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO [Company] (user_email) " +
                               "VALUES (@user_email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", userEmail);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(int companyId, string name, string descriptions, string address)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE [Company] SET name = @name, descriptions = @descriptions, address = @address " +
                               "WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", companyId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@descriptions", descriptions);
                    command.Parameters.AddWithValue("@address", address);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Company> GetAll()
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM [Company]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                companies.Add(new Company
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Descriptions = reader.GetString(reader.GetOrdinal("descriptions")),
                                    Address = reader.GetString(reader.GetOrdinal("address")),
                                    UserEmail = reader.GetString(reader.GetOrdinal("user_email"))
                                });
                            }
                        }
                    }
                }
                connection.Close();
            }

            return companies;
        }
    }
}

