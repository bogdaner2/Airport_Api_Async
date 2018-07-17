using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess
{
    public class AirportContext : DbContext
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<AircraftType> AircraftTypes { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Stewardess> Stewardesses { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Departures> Departures { get; set; }

        public AirportContext(DbContextOptions<AirportContext> options) : base (options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }

        public AirportContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}
