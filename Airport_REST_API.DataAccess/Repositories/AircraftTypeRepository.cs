using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class AircraftTypeRepository : IRepository<AircraftType>
    {
        private AirportContext db;

        public AircraftTypeRepository(AirportContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<AircraftType>> GetAllAsync()
        {
            return await db.AircraftTypes.ToListAsync();
        }

        public async Task<AircraftType> GetAsync(int id)
        {
            return await db.AircraftTypes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task CreateAsync(AircraftType entity)
        {
           await db.AircraftTypes.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var type = await db.AircraftTypes.FindAsync(id).ConfigureAwait(false);
            if (type != null)
            {
                db.AircraftTypes.Remove(type);
            }
        }

        public bool Update(int id, AircraftType obj)
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
