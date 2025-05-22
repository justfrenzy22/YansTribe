namespace dal.dto
{
    public class FriendUserDTO
    {
        public string status { get; set; } = "";
        public Guid user_id { get; set; }
        public string username { get; set; } = "";
        public string pfp_src { get; set; } = "";
        public bool is_private { get; set; }
    }
}