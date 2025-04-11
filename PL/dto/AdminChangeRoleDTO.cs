using System.ComponentModel.DataAnnotations;

namespace pl.dto
{
    public class AdminChangeRoleDTO
    {
        [Required]
        public required string user_id { get; set; }

        [Required]
        public required string role { get; set; }
    }
}