using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class PilotRepository : IRepository<Pilot>
    {
        private AirportContext db;

        public PilotRepository(AirportContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Pilot>> GetAllAsync()
        {
            return await db.Pilots.ToListAsync();
        }

        public async Task<Pilot> GetAsync(int id)
        {
            return await db.Pilots.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateRangeAsync(List<Pilot> entity)
        {
            await db.Pilots.AddRangeAsync(entity);
        }

        public async Task<Pilot> GetLastAsync()
        {
            return await db.Pilots.LastOrDefaultAsync();
        }

        public async Task CreateAsync(Pilot entity)
        {
            await db.Pilots.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var pilot = await db.Pilots.FindAsync(id).ConfigureAwait(false);
            if (pilot != null)
            {
                db.Pilots.Remove(pilot);
            }
        }

        public bool Update(int id, Pilot obj)
        {
            var flag = db.Pilots.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.Entry(obj).State = EntityState.Modified;
            return true;
        }
    }
}
