using mamajemenFreelance.Controller;
using mamajemenFreelance.Modals;
using mamajemenFreelance.Model;
using mamajemenFreelance.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance
{
    public partial class admin_dashboard : Form
    {
        private TaskController taskController = new TaskController();
        private UserController userController = new UserController();
        private FreelancerCompanyController freelancerCompanyController = new FreelancerCompanyController();
        private change_profile changeProfile = new change_profile();
        private FreelancerController freelancerController = new FreelancerController();

        public admin_dashboard()
        {
            InitializeComponent();
            InisialisasiListViewFreelance();
            InisialisasiListViewReportTask();
            bunifuFormDock1.SubscribeControlToDragEvents(sidePanel);
            bunifuFormDock1.SubscribeControlToDragEvents(adminPanel);
            InisialisasiProfil();
            LoadTasksByCompany();
            DisplayFreelancers();
            LoadDDN();
        }

        private void DisplayFreelancers()
        {
            var freelancers = userController.GetAllFreelancers();

            if (freelancers != null)
            {
                // Clear existing items in the ListView
                lvwfreelance.Items.Clear();

                // Add freelancers to the ListView
                foreach (var freelancer in freelancers)
                {
                    ListViewItem item = new ListViewItem(freelancer.Email);
                    item.SubItems.Add(freelancer.Username);
                    item.SubItems.Add(freelancer.Address);
                    item.SubItems.Add(freelancer.Phone.ToString());
                    item.SubItems.Add(freelancer.Instagram);
                    item.SubItems.Add(freelancer.Github);

                    // Add the item to the ListView
                    lvwfreelance.Items.Add(item);
                }
            }
        }

        private void LoadTasksByCompany()
        {
            // Ensure that CompanyId is available in the session
            if (Session.CompanyId > 0)
            {
                // Retrieve tasks for the current company
                var (tasks, message) = taskController.GetTasksByCompany(Session.CompanyId);

                if (tasks != null)
                {
                    // Clear existing items in the ListView
                    lvwReportTask.Items.Clear();

                    // Add tasks to the ListView
                    foreach (var task in tasks)
                    {
                        ListViewItem item = new ListViewItem(task.Id.ToString());
                        item.SubItems.Add(task.CompanyId.ToString());
                        item.SubItems.Add(task.Position);
                        item.SubItems.Add(task.TypeOfWork);
                        item.SubItems.Add(task.Salary.ToString());
                        item.SubItems.Add(task.Job);
                        item.SubItems.Add(task.Status);

                        // Add the item to the ListView
                        lvwReportTask.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show($"Error: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: CompanyId not found in the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InisialisasiProfil()
        {
            // Set User
            lbEmail.Text = Session.Email;
            lbUsername.Text = Session.Username;
            lbRole.Text = Session.Role;

            // Set Faunder
            lbGithub.Text = Session.github;
            lbWebsite.Text = Session.website;
            lbPhone.Text = Session.phone.ToString();
            lbInstagram.Text = Session.instagram;
            lbAddress.Text = Session.address;

            // Side Profil
            lbUname.Text = Session.Username;
            lbURole.Text = Session.Role;
        }


        private void InisialisasiListViewFreelance()
        {
            lvwfreelance.View = System.Windows.Forms.View.Details;
            lvwfreelance.FullRowSelect = true;
            lvwfreelance.GridLines = true;
            lvwfreelance.Columns.Add("Email", 200, HorizontalAlignment.Left);
            lvwfreelance.Columns.Add("Username", 150, HorizontalAlignment.Left);
            lvwfreelance.Columns.Add("Address", 200, HorizontalAlignment.Left);
            lvwfreelance.Columns.Add("Phone", 200, HorizontalAlignment.Left);
            lvwfreelance.Columns.Add("Instagram", 200, HorizontalAlignment.Left);
            lvwfreelance.Columns.Add("Github", 200, HorizontalAlignment.Left);
        }

        private void InisialisasiListViewReportTask()
        {
            lvwReportTask.View = System.Windows.Forms.View.Details;
            lvwReportTask.FullRowSelect = true;
            lvwReportTask.GridLines = true;
            lvwReportTask.Columns.Add("Id  Task", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Id  Company", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Position", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("type of work", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Salary", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Job", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Status", 200, HorizontalAlignment.Left);
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnProject_Click_1(object sender, EventArgs e)
        {
            indicator.Top = ((Control)sender).Top;
            bunifuPages1.SetPage("Project");
            LoadDDN();
        }

        private void btnFreelance_Click_1(object sender, EventArgs e)
        {
            DisplayFreelancers();
            indicator.Top = ((Control)sender).Top;
            bunifuPages1.SetPage("Freelance");
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            LoadTasksByCompany();
            indicator.Top = ((Control)sender).Top;
            bunifuPages1.SetPage("Perusahaan");
        }

        private void btnProfile_Click_1(object sender, EventArgs e)
        {
            InisialisasiProfil();
            indicator.Top = ((Control)sender).Top;
            bunifuPages1.SetPage("Profile");
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            change_faunder changeFaunder = new change_faunder();
            changeFaunder.ShowDialog();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void Profile_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            Modals.add_task add_Task = new Modals.add_task();
            add_Task.ShowDialog();
        }

        private void lvwReportTask_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddToCompany_Click(object sender, EventArgs e)
        {
            if (lvwfreelance.SelectedItems.Count > 0)
            {
                string selectedUserEmail = lvwfreelance.SelectedItems[0].Text;
                freelancerCompanyController.AddToCompany(selectedUserEmail);
            }
            else
            {
                MessageBox.Show("Please select a freelancer to add to the company.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void admin_dashboard_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {
            userController.Logout();
            this.Hide();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            changeProfile.ShowDialog();
        }

        private void btnGiveTask_Click(object sender, EventArgs e)
        {
            // Get the selected task id from the dropdown
            if (ddnTask.SelectedItem != null && ddnFreelancer.SelectedItem != null)
            {
                int taskId = (int)ddnTask.SelectedItem;
                string userEmail = ddnFreelancer.SelectedItem.ToString();

                // Add user task using the UserTaskController
                UserTaskController userTaskController = new UserTaskController();
                userTaskController.AddUserTask(taskId, userEmail);

                // Refresh the ListView or perform any other necessary actions
                LoadTasksByCompany();
            }
            else
            {
                MessageBox.Show("Please select both a task and a user.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadTasksDropdown()
        {
            // Load task ids into the cmbTasks dropdown
            UserTaskController userTaskController = new UserTaskController();
            List<int> taskIds = userTaskController.GetAllTaskIds();

            // Clear existing items in the dropdown
            ddnTask.Items.Clear();

            // Add task ids to the dropdown
            foreach (int taskId in taskIds)
            {
                ddnTask.Items.Add(taskId);
            }
        }

        private void LoadUsersDropdown()
        {
            // Load freelancer user emails into the cmbUsers dropdown
            UserTaskController userTaskController = new UserTaskController();
            List<string> userEmails = userTaskController.GetFreelancerUserEmails();

            // Clear existing items in the dropdown
            ddnFreelancer.Items.Clear();

            // Add user emails to the dropdown
            foreach (string userEmail in userEmails)
            {
                ddnFreelancer.Items.Add(userEmail);
            }
        }

        private void LoadDDN()
        {
            LoadTasksDropdown();
            LoadUsersDropdown();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (lvwReportTask.SelectedItems.Count > 0)
            {
                Console.WriteLine(lvwReportTask.SelectedItems.Count > 0);
                // Get the selected task id from the ListView
                int taskId = Convert.ToInt32(lvwReportTask.SelectedItems[0].SubItems[0].Text);

                // Ask for confirmation before deleting the task
                DialogResult result = MessageBox.Show("Are you sure you want to delete this task and its associated user tasks?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Call the DeleteTaskAndUserTasks method from the TaskController
                    taskController.DeleteTaskWithConfirmation(taskId);

                    // Refresh the ListView or perform any other necessary actions
                    LoadTasksByCompany();

                    MessageBox.Show("Task and associated user tasks deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearchUsername_Click(object sender, EventArgs e)
        {
            // Get the username keyword from the user
            string usernameKeyword = txtUsername.Text.Trim();

            // Check if the keyword is not empty
            if (!string.IsNullOrEmpty(usernameKeyword))
            {
                // Call the SearchFreelancersByUsername method from the UserController
                var (freelancers, message) = freelancerController.SearchFreelancersByUsername(usernameKeyword);

                if (freelancers != null)
                {
                    // Clear existing items in the ListView
                    lvwfreelance.Items.Clear();

                    // Add freelancers to the ListView
                    foreach (var freelancer in freelancers)
                    {
                        ListViewItem item = new ListViewItem(freelancer.Email);
                        item.SubItems.Add(freelancer.Username);
                        item.SubItems.Add(freelancer.Address);
                        item.SubItems.Add(freelancer.Phone.ToString());
                        item.SubItems.Add(freelancer.Instagram);
                        item.SubItems.Add(freelancer.Github);

                        // Add the item to the ListView
                        lvwfreelance.Items.Add(item);
                    }

                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a username keyword to search.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            // Get the job keyword from the user
            string jobKeyword = txtSearchJob.Text.Trim();

            // Check if the keyword is not empty
            if (!string.IsNullOrEmpty(jobKeyword))
            {
                // Call the SearchTasksByJob method from the TaskController
                var (tasks, message) = taskController.SearchTasksByJob(jobKeyword);

                if (tasks != null)
                {
                    // Clear existing items in the ListView
                    lvwReportTask.Items.Clear();

                    // Add tasks to the ListView
                    foreach (var task in tasks)
                    {
                        ListViewItem item = new ListViewItem(task.Id.ToString());
                        item.SubItems.Add(task.CompanyId.ToString());
                        item.SubItems.Add(task.Position);
                        item.SubItems.Add(task.TypeOfWork);
                        item.SubItems.Add(task.Salary.ToString());
                        item.SubItems.Add(task.Job);
                        item.SubItems.Add(task.Status);

                        // Add the item to the ListView
                        lvwReportTask.Items.Add(item);
                    }

                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a job keyword to search.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
