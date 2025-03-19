using System.Data;
using dal.interfaces;
using Microsoft.Data.SqlClient;

namespace dal.repo
{
    public class DBRepo : IDBRepo
    {

        private readonly string connString;

        public DBRepo(string connString)
        {
            this.connString = connString;
        }


        public async Task<int> nonQuery(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParameters(command, parameters);
                        await connection.OpenAsync();
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error in nonQuery: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error in nonQuery: {ex.Message}", ex);
            }
        }

        public async Task<object?> scalar(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParameters(command, parameters);
                        await connection.OpenAsync();
                        return await command.ExecuteScalarAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error in scalar: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error in scalar: {ex.Message}", ex);
            }
        }

        public async Task<DataTable> reader(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParameters(command, parameters);
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            return table;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error in reader: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error in reader: {ex.Message}", ex);
            }
        }

        private void AddParameters(SqlCommand command, Dictionary<string, object>? parameters)
        {
            try
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        if (param.Value == null)
                        {
                            command.Parameters.Add(param.Key, SqlDbType.VarChar).Value = DBNull.Value;
                        }
                        else
                        {
                            command.Parameters.Add(param.Key, SqlDbType.VarChar).Value = param.Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding parameters: {ex.Message}", ex);
            }
        }
    }
}