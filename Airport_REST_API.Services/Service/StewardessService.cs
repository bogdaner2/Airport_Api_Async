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
    public class StewardessService : IStewardessService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public StewardessService(IUnitOfWork uof, IMapper mapper)
        {
            db = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StewardessDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<StewardessDTO>>(await db.Stewardess.GetAllAsync());
        }

        public async Task<StewardessDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<StewardessDTO>(await db.Stewardess.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false;
            await db.Stewardess.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }
        public async Task<bool> CreateObjectAsync(StewardessDTO obj)
        {
            if (obj != null)
            {
                await db.Stewardess.CreateAsync(_mapper.Map<Stewardess>(obj));
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

        public async Task<bool> UpdateObjectAsync(int id, StewardessDTO obj)
        {
            var result = db.Stewardess.Update(id, _mapper.Map<Stewardess>(obj));
            await db.SaveAsync();
            return result;
        }
    }
}
