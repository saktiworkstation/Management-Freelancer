using mamajemenFreelance.Controller;
using mamajemenFreelance.Model;
using mamajemenFreelance.Pages;
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
    public partial class login_form : Form
    {
        private readonly UserController userController;

        public login_form()
        {
            InitializeComponent();
            this.userController = new UserController();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("login");
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("regist");

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string email = txtLEmail.Text;
            string password = txtLPassword.Text;

            if (userController.Login(email, password))
            {
                // Login successful, redirect to appropriate form based on role
                if (Session.Role == "Freelancer")
                {
                    // Open Freelancer form
                    freelance_form formFreelancer = new freelance_form();
                    formFreelancer.Show();
                }
                else if (Session.Role == "Faunder")
                {
                    // Open Fraunder form
                    admin_dashboard formFraunder = new admin_dashboard();
                    formFraunder.Show();
                }

                // Close current form (login form)
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string role = ddnRole.SelectedItem.ToString();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            try
            {
                userController.Register(username, email, role, password, confirmPassword);
                MessageBox.Show("registration Success", "User Registration Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("registration Faild", "User Registration Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
