using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport_REST_API.Services.Interfaces { 
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetCollectionAsync();
        Task<T> GetObjectAsync(int id);
        Task<bool> DeleteObjectAsync(int id);
        Task<bool> CreateObjectAsync(T obj);
        Task<bool> UpdateObjectAsync(int id, T obj);
    }
}
