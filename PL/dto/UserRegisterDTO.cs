namespace pl.dto
{
    public class UserRegisterDTO
    {
        public required string username { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public required string full_name { get; set; }
        public required string bio { get; set; }
        public required string location { get; set; }
        public required string website { get; set; }
        public required DateTime created_at { get; set; }

    }
}