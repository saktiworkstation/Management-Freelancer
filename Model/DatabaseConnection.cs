using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mamajemenFreelance.Model
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString = "Data Source=localhost;Initial Catalog=DB_MFreelance;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
