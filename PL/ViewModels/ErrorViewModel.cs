namespace pl.viewModel
{
    public class ErrViewModel
    {
        public int status { get; set; }
        public string message { get; set; } = "";

        public override string ToString()
        {
            return $"{{ \"statusCode\": {this.status}, \"message\": \"{this.message}\" }}";
        }
    }
}
