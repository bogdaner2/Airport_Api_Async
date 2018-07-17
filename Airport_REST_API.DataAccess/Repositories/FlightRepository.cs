using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private AirportContext db;

        public FlightRepository(AirportContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Flight>> GetAllAsync()
        {            
            return await db.Flights.Include(f => f.Ticket).ToListAsync();
        }

        public async Task<Flight> GetAsync(int id)
        {
            return await db.Flights.Include(f => f.Ticket).FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateAsync(Flight entity)
        {
            await db.Flights.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var flight = await db.Flights.FindAsync(id).ConfigureAwait(false);
            if (flight != null)
            {
                db.Flights.Remove(flight);
            }
        }

        public bool Update(int id, Flight obj)
        {
            var flag = db.Flights.Count(item => item.Id == id) == 0;
            if (flag)
                return false;
            obj.Id = id;
            db.Flights.Update(obj);
            return true;
        }
    }
}
