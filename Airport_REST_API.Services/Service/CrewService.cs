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
    public class CrewService : ICrewService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public CrewService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public IEnumerable<CrewDTO> GetCollection()
        {
            return _mapper.Map<List<CrewDTO>>(db.Crews.GetAll());
        }

        public CrewDTO GetObject(int id)
        {
            return _mapper.Map<CrewDTO>(db.Crews.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var crew = db.Crews.Get(id);
            if (crew != null)
            {
                db.Crews.Remove(crew);
                db.Save();
                return true;
            }
            return false;
        }

        public bool Add(CrewDTO obj)
        {
            var stewardesses = db.Stewardess.GetAll()
                .Where(i => obj.StewardessesId?.Contains(i.Id) == true).ToList();
            var pilot = db.Pilots.Get(obj.PilotId.Value);
            if (stewardesses.Count == 0 || pilot == null)
                return false; 
            var crew = _mapper.Map<Crew>(obj);
            crew.Pilot = pilot;
            crew.Stewardesses = stewardesses;
            db.Crews.Add(crew);
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

        public bool Update(int id, CrewDTO obj)
        {
            var stewardesses = db.Stewardess.GetAll()
                .Where(i => obj.StewardessesId?.Contains(i.Id) == true).ToList();
            var pilot = db.Pilots.Get(obj.PilotId.Value);
            if (stewardesses.Count == 0 || pilot == null)
                return false; 
            var crew = _mapper.Map<Crew>(obj);
            crew.Pilot = pilot;
            crew.Stewardesses = stewardesses;
            var result = db.Crews.UpdateObject(id, crew);
            db.Save();
            return result;
        }
    }
}
