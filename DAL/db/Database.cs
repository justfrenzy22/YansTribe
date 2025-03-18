using Microsoft.Data.SqlClient;

namespace DAL.db
{
    public class Database
    {
        private string _connectionString;
        public Database(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public SqlConnection getConn()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}