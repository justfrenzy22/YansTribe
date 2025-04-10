using System.ComponentModel.DataAnnotations;
using core.entities;
using core.responses;

namespace server.responses {
    public class AdminGetUsersRes : BaseRes
    {
    [Display(Name = "users")]
    public List<User>? users { get; set; }

    }

}