using core.entities;
using core.responses;

namespace server.responses {
    public class UserGetUserRes: BaseRes {
        public User? user { get; set; }
    }
}