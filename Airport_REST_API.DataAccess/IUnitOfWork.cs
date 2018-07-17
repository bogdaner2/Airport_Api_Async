using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.DataAccess.Repositories;

namespace Airport_REST_API.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Ticket> Tickets { get;}
        IRepository<Aircraft> Aircrafts { get; }
        IRepository<AircraftType> Types { get; }
        IRepository<Crew> Crews { get; }
        IRepository<Stewardess> Stewardess { get; }
        IRepository<Pilot> Pilots { get; }
        IRepository<Flight> Flights { get; }
        IRepository<Departures> Departures { get;}
        void Save();
    }
}
