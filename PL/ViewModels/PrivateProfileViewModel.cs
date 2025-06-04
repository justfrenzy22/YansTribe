using core.entities;
using core.enums;
using dal.dto;

namespace pl.viewModel
{
    public class PrivateProfileViewModel
    {
        public required UserAccount user { get; set; }
        public required UserAccount profile { get; set; }
        public required List<Post> posts { get; set; }
    }
}