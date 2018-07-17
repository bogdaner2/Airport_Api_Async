using System.Collections.Generic;

namespace Airport_REST_API.Services.Interfaces { 
    public interface IService<T>
    {
        IEnumerable<T> GetCollection();
        T GetObject(int id);
        bool RemoveObject(int id);
        bool Add(T obj);
        bool Update(int id, T obj);
    }
}
