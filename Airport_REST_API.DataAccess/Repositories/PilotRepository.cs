using System.Collections.Generic;
using System.Linq;
using Airport_REST_API.DataAccess.Models;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class PilotRepository : IRepository<Pilot>
    {
        private AirportContext db;

        public PilotRepository(AirportContext context)
        {
            db = context;
        }
        public IEnumerable<Pilot> GetAll()
        {
            return db.Pilots;
        }

        public Pilot Get(int id)
        {
            return db.Pilots.FirstOrDefault(item => item.Id == id);
        }

        public void Add(Pilot entity)
        {
            db.Pilots.Add(entity);
        }

        public void Remove(Pilot entity)
        {
            db.Pilots.Remove(entity);
        }

        public bool UpdateObject(int id, Pilot obj)
        {
            var flag = db.Pilots.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.Pilots.Update(obj);
            db.SaveChanges();
            return true;
        }
    }
}
