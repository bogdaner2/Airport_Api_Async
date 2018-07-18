using System;
using System.Collections.Generic;
using System.IO;
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

            if (id < 1)
                return false;
            await db.Crews.DeleteAsync(id);
            await db.SaveAsync();
            return true;
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

        public async Task<List<CrewDTO>> LoadDataAsync()
        {
            List<LoadCrewDTO> crews;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew"))
            using (HttpContent content = response.Content)
            {
                string responsJson = await content.ReadAsStringAsync();
                crews = JsonConvert.DeserializeObject<List<LoadCrewDTO>>(responsJson);
            }

            var items = crews.Take(10).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<LoadCrewDTO, CrewDTO>()
                .ForMember(x => x.StewardessesId, opt => opt.MapFrom(i => i.stewardess.Select(s => s.Id)))
                .ForMember(x => x.PilotId, opt => opt.MapFrom(i => i.pilot.FirstOrDefault().Id)));
            Parallel.Invoke( 
                () => {WriteToCSV(items, @"C:\Users\Богдан\Desktop\Binary Studio Academy\Airport_API_Async\Crew.csv");} ,
                async () => await LoadDataAsync());
            return Mapper.Map<List<CrewDTO>>(items);
        }

        private void WriteToCSV(List<LoadCrewDTO> list,string path)
        {
            using (var w = new StreamWriter(path))
            {
                foreach (var row in list)
                {
                    var id = row.id;
                    var pilot = row.pilot;
                    var stewardesses = row.stewardess;
                    var line = string.Format("{0},\"{1}\",\"{2}\"", id,pilot, stewardesses);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }

        private async Task LoadToDataBase(List<LoadCrewDTO> input)
        {
            foreach (var crew in input)
            {
                await db.Pilots.CreateAsync(_mapper.Map<Pilot>(crew.pilot));
                foreach (var stewardess in crew.stewardess)
                {
                    await db.Stewardess.CreateAsync(_mapper.Map<Stewardess>(stewardess));
                }
            }
        }
    }
}
