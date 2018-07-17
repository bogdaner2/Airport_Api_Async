using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport_REST_API.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task CreateAsync(T entity);
        Task DeleteAsync(int id);
        bool Update(int id, T obj);
    }
}
