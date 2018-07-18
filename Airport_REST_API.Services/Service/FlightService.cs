using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<FlightDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<FlightDTO>>(await db.Flights.GetAllAsync());
        }

        public async Task<FlightDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<FlightDTO>(await db.Flights.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false;
            await db.Flights.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }

        public async Task<bool> CreateObjectAsync(FlightDTO obj)
        {
            var tickets = (await db.Tickets.GetAllAsync()).Where(i => obj.TicketsId?.Contains(i.Id) == true).ToList();
            if (tickets.Count == 0 || obj == null )
                return false; 
            var flight = _mapper.Map<Flight>(obj);
            flight.Ticket = tickets;
            await db.Flights.CreateAsync(flight);
            try
            {
                await db.SaveAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateObjectAsync(int id, FlightDTO obj)
        {
            var tickets = (await db.Tickets.GetAllAsync()).Where(i => obj.TicketsId?.Contains(i.Id) == true).ToList();
            if (tickets.Count == 0 || obj == null)
                return false; 
            var flight = _mapper.Map<Flight>(obj);
            flight.Ticket = tickets;
            var result = db.Flights.Update(id, flight);
            await db.SaveAsync();
            return result;
        }
    }
}
