namespace dal.exceptions
{
    public class DuplicateUserException : Exception
    {
        public string ConflictingField { get; }

        public DuplicateUserException(string message, string ConflictingField, Exception inner) : base(message, inner) => this.ConflictingField = ConflictingField;

        public DuplicateUserException(string message, string ConflictingField) : base(message) => this.ConflictingField = ConflictingField;
    }
}