using dal.interfaces.db;

namespace dal.repo
{
    public abstract class BaseRepo
    {
        protected readonly IDBRepo db_repo;

        public BaseRepo(IDBRepo db_repo) => this.db_repo = db_repo;


    }
}