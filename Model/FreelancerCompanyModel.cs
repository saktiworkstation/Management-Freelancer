using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Model
{
    public class FreelancerCompanyModel
    {
        public void Create(int companyId, string userEmail)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Check if the combination of company_id and user_email already exists
                string checkExistingQuery = "SELECT COUNT(*) FROM freelancer_company WHERE company_id = @company_id AND user_email = @user_email";

                using (SqlCommand checkExistingCommand = new SqlCommand(checkExistingQuery, connection))
                {
                    checkExistingCommand.Parameters.AddWithValue("@company_id", companyId);
                    checkExistingCommand.Parameters.AddWithValue("@user_email", userEmail);

                    int existingCount = Convert.ToInt32(checkExistingCommand.ExecuteScalar());

                    if (existingCount > 0)
                    {
                        // User already exists in the company, display a warning message
                        MessageBox.Show("User is already associated with the company.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Insert the data into freelancer_company table
                        string insertQuery = "INSERT INTO freelancer_company (company_id, user_email) VALUES (@company_id, @user_email)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@company_id", companyId);
                            command.Parameters.AddWithValue("@user_email", userEmail);

                            command.ExecuteNonQuery();
                        }

                        //MessageBox.Show("User added to the company successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                connection.Close();
            }
        }
    }
}
