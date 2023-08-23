using System;
using System.Data.SqlClient;

namespace REZ
{
    public class Connection
    {
        SqlConnection sqlConnection = new SqlConnection();

        public Connection()
        {
            sqlConnection.ConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
        }

        public SqlConnection Connect()
        {
            return sqlConnection;
        }

        public void disconnect()
        {

        }

    }



}