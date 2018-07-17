using System;
using System.Collections.Generic;
using Airport_REST_API.DataAccess;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using AutoMapper;

namespace Airport_REST_API.Services.Service
{
    public class AircraftTypeService : IAircraftTypeService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public AircraftTypeService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }
        public IEnumerable<AircraftTypeDTO> GetCollection()
        {
            return _mapper.Map<List<AircraftTypeDTO>>(db.Types.GetAll());
        }

        public AircraftTypeDTO GetObject(int id)
        {
            return _mapper.Map<AircraftTypeDTO>(db.Types.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var type = db.Types.Get(id);
            if (type != null)
            {
                db.Types.Remove(type);
                db.Save();
                return true;
            }
            return false;
        }

        public bool Add(AircraftTypeDTO obj)
        {
            if (obj != null)
            {
                db.Types.Add(_mapper.Map<AircraftType>(obj));
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

        public bool Update(int id, AircraftTypeDTO obj)
        {
            var result = db.Types.UpdateObject(id, _mapper.Map<AircraftType>(obj));
            db.Save();
            return result;
        }
    }
}
