using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BoiMela.DataAccess
{
    public class Connection
    {
        public static SqlConnection GetSqlConnection()
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["boimelaDb"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnStr);
            return conn;

        }
    }
}