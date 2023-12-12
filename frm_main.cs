using mamajemenFreelance.Modals;
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
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            login_form login = new login_form();
            login.Show();

            this.Hide();
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
