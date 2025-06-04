using System.Data;
using dal.interfaces;
using dal.interfaces.db;
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

        private void AddParameters(SqlCommand cmd, Dictionary<string, object>? parameters)
        {
            try
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        SqlParameter sql_param = new SqlParameter(param.Key, param.Value ?? DBNull.Value);

                        if (param.Value is int)
                        {
                            sql_param.SqlDbType = SqlDbType.Int;
                        }
                        else if (param.Value is Guid)
                        {
                            sql_param.SqlDbType = SqlDbType.UniqueIdentifier;
                        }
                        else if (param.Value is string)
                        {
                            sql_param.SqlDbType = SqlDbType.VarChar;
                        }
                        else if (param.Value is bool)
                        {
                            sql_param.SqlDbType = SqlDbType.Bit;
                        }
                        else if (param.Value is DateTime)
                        {
                            sql_param.SqlDbType = SqlDbType.DateTime;
                        }
                        else if (param.Value is double)
                        {
                            sql_param.SqlDbType = SqlDbType.Float;
                        }
                        else
                        {
                            sql_param.SqlDbType = SqlDbType.VarChar;
                        }

                        cmd.Parameters.Add(sql_param);

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