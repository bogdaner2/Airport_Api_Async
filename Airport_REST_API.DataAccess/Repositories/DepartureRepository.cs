using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Departures>> GetAllAsync()
        {
            return await db.Departures.Include(d => d.Aircraft).Include(c => c.Crew).ToListAsync();
        }

        public async Task<Departures> GetAsync(int id)
        {
            return await db.Departures.Include(d => d.Aircraft).Include(c => c.Crew).FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateAsync(Departures entity)
        {
           await db.Departures.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var departure = await db.Departures.FindAsync(id).ConfigureAwait(false);
            if (departure != null)
            {
                db.Departures.Remove(departure);
            }
        }

        public bool Update(int id, Departures obj)
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
