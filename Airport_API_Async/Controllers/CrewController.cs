using System.Net;
using System.Threading.Tasks;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class CrewController : Controller
    {
        private ICrewService _service;
        public CrewController(ICrewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/Crew/:id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POSt api/Crew
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CrewDTO crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(crew);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Crew
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]CrewDTO crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id, crew);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Crew
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
             var result = await _service.DeleteObjectAsync(id);
             return result == true ? StatusCode(200) : StatusCode(500);
        }

        [HttpPost("crewload")]
        public async Task<IActionResult> LoadCrew()
        {
            var result = await _service.LoadDataAsync().ConfigureAwait(true);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}