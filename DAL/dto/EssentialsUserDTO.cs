using core.enums;

namespace dal.dto
{
    public class EssentialsUserDTO
    {
        public required Guid user_id { get; set; }
        public required string username { get; set; }
        public required string pfp_src { get; set; }
    }
}