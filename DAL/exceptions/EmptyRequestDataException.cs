namespace dal.exceptions
{
    public class EmptyRequestDataException : BaseException
    {
        public EmptyRequestDataException(string message) : base(message, 400) { }
    }
}