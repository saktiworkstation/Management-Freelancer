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

namespace mamajemenFreelance.Pages
{
    public partial class freelance_form : Form
    {
        private UserController userController = new UserController();
        private TaskController taskController = new TaskController();
        private change_profile changeProfile = new change_profile();
        private TaskModel taskModel = new TaskModel();

        public freelance_form()
        {
            InitializeComponent();
            InisialisasiProfil();
            InisialisasiTask();
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

        private void SetupListView()
        {
            lvwReportTask.View = System.Windows.Forms.View.Details;
            lvwReportTask.FullRowSelect = true;
            lvwReportTask.GridLines = true;
            lvwReportTask.Columns.Add("Id  Task", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Id Company", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Position", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Type of Work", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Salary", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Job", 100, HorizontalAlignment.Left);
            lvwReportTask.Columns.Add("Status", 200, HorizontalAlignment.Left);
        }

        public void InisialisasiTask()
        {
            lvwReportTask.Items.Clear(); // Clear existing items before reloading
            SetupListView();

            // Mendapatkan data tugas (tasks) berdasarkan UserId dari sesi saat ini
            TaskController taskController = new TaskController();
            var (tasks, message) = taskController.GetTasksByUser(Session.Email);

            if (tasks != null)
            {
                // Menampilkan data tugas ke dalam ListView
                foreach (var task in tasks)
                {
                    ListViewItem item = new ListViewItem(task.Id.ToString());
                    item.SubItems.Add(task.CompanyId.ToString());
                    item.SubItems.Add(task.Position);
                    item.SubItems.Add(task.TypeOfWork);
                    item.SubItems.Add(task.Salary.ToString());
                    item.SubItems.Add(task.Job);
                    item.SubItems.Add(task.Status);
                    lvwReportTask.Items.Add(item);
                }
            }
            else
            {
                // Menampilkan pesan kesalahan jika ada
                MessageBox.Show($"Error: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        Modals.change_profile form_edit_profile = new Modals.change_profile();
        Modals.change_password form_ubah_pass = new Modals.change_password();   


        private void searchTask_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (lvwReportTask.SelectedItems.Count > 0)
            {
                // Mendapatkan id task dari item yang dipilih
                int selectedTaskId = Convert.ToInt32(lvwReportTask.SelectedItems[0].SubItems[0].Text);

                // Menangani GrantTask
                string resultMessage = taskController.GrantTask(selectedTaskId);

                // Menampilkan pesan hasil operasi
                MessageBox.Show(resultMessage, "Grant Task", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh data tugas setelah operasi GrantTask
                InisialisasiTask();
            }
            else
            {
                MessageBox.Show("Please select a task from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            InisialisasiTask();
            indicator.Top = ((Control)sender).Top;
            bunifuPages1.SetPage("taskList");
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            InisialisasiProfil();
            indicator.Top = ((Control)sender).Top;
            bunifuPages1.SetPage("profileFreelance");
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            form_edit_profile.ShowDialog();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            form_ubah_pass.ShowDialog();
        }

        private void bunifuBanner_Click(object sender, EventArgs e)
        {

        }

        private void freelance_form_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            userController.Logout();
            this.Hide();
        }

        private void lvwReportTask_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            changeProfile.ShowDialog();
            InisialisasiProfil();
        }

        private void btnEdirFaunder_Click(object sender, EventArgs e)
        {
            change_feelancer change = new change_feelancer();
            change.ShowDialog();
        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchJob.Text;

            // Memanggil fungsi model untuk melakukan pencarian
            List<Tasks> searchResults = taskModel.SearchByJobAndUserEmail(keyword, Session.Email);

            // Menangani hasil pencarian
            if (searchResults.Count > 0)
            {
                // Menampilkan hasil ke dalam ListView
                lvwReportTask.Items.Clear();
                SetupListView();

                foreach (var task in searchResults)
                {
                    ListViewItem item = new ListViewItem(task.Id.ToString());
                    item.SubItems.Add(task.CompanyId.ToString());
                    item.SubItems.Add(task.Position);
                    item.SubItems.Add(task.TypeOfWork);
                    item.SubItems.Add(task.Salary.ToString());
                    item.SubItems.Add(task.Job);
                    item.SubItems.Add(task.Status);
                    lvwReportTask.Items.Add(item);
                }

                MessageBox.Show("Pencarian berhasil!");
            }
            else
            {
                MessageBox.Show("Tidak ada hasil yang ditemukan.");
            }
        }
    }
}
