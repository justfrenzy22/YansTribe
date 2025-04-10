namespace dal.exceptions
{
    public class BaseException : Exception
    {
        public int status { get; }
        public BaseException(string message, int status) : base(message) => this.status = status;
        public BaseException(string message, int status, Exception inner) : base(message, inner) => this.status = status;
    }
}