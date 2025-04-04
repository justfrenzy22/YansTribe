namespace dal.exceptions {
    public class DataAccessException : Exception {
        public DataAccessException(string message) : base(message) { }
        public DataAccessException(string message, Exception inner) : base(message, inner) { }
    }
}