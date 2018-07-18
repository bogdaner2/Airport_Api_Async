using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class StewardessRepository : IRepository<Stewardess>
    {
        private AirportContext db;
        public StewardessRepository(AirportContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Stewardess>> GetAllAsync()
        {
            return await db.Stewardesses.ToListAsync();
        }

        public async Task<Stewardess> GetAsync(int id)
        {
            return await db.Stewardesses.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateAsync(Stewardess entity)
        {
            await db.Stewardesses.AddAsync(entity);
        }
        public async Task<List<Stewardess>> GetLastRangeAsync(int n)
        {
            return await db.Stewardesses.TakeLast(n).ToListAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var stewardess = await db.Stewardesses.FindAsync(id).ConfigureAwait(false);
            if (stewardess != null)
            {
                db.Stewardesses.Remove(stewardess);
            }
        }

        public bool Update(int id, Stewardess obj)
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
