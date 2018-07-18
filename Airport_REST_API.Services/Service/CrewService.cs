using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            var stewardesses = (await db.Stewardess.GetAllAsync())
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
            var stewardesses = (await db.Stewardess.GetAllAsync())
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

        public async Task<bool> LoadDataAsync()
        {
            List<LoadCrewDTO> crews;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew"))
            using (HttpContent content = response.Content)
            {
                string responsJson = await content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
                crews = JsonConvert.DeserializeObject<List<LoadCrewDTO>>(responsJson);
            }
            var items = crews.Take(10).ToList();
            var fileName = $"log_{DateTime.Now.ToString("yy-MM-dd__H-mm")}.csv";
            await Task.WhenAll(LoadToDataBase(items),WriteToCSV(items, Path.Combine(Environment.CurrentDirectory, @"Logs\", fileName)));
            return true;
        }
       
        private async Task WriteToCSV(List<LoadCrewDTO> list,string path)
        {
            using (var w = new StreamWriter(path))
            {
                await w.WriteLineAsync("id,pilot,stewardess");
                foreach (var row in list)
                {
                    var id = row.id;
                    var pilot = JsonConvert.SerializeObject(row.pilot.FirstOrDefault());
                    var stewardesses = JsonConvert.SerializeObject(row.stewardess);
                    var line = string.Format("{0},\"{1}\",\"{2}\"", id,pilot, stewardesses);
                    await w.WriteLineAsync(line);
                    w.Flush();
                }
            }
        }
        private async Task LoadToDataBase(List<LoadCrewDTO> input)
        {
            var stewardess = input.SelectMany(i => i.stewardess);
            var pilot = input.SelectMany(i => i.pilot);
            stewardess.ToList().ForEach(async x =>
            {
                x.Id = 0;
                await db.Stewardess.CreateAsync(x);
            });
            pilot.ToList().ForEach(async x =>
            {
                x.Id = 0;
                await db.Pilots.CreateAsync(x);
            });
            input.ToList().ForEach(async x =>
            {
                x.id = 0;
                await db.Crews.CreateAsync(_mapper.Map<Crew>(x));
            });
            await db.SaveAsync();
        }
    }
}
