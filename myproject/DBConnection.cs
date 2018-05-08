using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myproject
{
    class DBConnection
    {
        public DBConnection()
        {
        }

       
        public static SqlConnection Instance()
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connetionString = @"Data Source=THANGVQ\SQLEXPRESS;Initial Catalog=quanlydat;User ID=root3;Password=111111";
            sql = "SELECT * FROM users";
            connection = new SqlConnection(connetionString);

            return connection;
        }

        
    }
}
