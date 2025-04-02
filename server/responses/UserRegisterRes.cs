using System.ComponentModel.DataAnnotations;
using core.responses;

namespace server.responses
{
    public class UserRegisterRes : BaseRes
    {
        [Display(Name = "user_id")]
        public int? user_id { get; set; }
    }
}