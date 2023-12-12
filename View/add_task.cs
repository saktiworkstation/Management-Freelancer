using mamajemenFreelance.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Modals
{
    public partial class add_task : Form
    {
        private readonly TaskController taskController;

        public add_task()
        {
            InitializeComponent();
            taskController = new TaskController();
        }

        private void add_task_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values from the form controls
                string position = txtPosition.Text;
                string typeOfWork = ddnTypeOfWork.SelectedItem.ToString();
                int salary = Convert.ToInt32(txtSalary.Text);
                string job = txtJob.Text;
                string descriptions = txtDescriptions.Text;

                // Call the controller method to create a task
                string result = taskController.CreateTask(position, typeOfWork, salary, job, descriptions);

                // Display the result to the user
                MessageBox.Show(result, "Task Creation Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during task creation
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
