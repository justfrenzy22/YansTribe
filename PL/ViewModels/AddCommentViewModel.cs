namespace pl.viewModel
{
    public class AddCommentViewModel
    {
        public required string post_id { get; set; }
        public required string user_id { get; set; }
        public required string content { get; set; }
    }
}