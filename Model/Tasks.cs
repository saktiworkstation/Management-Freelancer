using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Model
{
    public class Tasks
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Position { get; set; }
        public string TypeOfWork { get; set; }
        public int Salary { get; set; }
        public string Job { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }
    }
}
