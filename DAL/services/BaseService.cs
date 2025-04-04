using core.responses;
using dal.exceptions;
// using dal.services;

namespace dal.services
{
    public abstract class BaseService
    {
        protected async Task<T> executeSecure<T>(Func<Task<T>> action) where T : BaseRes, new()
        {
            try
            {
                return await action();
            }
            catch (Exception ex) when (ex is EmptyRequestDataException or DuplicateWaitObjectException or NotFoundException or DatabaseOperationException or DataAccessException)
            {
                return new T { check = false, exception = ex.Message };
            }
        }
    }
}