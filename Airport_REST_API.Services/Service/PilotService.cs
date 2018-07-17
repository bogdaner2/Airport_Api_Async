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
    public class PilotService : IPilotService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public PilotService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public IEnumerable<PilotDTO> GetCollection()
        {
            return _mapper.Map<List<PilotDTO>>(db.Pilots.GetAll());
        }

        public PilotDTO GetObject(int id)
        {
            return _mapper.Map<PilotDTO>(db.Pilots.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var pilot = db.Pilots.Get(id);
            if (pilot != null)
            {
                db.Pilots.Remove(pilot);
                db.Save();
                return true;
            }
            return false;
        }

        public bool Add(PilotDTO obj)
        {
            if (obj != null)
            {
                db.Pilots.Add(_mapper.Map<Pilot>(obj));
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

        public bool Update(int id, PilotDTO obj)
        {
            var result = db.Pilots.UpdateObject(id, _mapper.Map<Pilot>(obj));
            db.Save();
            return result;
        }
    }
}
