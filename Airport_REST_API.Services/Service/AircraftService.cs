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
    public class AircraftService : IAircraftService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public AircraftService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public IEnumerable<AircraftDTO> GetCollection()
        {
            return _mapper.Map<List<AircraftDTO>>(db.Aircrafts.GetAll().ToList());
        }

        public AircraftDTO GetObject(int id)
        {
            return _mapper.Map<AircraftDTO>(db.Aircrafts.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var aircraft = db.Aircrafts.Get(id);
            if (aircraft == null)
                return false;
            db.Aircrafts.Remove(aircraft);
            db.Save();
            return true;
        }

        public bool Add(AircraftDTO obj)
        {
            var type = db.Types.Get(obj.TypeId);
            if (type == null)
                return false;
            var aircraft = _mapper.Map<Aircraft>(obj);
            aircraft.Type = type;
            db.Aircrafts.Add(aircraft);
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

        public bool Update(int id, AircraftDTO obj)
        {
            var type = db.Types.Get(obj.TypeId);
            if (type == null)
                return false;
            var aircraft = _mapper.Map<Aircraft>(obj);
            aircraft.Type = type;
            db.Save();
            return db.Aircrafts.UpdateObject(id,aircraft);
        }
    }
}
