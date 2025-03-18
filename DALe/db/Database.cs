using System;
using System.Data;
using System.Data.SqlClient;


namespace DAL.db
{
    public class Database
    {
        private string _connectionString;

        public Database(string connectionString)
        {
            this._connectionString = connectionString;
        }

        // ADD ENV configuration

        public SqlConnection getConn()
        {
            return new SqlConnection(this._connectionString);
        }


    }
}