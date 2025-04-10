namespace pl.dto
{
    public class ErrDTO
    {
        public int status { get; set; }
        public string message { get; set; } = "";

        public override string ToString()
        {
            return $"{{ \"statusCode\": {this.status}, \"message\": \"{this.message}\" }}";
        }
    }
}
