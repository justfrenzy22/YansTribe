using System.ComponentModel.DataAnnotations;

namespace pl.viewModel
{
    public class AdminChangeRoleViewModel
    {
        [Required]
        public required string user_id { get; set; }

        [Required]
        public required string role { get; set; }
    }
}