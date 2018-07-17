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
    public class StewardessService : IStewardessService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public StewardessService(IUnitOfWork uof, IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public IEnumerable<StewardessDTO> GetCollection()
        {
            return _mapper.Map<List<StewardessDTO>>(db.Stewardess.GetAll());
        }

        public StewardessDTO GetObject(int id)
        {
            return _mapper.Map<StewardessDTO>(db.Stewardess.Get(id));
        }

        public bool RemoveObject(int id)
        {
            var stewardess = db.Stewardess.Get(id);
            if (stewardess != null)
            {
                db.Stewardess.Remove(stewardess);
                db.Save();
                return true;
            }
            return false;
        }

        public bool Add(StewardessDTO obj)
        {
            if (obj != null)
            {
                db.Stewardess.Add(_mapper.Map<Stewardess>(obj));
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

        public bool Update(int id, StewardessDTO obj)
        {
            var result = db.Stewardess.UpdateObject(id, _mapper.Map<Stewardess>(obj));
            db.Save();
            return result;
        }
    }
}
