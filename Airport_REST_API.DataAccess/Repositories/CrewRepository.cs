using System.Collections.Generic;
using System.Linq;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class CrewRepository : IRepository<Crew>
    {
        private AirportContext db;

        public CrewRepository(AirportContext context)
        {
            db = context;
        }
        public IEnumerable<Crew> GetAll()
        {
            return db.Crews.Include(c => c.Stewardesses).Include(c => c.Pilot);
        }

        public Crew Get(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id == id);
        }

        public void Add(Crew entity)
        {
            db.Crews.Add(entity);
        }

        public void Remove(Crew entity)
        {
            db.Crews.Remove(entity);
        }

        public bool UpdateObject(int id, Crew obj)
        {
            var flag = db.Crews.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.Crews.Update(obj);
            return true;
        }
    }
}
