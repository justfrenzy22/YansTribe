using System.Data;
namespace server.interfaces
{
    public interface IDBManager
    {
        int ExecuteNonQuery(string query, Dictionary<string, object>? parameters = null);
        object? ExecuteScalar(string query, Dictionary<string, object>? parameters = null);

        DataTable ExecuteReader(string query, Dictionary<string, object>? parameters = null);
    }
}
