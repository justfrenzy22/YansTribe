namespace dal.exceptions
{
    public class EmptyRequestDataException : Exception
    {
        public EmptyRequestDataException(string message) : base(message) { }
    }
}