using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Model
{
    public class TaskModel
    {
        public void Create(string position, string typeOfWork, int salary, string job, string descriptions)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Mendapatkan user_email dari Session
                string user_email = Session.Email;

                // Query untuk mendapatkan id company berdasarkan user_email
                string companyIdQuery = "SELECT id FROM [company] WHERE user_email = @user_email";

                using (SqlCommand companyIdCommand = new SqlCommand(companyIdQuery, connection))
                {
                    companyIdCommand.Parameters.AddWithValue("@user_email", user_email);

                    // Mengeksekusi query untuk mendapatkan id company
                    int companyId = Convert.ToInt32(companyIdCommand.ExecuteScalar());
                    int status = 0;

                    // Query untuk menyimpan data task
                    string query = "INSERT INTO task (position, company_id, type_of_work, salary, job, descriptions, status) " +
                                   "VALUES (@position, @company_id, @type_of_work, @salary, @job, @descriptions, @status)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@position", position);
                        command.Parameters.AddWithValue("@company_id", companyId); // Menambahkan company_id ke dalam parameter
                        command.Parameters.AddWithValue("@type_of_work", typeOfWork);
                        command.Parameters.AddWithValue("@salary", salary);
                        command.Parameters.AddWithValue("@job", job);
                        command.Parameters.AddWithValue("@descriptions", descriptions);
                        command.Parameters.AddWithValue("@status", status);

                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

        public void Update(int taskId, int companyId, string position, int typeOfWork, decimal salary, string job, string descriptions)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE task SET company_id = @company_id, position = @position, " +
                               "type_of_work = @type_of_work_id, salary = @salary, job = @job, descriptions = @descriptions " +
                               "WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", taskId);
                    command.Parameters.AddWithValue("@company_id", companyId);
                    command.Parameters.AddWithValue("@position", position);
                    command.Parameters.AddWithValue("@type_of_work", typeOfWork);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.Parameters.AddWithValue("@job", job);
                    command.Parameters.AddWithValue("@descriptions", descriptions);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Tasks> GetAll()
        {
            List<Tasks> tasks = new List<Tasks>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM task";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            tasks.Add(new Tasks
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                CompanyId = reader.GetInt32(reader.GetOrdinal("company_id")),
                                Position = reader.GetString(reader.GetOrdinal("position")),
                                TypeOfWork = reader.GetString(reader.GetOrdinal("type_of_work")),
                                Salary = reader.GetInt32(reader.GetOrdinal("salary")),
                                Job = reader.GetString(reader.GetOrdinal("job")),
                                Descriptions = reader.GetString(reader.GetOrdinal("descriptions")),
                                Status = MapStatus(reader["status"])
                            });
                        }
                    }
                }
                connection.Close();
            }

            return tasks;
        }

        public List<Tasks> GetByUser(string userEmail)
        {
            List<Tasks> tasks = new List<Tasks>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Ambil semua data user_task dengan data user_task.user_email yang sama dengan session.Email
                string userTaskQuery = "SELECT id_task FROM user_task WHERE user_email = @user_email";

                using (SqlCommand userTaskCommand = new SqlCommand(userTaskQuery, connection))
                {
                    userTaskCommand.Parameters.AddWithValue("@user_email", userEmail);

                    using (SqlDataReader userTaskReader = userTaskCommand.ExecuteReader())
                    {
                        while (userTaskReader.HasRows && userTaskReader.Read())
                        {
                            int taskId = userTaskReader.GetInt32(userTaskReader.GetOrdinal("id_task"));

                            // Ambil data task dengan task.id yang sama dengan user_task.id_task
                            using (SqlConnection innerConnection = DatabaseConnection.GetConnection())
                            {
                                innerConnection.Open();

                                string taskQuery = "SELECT t.*, ut.user_email FROM task t " +
                                                   "INNER JOIN user_task ut ON t.id = ut.id_task " +
                                                   "WHERE t.id = @task_id";

                                using (SqlCommand taskCommand = new SqlCommand(taskQuery, innerConnection))
                                {
                                    taskCommand.Parameters.AddWithValue("@task_id", taskId);

                                    using (SqlDataReader taskReader = taskCommand.ExecuteReader())
                                    {
                                        while (taskReader.HasRows && taskReader.Read())
                                        {
                                            tasks.Add(new Tasks
                                            {
                                                Id = taskReader.GetInt32(taskReader.GetOrdinal("id")),
                                                CompanyId = taskReader.GetInt32(taskReader.GetOrdinal("company_id")),
                                                Position = taskReader.GetString(taskReader.GetOrdinal("position")),
                                                TypeOfWork = taskReader.GetString(taskReader.GetOrdinal("type_of_work")),
                                                Salary = taskReader.GetInt32(taskReader.GetOrdinal("salary")),
                                                Job = taskReader.GetString(taskReader.GetOrdinal("job")),
                                                Descriptions = taskReader.GetString(taskReader.GetOrdinal("descriptions")),
                                                Status = MapStatus(taskReader["status"])
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return tasks;
        }


        public List<Tasks> GetByCompany(int companyId)
        {
            List<Tasks> tasks = new List<Tasks>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM task WHERE company_id = @company_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@company_id", companyId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            tasks.Add(new Tasks
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                CompanyId = reader.GetInt32(reader.GetOrdinal("company_id")),
                                Position = reader.GetString(reader.GetOrdinal("position")),
                                TypeOfWork = reader.GetString(reader.GetOrdinal("type_of_work")),
                                Salary = reader.GetInt32(reader.GetOrdinal("salary")),
                                Job = reader.GetString(reader.GetOrdinal("job")),
                                Descriptions = reader.GetString(reader.GetOrdinal("descriptions")),
                                Status = MapStatus(reader["status"])
                            });
                        }
                    }
                }
                connection.Close();
            }

            return tasks;
        }

        private string MapStatus(object statusValue)
        {
            if (statusValue == DBNull.Value || statusValue == null)
            {
                return "Status not available";
            }

            if (int.TryParse(statusValue.ToString(), out int status))
            {
                switch (status)
                {
                    case 0:
                        return "Belum dikerjakan";
                    case 1:
                        return "Sedang dikerjakan";
                    case 2:
                        return "Sedang pengecekan";
                    case 3:
                        return "Selesai";
                    default:
                        return "Status tidak valid";
                }
            }
            else
            {
                return "Status conversion error";
            }
        }

        public void AddUserTask(int taskId, string userEmail)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Check if the user task already exists
                if (IsUserTaskExists(userEmail, taskId))
                {
                    // Handle case when the user task already exists using MessageBox
                    MessageBox.Show("User task already exists. Cannot override.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Query to insert data into user_task table
                string query = "INSERT INTO user_task (user_email, id_task) " +
                               "VALUES (@user_email, @id_task)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", userEmail);
                    command.Parameters.AddWithValue("@id_task", taskId);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private bool IsUserTaskExists(string userEmail, int taskId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM user_task WHERE user_email = @user_email AND id_task = @id_task";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", userEmail);
                    command.Parameters.AddWithValue("@id_task", taskId);

                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the user task already exists
                    return count > 0;
                }
            }
        }

        public void GrantTask(int taskId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                try
                {
                    string getStatusQuery = "SELECT status FROM [task] WHERE id = @task_id";

                    using (SqlCommand getStatusCommand = new SqlCommand(getStatusQuery, connection))
                    {
                        getStatusCommand.Parameters.AddWithValue("@task_id", taskId);

                        int currentStatus = Convert.ToInt32(getStatusCommand.ExecuteScalar());

                        Console.WriteLine($"Current Status: {currentStatus}");

                        if (currentStatus == 3)
                        {
                            throw new InvalidOperationException("Task is already marked as completed.");
                        }

                        int newStatus = (currentStatus < 3) ? currentStatus + 1 : currentStatus;

                        string grantTaskQuery = "UPDATE [task] SET status = @new_status WHERE id = @task_id";

                        using (SqlCommand grantTaskCommand = new SqlCommand(grantTaskQuery, connection))
                        {
                            grantTaskCommand.Parameters.AddWithValue("@task_id", taskId);
                            grantTaskCommand.Parameters.AddWithValue("@new_status", newStatus);

                            grantTaskCommand.ExecuteNonQuery();
                        }

                        Console.WriteLine("Task granted successfully.");
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteTaskAndUserTasks(int taskId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete user tasks associated with the task
                        string deleteUserTasksQuery = "DELETE FROM [user_task] WHERE id_task = @task_id";
                        using (SqlCommand deleteUserTasksCommand = new SqlCommand(deleteUserTasksQuery, connection, transaction))
                        {
                            deleteUserTasksCommand.Parameters.AddWithValue("@task_id", taskId);
                            deleteUserTasksCommand.ExecuteNonQuery();
                        }

                        // Delete the task
                        string deleteTaskQuery = "DELETE FROM [task] WHERE id = @task_id";
                        using (SqlCommand deleteTaskCommand = new SqlCommand(deleteTaskQuery, connection, transaction))
                        {
                            deleteTaskCommand.Parameters.AddWithValue("@task_id", taskId);
                            deleteTaskCommand.ExecuteNonQuery();
                        }

                        // Commit the transaction if everything is successful
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and log the details
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine($"StackTrace: {ex.StackTrace}");

                        // Roll back the transaction if something goes wrong
                        transaction.Rollback();
                    }
                }

                connection.Close();
            }
        }

        public List<Tasks> SearchByJob(string keyword)
        {
            List<Tasks> tasks = new List<Tasks>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Menambahkan kondisi WHERE untuk memfilter berdasarkan Session.CompanyId
                string query = "SELECT * FROM task WHERE job LIKE @keyword AND company_id = @company_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                    // Menambahkan parameter untuk Session.CompanyId
                    command.Parameters.AddWithValue("@company_id", Session.CompanyId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.Read())
                        {
                            tasks.Add(new Tasks
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                CompanyId = reader.GetInt32(reader.GetOrdinal("company_id")),
                                Position = reader.GetString(reader.GetOrdinal("position")),
                                TypeOfWork = reader.GetString(reader.GetOrdinal("type_of_work")),
                                Salary = reader.GetInt32(reader.GetOrdinal("salary")),
                                Job = reader.GetString(reader.GetOrdinal("job")),
                                Descriptions = reader.GetString(reader.GetOrdinal("descriptions")),
                                Status = MapStatus(reader["status"])
                            });
                        }
                    }
                }

                connection.Close();
            }

            return tasks;
        }

        public List<Tasks> SearchTasksByUserEmail(string userEmail, string keyword)
        {
            List<Tasks> tasks = new List<Tasks>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Ambil semua data user_task dengan data user_task.user_email yang sama dengan userEmail
                string userTaskQuery = "SELECT id_task FROM user_task WHERE user_email = @user_email";

                using (SqlCommand userTaskCommand = new SqlCommand(userTaskQuery, connection))
                {
                    userTaskCommand.Parameters.AddWithValue("@user_email", userEmail);

                    using (SqlDataReader userTaskReader = userTaskCommand.ExecuteReader())
                    {
                        while (userTaskReader.HasRows && userTaskReader.Read())
                        {
                            int taskId = userTaskReader.GetInt32(userTaskReader.GetOrdinal("id_task"));

                            // Ambil data task dengan task.id yang sama dengan user_task.id_task
                            string taskQuery = "SELECT * FROM task WHERE id = @task_id AND job LIKE @keyword";

                            using (SqlCommand taskCommand = new SqlCommand(taskQuery, connection))
                            {
                                taskCommand.Parameters.AddWithValue("@task_id", taskId);
                                taskCommand.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                                using (SqlDataReader taskReader = taskCommand.ExecuteReader())
                                {
                                    while (taskReader.HasRows && taskReader.Read())
                                    {
                                        tasks.Add(new Tasks
                                        {
                                            Id = taskReader.GetInt32(taskReader.GetOrdinal("id")),
                                            CompanyId = taskReader.GetInt32(taskReader.GetOrdinal("company_id")),
                                            Position = taskReader.GetString(taskReader.GetOrdinal("position")),
                                            TypeOfWork = taskReader.GetString(taskReader.GetOrdinal("type_of_work")),
                                            Salary = taskReader.GetInt32(taskReader.GetOrdinal("salary")),
                                            Job = taskReader.GetString(taskReader.GetOrdinal("job")),
                                            Descriptions = taskReader.GetString(taskReader.GetOrdinal("descriptions")),
                                            Status = MapStatus(taskReader["status"])
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return tasks;
        }

        public List<Tasks> SearchByJobAndUserEmail(string keyword, string userEmail)
        {
            List<Tasks> tasks = new List<Tasks>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Ambil semua data user_task dengan data user_task.user_email yang sama dengan userEmail
                string userTaskQuery = "SELECT id_task FROM user_task WHERE user_email = @user_email";

                using (SqlCommand userTaskCommand = new SqlCommand(userTaskQuery, connection))
                {
                    userTaskCommand.Parameters.AddWithValue("@user_email", userEmail);

                    using (SqlDataReader userTaskReader = userTaskCommand.ExecuteReader())
                    {
                        while (userTaskReader.HasRows && userTaskReader.Read())
                        {
                            int taskId = userTaskReader.GetInt32(userTaskReader.GetOrdinal("id_task"));

                            // Ambil data task dengan task.id yang sama dengan user_task.id_task
                            using (SqlConnection innerConnection = DatabaseConnection.GetConnection())
                            {
                                innerConnection.Open();

                                string taskQuery = "SELECT t.*, ut.user_email FROM task t " +
                                                   "INNER JOIN user_task ut ON t.id = ut.id_task " +
                                                   "WHERE t.id = @task_id AND job LIKE @keyword";

                                using (SqlCommand taskCommand = new SqlCommand(taskQuery, innerConnection))
                                {
                                    taskCommand.Parameters.AddWithValue("@task_id", taskId);
                                    taskCommand.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                                    using (SqlDataReader taskReader = taskCommand.ExecuteReader())
                                    {
                                        while (taskReader.HasRows && taskReader.Read())
                                        {
                                            tasks.Add(new Tasks
                                            {
                                                Id = taskReader.GetInt32(taskReader.GetOrdinal("id")),
                                                CompanyId = taskReader.GetInt32(taskReader.GetOrdinal("company_id")),
                                                Position = taskReader.GetString(taskReader.GetOrdinal("position")),
                                                TypeOfWork = taskReader.GetString(taskReader.GetOrdinal("type_of_work")),
                                                Salary = taskReader.GetInt32(taskReader.GetOrdinal("salary")),
                                                Job = taskReader.GetString(taskReader.GetOrdinal("job")),
                                                Descriptions = taskReader.GetString(taskReader.GetOrdinal("descriptions")),
                                                Status = MapStatus(taskReader["status"])
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return tasks;
            }
        }

    }
}
