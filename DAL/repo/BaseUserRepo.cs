using core.enums;
// using dal.interfaces;
using dal.interfaces.db;

namespace dal.repo
{
    public abstract class BaseUserRepo : BaseRepo
    {
        public BaseUserRepo(IDBRepo db_repo) : base(db_repo) { }

        // private readonly IDBRepo _dbRepo;

        // public BaseUserRepo(IDBRepo dbRepo) => this._dbRepo = dbRepo;

        // public IDBRepo dbRepo { get; set; }

        // public Role ParseRole<Role>(string value) => (Role)Enum.Parse(typeof(Role), value, true);
        public Role ParseRole(string value)
        {
            if (value == "user")
            {
                return Role.User;
            }
            else if (value == "admin")
            {
                return Role.Admin;
            }
            else if (value == "superadmin")
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
    }
}