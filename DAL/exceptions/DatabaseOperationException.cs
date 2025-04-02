namespace dal.exceptions
{
    public class DatabaseOperationException : Exception
    {
        public int? SqlErrorCode { get; }

        public DatabaseOperationException(string message, Exception inner) : base(message, inner)
        {
            if (inner is Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                SqlErrorCode = sqlEx.Number;
            }
        }

        public DatabaseOperationException(string message) : base(message) { }
    }
}