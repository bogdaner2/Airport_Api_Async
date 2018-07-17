using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Aircraft>> GetAllAsync()
        {
            return await db.Aircrafts.Include(a => a.Type).ToListAsync();
        }

        public async Task<Aircraft> GetAsync(int id)
        {
            return await db.Aircrafts.Include(a => a.Type).FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateAsync(Aircraft entity)
        {
            await db.Aircrafts.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var aircraft = await db.Aircrafts.FindAsync(id).ConfigureAwait(false);
            if (aircraft != null)
            {
                db.Aircrafts.Remove(aircraft);
            }
        }

        public bool Update(int id, Aircraft obj)
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
