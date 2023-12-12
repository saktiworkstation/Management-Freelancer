using mamajemenFreelance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mamajemenFreelance.Controller
{
    public class UserController
    {
        private FreelancerModel freelancerModel = new FreelancerModel();
        private readonly UserModel userModel;

        public UserController()
        {
            this.userModel = new UserModel();
        }

        public string Register(string username, string email, string role, string password, string confirmPassword)
        {
            try
            {
                userModel.Register(username, email, role, password, confirmPassword);
                return "Task created successfully.";
            }
            catch (Exception ex)
            {
                return $"Error creating task: {ex.Message}";
            }
        }

        public bool Login(string email, string password)
        {
            return userModel.Login(email, password);
        }

        public void Logout()
        {
            frm_main main = new frm_main();
            main.Show();
            userModel.Logout();
            MessageBox.Show("Success: Logout Success, All Sessions Cleared", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }


        public static Session Session { get; } = new Session();

        public List<UserFreelancer> GetAllFreelancers()
        {
            try
            {
                return freelancerModel.GetAllFreelancer();
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string UpdateUser(string username, string password, string confirmPassword)
        {
            try
            {
                userModel.Update(username, password, confirmPassword);
                return "User updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error updating user: {ex.Message}";
            }
        }
    }
}
