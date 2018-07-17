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
    public class DepartureService : IDepartureService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public DepartureService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public IEnumerable<DeparturesDTO> GetCollection()
        {
            return _mapper.Map<List<DeparturesDTO>>(db.Departures.GetAll());
        }

        public DeparturesDTO GetObject(int id)
        {
            return _mapper.Map<DeparturesDTO>(db.Departures.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var departure = db.Departures.Get(id);
            if (departure == null)
                return false; 
            db.Departures.Remove(departure);
            db.Save();
            return true;
        }

        public bool Add(DeparturesDTO obj)
        {
            var crew = db.Crews.Get(obj.CrewId.Value);
            var aircraft = db.Aircrafts.Get(obj.AircraftId.Value);
            if (crew == null || aircraft == null)
                return false; 
            var departure = _mapper.Map<Departures>(obj);
            departure.Aircraft = aircraft;
            departure.Crew = crew;
            db.Departures.Add(departure);
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

        public bool Update(int id, DeparturesDTO obj)
        {
            var crew = db.Crews.Get(obj.CrewId.Value);
            var aircraft = db.Aircrafts.Get(obj.AircraftId.Value);
            if (crew == null || aircraft == null)
                return false; 
            var departure = _mapper.Map<Departures>(obj);
            departure.Aircraft = aircraft;
            departure.Crew = crew;
            var result = db.Departures.UpdateObject(id, departure);
            db.Save();
            return result;
        }
    }
}
