using core.enums;
using dal.interfaces;

public abstract class BaseUserRepo
{
    private readonly IDBRepo _dbRepo;

    public BaseUserRepo(IDBRepo dbRepo) => this._dbRepo = dbRepo;

    // public IDBRepo dbRepo { get; set; }

    public Role ParseRole<Role>(string value) => (Role)Enum.Parse(typeof(Role), value, true);
}