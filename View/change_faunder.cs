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

namespace mamajemenFreelance.View
{
    public partial class change_faunder : Form
    {
        private FaunderController faunderController;
        public change_faunder()
        {
            InitializeComponent();
            faunderController = new FaunderController();
            Inisialisasiteks();
        }

        private void Inisialisasiteks()
        {
            txtPhone.Text = Session.phone.ToString();
            txtAddress.Text = Session.address;
            txtGithub.Text = Session.github;
            txtWebsite.Text = Session.website;
            txtInstagram.Text = Session.instagram;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string userEmail = Session.Email;
            int phone = Int32.Parse(txtPhone.Text);
            string address = txtAddress.Text;
            string github = txtGithub.Text;
            string website = txtWebsite.Text;
            string instagram = txtInstagram.Text;

            string result = faunderController.UpdateFaunderProfile(userEmail, phone, address, github, website, instagram);

            MessageBox.Show(result, "Update Faunder Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
