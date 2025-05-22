using core.entities;
using core.enums;
using dal.dto;

namespace pl.viewModel
{
    public class PrivateProfileViewModel
    {
        public required BaseUser user { get; set; }
        public required BaseUser profile { get; set; }
        public required List<Post> posts { get; set; }
    }
}