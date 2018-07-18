using System.Threading.Tasks;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class PilotController : Controller
    {
        private readonly IPilotService _service;
        public PilotController(IPilotService service)
        {
            _service = service;
        }
        // GET api/Pilot
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/Pilot/:id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POSt api/Pilot
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PilotDTO Pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(Pilot);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Pilot
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody]PilotDTO Pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id,Pilot);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Pilot
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}