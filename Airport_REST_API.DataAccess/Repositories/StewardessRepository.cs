using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Airport_REST_API.DataAccess.Models;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class StewardessRepository : IRepository<Stewardess>
    {
        private AirportContext db;
        public StewardessRepository(AirportContext context)
        {
            db = context;
        }
        public IEnumerable<Stewardess> GetAll()
        {
            return db.Stewardesses;
        }

        public Stewardess Get(int id)
        {
            return db.Stewardesses.FirstOrDefault(item => item.Id == id);
        }

        public void Add(Stewardess entity)
        {
            db.Stewardesses.Add(entity);
        }

        public void Remove(Stewardess entity)
        {
            db.Stewardesses.Remove(entity);
        }

        public bool UpdateObject(int id, Stewardess obj)
        {
            var flag = db.Stewardesses.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.Stewardesses.Update(obj);
            return true;
        }
    }
}
