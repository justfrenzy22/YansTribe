using core.entities;
using core.enums;

namespace dal.dto
{
    public class ProfileUserDTO
    {
        public required Guid user_id { get; set; }
        public required string username { get; set; }
        public required string email { get; set; }
        public required string full_name { get; set; }
        public required string bio { get; set; }
        public required string pfp_src { get; set; }
        public required string location { get; set; }
        public required string website { get; set; }
        public required bool is_private { get; set; }
        public required DateTime created_at { get; set; }
        public required Role role { get; set; }
        public required int friends_num { get; set; }
        public required RelationShipInfo relationship_info { get; set; }
    }
}