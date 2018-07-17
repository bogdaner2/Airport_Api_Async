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
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }
        public IEnumerable<TicketDTO> GetCollection()
        {
            return _mapper.Map<List<TicketDTO>>(db.Tickets.GetAll());
        }

        public TicketDTO GetObject(int id)
        {
            return _mapper.Map<TicketDTO>(db.Tickets.Get(id)); ;
        }

        public bool RemoveObject(int id)
        {
            var ticket = db.Tickets.Get(id);
            if (ticket != null)
            {
                db.Tickets.Remove(ticket);
                db.Save();
                return true;
            }
            return false;
        }

        public bool Add(TicketDTO ticket)
        {
            if (ticket != null)
            {
                db.Tickets.Add(_mapper.Map<Ticket>(ticket));
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
            return false;
        }

        public bool Update(int id,TicketDTO obj)
        {
            var result = db.Tickets.UpdateObject(id, _mapper.Map<Ticket>(obj));
            db.Save();
            return result;
        }
    }
}
