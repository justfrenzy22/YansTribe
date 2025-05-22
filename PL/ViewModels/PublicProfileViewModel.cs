using core.entities;
using core.enums;
using dal.dto;

namespace pl.viewModel
{
    public class PublicProfileViewModel
    {
        public required PublicUserViewModel user { get; set; }
        public required List<Post> posts { get; set; }
    }
}