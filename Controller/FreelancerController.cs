using mamajemenFreelance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Controller
{
    public class FreelancerController
    {
        private FreelancerModel freelancerModel;

        public FreelancerController()
        {
            freelancerModel = new FreelancerModel();
        }

        public string UpdateFreelancerProfile(string userEmail, int phone, string address, string github, string website, string instagram)
        {
            try
            {
                freelancerModel.Update(userEmail, phone, address, github, website, instagram);
                return "Faunder profile updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error updating Faunder profile: {ex.Message}";
            }
        }

        public (List<UserFreelancer>, string) SearchFreelancersByUsername(string username)
        {
            try
            {
                List<UserFreelancer> freelancers = freelancerModel.SearchFreelancersByUsername(username);

                if (freelancers != null && freelancers.Count > 0)
                {
                    return (freelancers, "Freelancers retrieved successfully.");
                }
                else
                {
                    return (null, "No freelancers found matching the search criteria.");
                }
            }
            catch (Exception ex)
            {
                return (null, $"Error searching freelancers: {ex.Message}");
            }
        }

    }
}
