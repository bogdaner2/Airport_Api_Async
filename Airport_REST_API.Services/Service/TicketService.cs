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
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<TicketDTO>>(await db.Tickets.GetAllAsync());
        }

        public async Task<TicketDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<TicketDTO>(await db.Tickets.GetAsync(id)); ;
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false;
            await db.Tickets.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }

        public async Task<bool> CreateObjectAsync(TicketDTO ticket)
        {
            if (ticket != null)
            {
                await db.Tickets.CreateAsync(_mapper.Map<Ticket>(ticket));
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
            return false;
        }

        public async Task<bool> UpdateObjectAsync(int id,TicketDTO obj)
        {
            var result = db.Tickets.Update(id, _mapper.Map<Ticket>(obj));
            await db.SaveAsync();
            return result;
        }
    }
}
