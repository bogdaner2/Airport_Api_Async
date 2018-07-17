using System;
using System.Collections.Generic;
using System.Linq;
using Airport_REST_API.DataAccess;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using AutoMapper;

namespace Airport_REST_API.Services.Service
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;
        public FlightService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }
        public IEnumerable<FlightDTO> GetCollection()
        {
            return _mapper.Map<List<FlightDTO>>(db.Flights.GetAll());
        }

        public FlightDTO GetObject(int id)
        {
            return _mapper.Map<FlightDTO>(db.Flights.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var flight = db.Flights.Get(id);
            if (flight == null)
                return false;
            db.Flights.Remove(flight);
            db.Save();
            return true;
        }

        public bool Add(FlightDTO obj)
        {
            var tickets = db.Tickets.GetAll().Where(i => obj.TicketsId?.Contains(i.Id) == true).ToList();
            if (tickets.Count == 0 || obj == null )
                return false; 
            var flight = _mapper.Map<Flight>(obj);
            flight.Ticket = tickets;
            db.Flights.Add(flight);
            try
            {
                db.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(int id, FlightDTO obj)
        {
            var tickets = db.Tickets.GetAll().Where(i => obj.TicketsId?.Contains(i.Id) == true).ToList();
            if (tickets.Count == 0 || obj == null)
                return false; 
            var flight = _mapper.Map<Flight>(obj);
            flight.Ticket = tickets;
            var result = db.Flights.UpdateObject(id, flight);
            db.Save();
            return result;
        }
    }
}
