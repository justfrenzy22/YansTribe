namespace pl.dto
{
    public class BaseUserDTO
    {
        public BaseUserDTO(string user_id, string username, string pfp_src)
        {
            this.user_id = user_id;
            this.username = username;
            this.pfp_src = pfp_src;
        }

        public string user_id { get; set; }
        public string username { get; set; }
        public string pfp_src { get; set; }
    }
}