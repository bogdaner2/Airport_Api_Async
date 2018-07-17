using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess
{
    public class AirportContext : DbContext
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Aircraft> Aircrafts { get; set; }
        public virtual DbSet<AircraftType> AircraftTypes { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<Stewardess> Stewardesses { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Departures> Departures { get; set; }

        public AirportContext(DbContextOptions<AirportContext> options) : base (options) { }
        public AirportContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    }
}
