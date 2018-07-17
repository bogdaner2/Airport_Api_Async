using System;
using System.Collections.Generic;
using System.Text;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.DataAccess.Repositories;

namespace Airport_REST_API.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private AirportContext db;
        private IRepository<Ticket> _ticketRepository;
        private IRepository<Aircraft> _aircraftRepository;
        private IRepository<AircraftType> _typeRepository;
        private IRepository<Crew> _crewRepository;
        private IRepository<Stewardess> _stewardessRepository;
        private IRepository<Pilot> _pilotRepository;
        private IRepository<Departures> _departureRepository;
        private IRepository<Flight> _flightRepository;

        public UnitOfWork(AirportContext context)
        {
            db = context;
        }

        public IRepository<Ticket> Tickets
        {
            get
            {
                if (_ticketRepository == null)
                    _ticketRepository = new TicketRepository(db);
                return _ticketRepository;
            }
        }
        public IRepository<Aircraft> Aircrafts
        {
            get
            {
                if (_aircraftRepository == null)
                    _aircraftRepository = new AircraftRepository(db);
                return _aircraftRepository;
            }
        }
        public IRepository<AircraftType> Types
        {
            get
            {
                if (_typeRepository == null)
                    _typeRepository = new AircraftTypeRepository(db);
                return _typeRepository;
            }
        }
        public IRepository<Crew> Crews
        {
            get
            {
                if (_crewRepository == null)
                    _crewRepository = new CrewRepository(db);
                return _crewRepository;
            }
        }
        public IRepository<Stewardess> Stewardess
        {
            get
            {
                if (_stewardessRepository == null)
                    _stewardessRepository = new StewardessRepository(db);
                return _stewardessRepository;
            }
        }
        public IRepository<Pilot> Pilots
        {
            get
            {
                if (_pilotRepository == null)
                    _pilotRepository = new PilotRepository(db);
                return _pilotRepository;
            }
        }
        public IRepository<Flight> Flights
        {
            get
            {
                if (_flightRepository == null)
                    _flightRepository = new FlightRepository(db);
                return _flightRepository;
            }
        }
        public IRepository<Departures> Departures
        {
            get
            {
                if (_departureRepository == null)
                    _departureRepository = new DepartureRepository(db);
                return _departureRepository;
            }
        }

        public void Save() => db.SaveChanges();
    }

}
