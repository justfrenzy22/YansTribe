namespace dal.exceptions
{
    public class DuplicateUserException : BaseException
    {
        public string ConflictingField { get; }

        public DuplicateUserException(string message, string ConflictingField, Exception inner) : base(message, 409, inner) => this.ConflictingField = ConflictingField;


        public DuplicateUserException(string message, string ConflictingField) : base(message, 409) => this.ConflictingField = ConflictingField;

    }
}