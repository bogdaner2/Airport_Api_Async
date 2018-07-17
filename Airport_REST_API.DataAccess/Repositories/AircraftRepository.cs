using System.Collections.Generic;
using System.Linq;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class AircraftRepository : IRepository<Aircraft>
    {
        private AirportContext db;

        public AircraftRepository(AirportContext context)
        {
            db = context;
        }
        public IEnumerable<Aircraft> GetAll()
        {
            return db.Aircrafts.Include(a => a.Type);
        }

        public Aircraft Get(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id == id);
        }

        public void Add(Aircraft entity)
        {
            db.Aircrafts.Add(entity);
        }

        public void Remove(Aircraft entity)
        {
            db.Aircrafts.Remove(entity);
        }

        public bool UpdateObject(int id, Aircraft obj)
        {
            var flag = db.Aircrafts.Count(item => item.Id == id) == 0 ;
            if (flag)
                return false;
            obj.Id = id;
            db.Aircrafts.Update(obj);
            return true;
        }
    }
}
