using dal.interfaces.db;

namespace dal.repo
{
    public abstract class BaseRepo
    {
        protected readonly IDBRepo _db_repo;

        public BaseRepo(IDBRepo db_repo) => this._db_repo = db_repo;


    }
}