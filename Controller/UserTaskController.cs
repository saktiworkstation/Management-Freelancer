using mamajemenFreelance.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Controller
{
    public class UserTaskController
    {
        private TaskModel taskModel = new TaskModel();

        public void AddUserTask(int taskId, string userEmail)
        {
            try
            {
                // Add user task using the TaskModel
                taskModel.AddUserTask(taskId, userEmail);
                MessageBox.Show("User task added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<int> GetAllTaskIds()
        {
            List<int> taskIds = new List<int>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Add a condition to the query to select tasks with the same company_id as Session CompanyId
                string query = "SELECT id FROM task WHERE company_id = @CompanyId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add a parameter for the CompanyId
                    command.Parameters.AddWithValue("@CompanyId", Session.CompanyId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            int taskId = reader.GetInt32(reader.GetOrdinal("id"));
                            taskIds.Add(taskId);
                        }
                    }
                }
            }
            
            return taskIds;
        }


        public List<string> GetFreelancerUserEmails()
        {
            List<string> userEmails = new List<string>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Add conditions to the query to select users based on company_id and user_email
                string query = "SELECT [user].email FROM freelancer_company " +
                                "INNER JOIN [user] ON freelancer_company.user_email = [user].email " +
                                "WHERE freelancer_company.company_id = @CompanyId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for the CompanyId
                    command.Parameters.AddWithValue("@CompanyId", Session.CompanyId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            string userEmail = reader.GetString(reader.GetOrdinal("email"));
                            userEmails.Add(userEmail);
                        }
                    }
                }
            }

            return userEmails;
        }

    }
}
