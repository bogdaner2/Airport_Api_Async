using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<AircraftTypeDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<AircraftTypeDTO>>(await db.Types.GetAllAsync());
        }

        public async Task<AircraftTypeDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<AircraftTypeDTO>(await db.Types.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id < 1)
                return false;
            await db.Types.DeleteAsync(id);
            await db.SaveAsync();
            return true;
        }

        public async Task<bool> CreateObjectAsync(AircraftTypeDTO obj)
        {
            if (obj != null)
            {
                await db.Types.CreateAsync(_mapper.Map<AircraftType>(obj));
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

        public async Task<bool> UpdateObjectAsync(int id, AircraftTypeDTO obj)
        {
            var result = db.Types.Update(id, _mapper.Map<AircraftType>(obj));
            await db.SaveAsync();
            return result;
        }
    }
}
