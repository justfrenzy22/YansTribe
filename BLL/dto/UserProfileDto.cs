using core.entities;
using dal.dto;
using System.Collections.Generic;

namespace bll.dto
{
    public class UserProfileDto
    {
        public required SafeUser user { get; set; }
    }
}
