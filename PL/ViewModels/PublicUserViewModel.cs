namespace pl.viewModel
{
    public class PublicUserViewModel
    {
        public required string user_id { get; set; }
        public required string username { get; set; }
        public required string pfp_src { get; set; }
        public required string full_name { get; set; }
        public required string bio { get; set; }
        public required string location { get; set; }
        public required string website { get; set; }
        public required bool is_private { get; set; }
        public required DateTime created_at { get; set; }
    }
}