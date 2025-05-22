using core.entities;

namespace pl.viewModel
{
    public class BaseUserViewModel
    {
        public required string user_id { get; set; }
        public required string username { get; set; }
        public required string pfp_src { get; set; }
        public required Notifications notifications { get; set; }
    }
}