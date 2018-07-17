using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess.Repositories
{
    public class TicketRepository : IRepository<Ticket>
    {
        private AirportContext db;

        public TicketRepository(AirportContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await db.Tickets.ToListAsync();
        }
        public async Task<Ticket> GetAsync(int id)
        {
            return await db.Tickets.FirstOrDefaultAsync(item => item.Id == id);
        }
        public async Task CreateAsync(Ticket ticket)
        {
            await db.Tickets.AddAsync(ticket);
        }
        public async Task DeleteAsync(int id)
        {
            var ticket = await db.Tickets.FindAsync(id).ConfigureAwait(false);
            if (ticket != null)
            {
                db.Tickets.Remove(ticket);
            }
        }
        public bool Update(int id, Ticket obj)
        {
            var flag =  db.Tickets.Count(item => item.Id == id) == 0;
            if (flag)
                return false;            
            var temp = GetAsync(id).Result;
            temp.Number = obj.Number;
            temp.Price = obj.Price;
            return true;
        }
    }
}
