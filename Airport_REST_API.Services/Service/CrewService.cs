using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using AutoMapper;
using Newtonsoft.Json;

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

        public async Task<IEnumerable<CrewDTO>> GetCollectionAsync()
        {
            return _mapper.Map<List<CrewDTO>>(await db.Crews.GetAllAsync());
        }

        public async Task<CrewDTO> GetObjectAsync(int id)
        {
            return _mapper.Map<CrewDTO>(await db.Crews.GetAsync(id));
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {

            if (id < 0)
            {
                await db.Crews.DeleteAsync(id);
                await db.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CreateObjectAsync(CrewDTO obj)
        {
            var stewardesses = db.Stewardess.GetAllAsync().Result
                .Where(i => obj.StewardessesId?.Contains(i.Id) == true).ToList();
            var pilot = await db.Pilots.GetAsync(obj.PilotId.Value);
            if (stewardesses.Count == 0 || pilot == null)
                return false; 
            var crew = _mapper.Map<Crew>(obj);
            crew.Pilot = pilot;
            crew.Stewardesses = stewardesses;
            await db.Crews.CreateAsync(crew);
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

        public async Task<bool> UpdateObjectAsync(int id, CrewDTO obj)
        {
            var stewardesses = db.Stewardess.GetAllAsync().Result
                .Where(i => obj.StewardessesId?.Contains(i.Id) == true).ToList();
            var pilot = await db.Pilots.GetAsync(obj.PilotId.Value);
            if (stewardesses.Count == 0 || pilot == null)
                return false; 
            var crew = _mapper.Map<Crew>(obj);
            crew.Pilot = pilot;
            crew.Stewardesses = stewardesses;
            var result = db.Crews.Update(id, crew);
            await db.SaveAsync();
            return result;
        }

        public async Task<List<Crew>> LoadDataAsync()
        {
            List<Crew> crews;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew"))
            using (HttpContent content = response.Content)
            {
                string responsJson = await content.ReadAsStringAsync();
                crews = JsonConvert.DeserializeObject<List<Crew>>(responsJson);
            }

            var items = crews.Take(10).ToList();
            return items;
        }
    }
}
