using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Flight> GetAll()
        {            
            return db.Flights.Include(f => f.Ticket);
        }

        public Flight Get(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id == id);
        }

        public void Add(Flight entity)
        {
            db.Flights.Add(entity);
        }

        public void Remove(Flight entity)
        {
            db.Flights.Remove(entity);
        }

        public bool UpdateObject(int id, Flight obj)
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
