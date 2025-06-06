using core.enums;
// using dal.interfaces;
using dal.interfaces.db;

namespace dal.repo
{
    public abstract class BaseUserRepo : BaseRepo
    {
        public BaseUserRepo(IDBRepo db_repo) : base(db_repo) { }

        public Role ParseRole(string value)
        {
            if (value == "user" || value == "User")
            {
                return Role.User;
            }
            else if (value == "admin" || value == "Admin")
            {
                return Role.Admin;
            }
            else if (value == "superadmin" || value == "SuperAdmin")
            {
                return Role.SuperAdmin;
            }
            else
            {
                throw new Exception("Invalid role value");
            }
        }

        public string ParseStringRole(Role role)
        {
            if (role == Role.User)
            {
                return "user";
            }
            else if (role == Role.Admin)
            {
                return "admin";
            }
            else
            {
                return "superadmin";
            }
        }

        public FriendShipStatus ParseFriendStatus(string value) => (FriendShipStatus)Enum.Parse(typeof(FriendShipStatus), value, true);
    }
}