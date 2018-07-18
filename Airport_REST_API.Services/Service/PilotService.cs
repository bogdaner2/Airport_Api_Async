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
    public class PilotService : IPilotService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public PilotService(IUnitOfWork uof,IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PilotDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<PilotDTO>>(await db.Pilots.GetAllAsync());
        }

        public async Task<PilotDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<PilotDTO>(await db.Pilots.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false;
            await db.Pilots.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }

        public async Task<bool> CreateObjectAsync(PilotDTO obj)
        {
            if (obj != null)
            {
                await db.Pilots.CreateAsync(_mapper.Map<Pilot>(obj));
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

        public async Task<bool> UpdateObjectAsync(int id, PilotDTO obj)
        {
            var result = db.Pilots.Update(id, _mapper.Map<Pilot>(obj));
            await db.SaveAsync();
            return result;
        }
    }
}
