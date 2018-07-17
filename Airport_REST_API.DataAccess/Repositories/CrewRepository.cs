using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Crew>> GetAllAsync()
        {
            return await db.Crews.Include(c => c.Stewardesses).Include(c => c.Pilot).ToListAsync();
        }

        public async Task<Crew> GetAsync(int id)
        {
            return await db.Crews.Include(c => c.Stewardesses).Include(c => c.Pilot).FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateAsync(Crew entity)
        {
            await db.Crews.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var crew = await db.Crews.FindAsync(id).ConfigureAwait(false);
            if (crew != null)
            {
                db.Crews.Remove(crew);
            }
        }

        public bool Update(int id, Crew obj)
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
