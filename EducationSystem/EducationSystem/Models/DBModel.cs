using EducationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EducationSystem.Models
{
    public class DBModel
    {
        public static SqlConnection Get_DB_Connection()
        {
            string connetionString = @"Data Source=" + Configuration.SERVERNAME + ";Initial Catalog=" + Configuration.DBNAME + ";User ID=" + Configuration.USERNAME + ";Password=" + Configuration.PASSWORD;
            SqlConnection connection = new SqlConnection(connetionString);
            return connection;
        }
    }
}