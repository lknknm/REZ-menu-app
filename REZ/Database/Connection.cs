using System;
using System.Data.SqlClient;

namespace REZ
{
    public class Connection
    {
        SqlConnection sqlConnection = new SqlConnection();


        //----------------------------------------------------------------------------
        public Connection()
        {
            sqlConnection.ConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
        }

        //----------------------------------------------------------------------------
        public SqlConnection Connect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }

        //----------------------------------------------------------------------------
        public void disconnect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) 
            { 
            sqlConnection.Close();
            }  

        }

    }



}