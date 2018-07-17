using System.Collections.Generic;
using System.Linq;
using Airport_REST_API.DataAccess.Models;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class AircraftTypeRepository : IRepository<AircraftType>
    {
        private AirportContext db;

        public AircraftTypeRepository(AirportContext context)
        {
            db = context;
        }
        public IEnumerable<AircraftType> GetAll()
        {
            return db.AircraftTypes;
        }

        public AircraftType Get(int id)
        {
            return db.AircraftTypes.FirstOrDefault(i => i.Id == id);
        }

        public void Add(AircraftType entity)
        {
           db.AircraftTypes.Add(entity);
        }

        public void Remove(AircraftType entity)
        {
            db.AircraftTypes.Remove(entity);
        }

        public bool UpdateObject(int id, AircraftType obj)
        {
            var flag = db.AircraftTypes.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.AircraftTypes.Update(obj);
            return true;
        }
    }
}
