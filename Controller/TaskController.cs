using mamajemenFreelance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mamajemenFreelance.Controller
{
    public class TaskController
    {
        private readonly TaskModel taskModel;

        public TaskController()
        {
            this.taskModel = new TaskModel();
        }

        public string CreateTask(string position, string typeOfWork, int salary, string job, string descriptions)
        {
            try
            {
                taskModel.Create(position, typeOfWork, salary, job, descriptions);
                return "Task created successfully.";
            }
            catch (Exception ex)
            {
                return $"Error creating task: {ex.Message}";
            }
        }

        public string UpdateTask(int taskId, int companyId, string position, int typeOfWork, int salary, string job, string descriptions)
        {
            try
            {
                taskModel.Update(taskId, companyId, position, typeOfWork, salary, job, descriptions);
                return "Task updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error updating task: {ex.Message}";
            }
        }

        public (List<Tasks>, string) GetAllTasks()
        {
            try
            {
                List<Tasks> tasks = taskModel.GetAll();
                return (tasks, "All tasks retrieved successfully.");
            }
            catch (Exception ex)
            {
                return (null, $"Error retrieving tasks: {ex.Message}");
            }
        }

        public (List<Tasks>, string) GetTasksByUser(string userEmail)
        {
            try
            {
                List<Tasks> tasks = taskModel.GetByUser(userEmail);
                return (tasks, "Tasks for user retrieved successfully.");
            }
            catch (Exception ex)
            {
                return (null, $"Error retrieving tasks for user: {ex.Message}");
            }
        }

        public (List<Tasks>, string) GetTasksByCompany(int companyId)
        {
            try
            {
                List<Tasks> tasks = taskModel.GetByCompany(companyId);
                return (tasks, "Tasks for company retrieved successfully.");
            }
            catch (Exception ex)
            {
                return (null, $"Error retrieving tasks for company: {ex.Message}");
            }
        }

        public string GrantTask(int taskId)
        {
            try
            {
                taskModel.GrantTask(taskId);
                return "Task granted successfully.";
            }
            catch (InvalidOperationException ex)
            {
                return $"Error granting task: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error granting task: {ex.Message}";
            }
        }

        public void DeleteTaskWithConfirmation(int taskId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this task? (yes/no)", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                taskModel.DeleteTaskAndUserTasks(taskId);
                MessageBox.Show("Task deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Task deletion canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public (List<Tasks>, string) SearchTasksByJob(string keyword)
        {
            try
            {
                List<Tasks> tasks = taskModel.SearchByJob(keyword);
                return (tasks, "Tasks retrieved successfully.");
            }
            catch (Exception ex)
            {
                return (null, $"Error searching tasks by job: {ex.Message}");
            }
        }


    }
}
