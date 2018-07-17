using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_REST_API.DataAccess
{
    public class DataInitializer
    {
        private AirportContext _context;

        public DataInitializer(AirportContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Aircrafts.Any())
            {
                Aircrafts.ForEach(i => _context.Aircrafts.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.AircraftTypes.Any())
            {
                AircraftTypes.ForEach(i => _context.AircraftTypes.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.Pilots.Any())
            {
                Pilots.ForEach(i => _context.Pilots.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.Crews.Any())
            {
                Crews.ForEach(i => _context.Crews.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.Stewardesses.Any())
            {
                Stewardesses.ForEach(i => _context.Stewardesses.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.Tickets.Any())
            {
                Tickets.ForEach(i => _context.Tickets.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.Flights.Any())
            {
                Flights.ForEach(i => _context.Flights.Add(i));
                await _context.SaveChangesAsync();
            }

            if (!_context.Departures.Any())
            {
                Departureses.ForEach(i => _context.Departures.Add(i));
                await _context.SaveChangesAsync();
            }
        }
        public List<Aircraft> Aircrafts = new List<Aircraft>
        {
            new Aircraft {
                ReleseDate = new DateTime(2008,6,15),
                Name = "ARM273" , 
                Lifetime = TimeSpan.FromTicks(10000000),
                Type = new AircraftType { CarryingCapacity = 107900,CountOfSeats = 214, Model = "Туполев Ту-204"}}
        };
        public List<AircraftType> AircraftTypes = new List<AircraftType>
        {
            new AircraftType { CarryingCapacity = 47000,CountOfSeats = 96, Model = "Туполев Ту-134"}
        };
        public List<Ticket> Tickets = new List<Ticket>
        {     
            new Ticket { Number = "AH107", Price = 1250},
            new Ticket { Number = "AH108", Price = 3400},
        };
        public List<Stewardess> Stewardesses = new List<Stewardess>
        {
            new Stewardess
            {
                FirstName = "Natya" ,LastName = "Bondarenko",DateOfBirth = new DateTime(1987,3,13),
            }
        };
        public List<Pilot> Pilots = new List<Pilot>
        {
            new Pilot
            {
                FirstName = "Ivan" ,LastName = "Kotov",Experience = 10,DateOfBirth = new DateTime(1978,6,17)
            },
        };
        public List<Crew> Crews = new List<Crew>
        {
            new Crew
            {
             
              Pilot = new Pilot
              {
                  FirstName = "Oleg" ,LastName = "Nikolaev",Experience = 10,DateOfBirth = new DateTime(1978,6,17)
              },
              Stewardesses = new List<Stewardess>
              {
                  new Stewardess
                  {
                      FirstName = "Inna" ,LastName  = "Vlasova",DateOfBirth = new DateTime(1991,9,17)
                  },
                  new Stewardess
                  {
                      FirstName = "Yana" ,LastName = "Vlasova",DateOfBirth = new DateTime(1991,9,28),
                  },
              }
            }
        };
        public List<Departures> Departureses = new List<Departures>
        {
            new Departures
            {
                Number = "AH95",DepartureTime = new DateTime(2018,6,26),
                Aircraft = new Aircraft {
                    ReleseDate = new DateTime(2008,7,25),
                    Name = "Litak" ,
                    Lifetime = TimeSpan.FromTicks(20000000),
                    Type =  new AircraftType {CarryingCapacity = 72000,CountOfSeats = 158, Model = "Туполев Ту-154 "}},
                Crew =  new Crew
                {                   
                    Pilot = new Pilot
                    {
                        FirstName = "Nikita" ,LastName = "Ogurchikov",Experience = 10,DateOfBirth = new DateTime(1978,6,17)
                    },
                    Stewardesses = new List<Stewardess>
                    {
                        new Stewardess
                        {
                            FirstName = "Katya" ,LastName = "Puskina",DateOfBirth = new DateTime(1987,3,13),
                        },
                        new Stewardess
                        {
                            FirstName = "Onsana" ,LastName = "Ivanova",DateOfBirth = new DateTime(1987,3,13),
                        }
                    }
                }
            }
        };
        public List<Flight> Flights = new List<Flight>
        {
            new Flight
            {
                Number = "107",PointOfDeparture = "London",Destination = "Paris",DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddDays(1),
                Ticket = new List<Ticket>
                {
                    new Ticket { Number = "AH101", Price = 100},
                    new Ticket { Number = "AH102", Price = 150},
                    new Ticket { Number = "AH103", Price = 200},
                    new Ticket { Number = "AH104", Price = 250},
                    new Ticket { Number = "AH105", Price = 300},
                }
            }
        };
    }
}
