namespace dal.exceptions
{
    public class DataAccessException : BaseException
    {
        public DataAccessException(string message) : base(message, 402) { }
        public DataAccessException(string message, Exception inner) : base(message, 402, inner) { }
    }
}