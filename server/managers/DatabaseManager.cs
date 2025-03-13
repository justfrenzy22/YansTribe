using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace server.managers
{
    public abstract class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected int nonQuery(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParameters(command, parameters);
                        connection.Open();
                        return command.ExecuteNonQuery();
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

        protected object? scalar(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParameters(command, parameters);
                        connection.Open();
                        return command.ExecuteScalar();
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

        protected DataTable reader(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                    {
                        AddParameters(dataAdapter.SelectCommand, parameters);
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table);  // Populate the DataTable with results
                        return table;
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

        protected void AddParameters(SqlCommand command, Dictionary<string, object>? parameters)
        {
            try
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        if (param.Value == null)
                        {
                            // If value is null, use DBNull
                            command.Parameters.Add(param.Key, SqlDbType.VarChar).Value = DBNull.Value;
                        }
                        else
                        {
                            // Explicitly define the SQL type for strings to avoid any ambiguity
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
