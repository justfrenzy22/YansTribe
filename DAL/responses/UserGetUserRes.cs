using core.entities;
using core.responses;

namespace dal.responses {
    public class UserGetUserRes: BaseRes {
        public User? user { get; set; }
    }
}