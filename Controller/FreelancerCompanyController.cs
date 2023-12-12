using mamajemenFreelance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Controller
{
    public class FreelancerCompanyController
    {
        private FreelancerCompanyModel freelancerCompanyModel = new FreelancerCompanyModel();

        public void AddToCompany(string selectedUserEmail)
        {
            try
            {
                // Ensure that CompanyId is available in the session
                if (Session.CompanyId > 0)
                {
                    freelancerCompanyModel.Create(Session.CompanyId, selectedUserEmail);
                    MessageBox.Show("Freelancer added to company successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error: CompanyId not found in the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding freelancer to company: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
