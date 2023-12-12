using mamajemenFreelance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Controller
{
    public class FaunderController
    {
        private FaunderModel faunderModel;

        public FaunderController()
        {
            faunderModel = new FaunderModel();
        }

        public string UpdateFaunderProfile(string userEmail, int phone, string address, string github, string website, string instagram)
        {
            try
            {
                faunderModel.Update(userEmail, phone, address, github, website, instagram);
                return "Faunder profile updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error updating Faunder profile: {ex.Message}";
            }
        }
    }
}
