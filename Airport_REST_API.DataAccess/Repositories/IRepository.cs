using System.Collections.Generic;

namespace Airport_REST_API.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Remove(T entity);
        bool UpdateObject(int id, T obj);
    }
}
