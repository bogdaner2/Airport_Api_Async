using System.Collections.Generic;
using System.Linq;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class DepartureRepository : IRepository<Departures>
    {
        private AirportContext db;

        public DepartureRepository(AirportContext context)
        {
            db = context;
        }
        public IEnumerable<Departures> GetAll()
        {
            return db.Departures.Include(d => d.Aircraft).Include(c => c.Crew);
        }

        public Departures Get(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id == id);
        }

        public void Add(Departures entity)
        {
           db.Departures.Add(entity);
        }

        public void Remove(Departures entity)
        {
            db.Departures.Remove(entity);
        }

        public bool UpdateObject(int id, Departures obj)
        {
            var flag = db.Departures.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.Departures.Update(obj);
            return true;
        }
    }
}
