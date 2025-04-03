using System.Data;

namespace dal.interfaces.db
{
    public interface IDBRepo
    {
        public Task<int> nonQuery(string query, Dictionary<string, object>? parameters = null);
        public Task<object?> scalar(string query, Dictionary<string, object>? parameters = null);
        public Task<DataTable> reader(string query, Dictionary<string, object>? parameters = null);
    }
}