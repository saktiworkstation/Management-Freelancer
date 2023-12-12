using mamajemenFreelance.Controller;
using mamajemenFreelance.Model;
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
    public partial class change_profile : Form
    {
        UserController userController = new UserController();

        public change_profile()
        {
            InitializeComponent();
            InisialisasiProfil();
        }

        private void logolabel_Click(object sender, EventArgs e)
        {

        }

        private void InisialisasiProfil()
        {
            txtUsername.Text = Session.Username;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            string result = userController.UpdateUser(username, password, confirmPassword);

            MessageBox.Show(result, "Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Optionally, you can refresh user details on the form after a successful update.
            InisialisasiProfil();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
