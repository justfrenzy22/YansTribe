namespace dal.exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, 400) { }
    }
}