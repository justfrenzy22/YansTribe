using System.ComponentModel.DataAnnotations;
using core.entities;
using core.responses;

namespace dal.responses {
    public class AdminGetUsersRes : BaseRes
    {
        [Display(Name = "users")]
        public List<User>? users { get; set; }
    }
}