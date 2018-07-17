using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport_REST_API.Services.Interfaces { 
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetCollection();
        Task<T> GetObject(int id);
        Task<bool> RemoveObject(int id);
        Task<bool> Add(T obj);
        Task<bool> Update(int id, T obj);
    }
}
