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
            var collection = await _service.GetCollection();
            return Ok(collection);
        }

        // GET api/Pilot/:id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        // POSt api/Pilot
        [HttpPost]
        public IActionResult Post([FromBody]PilotDTO Pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(Pilot);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Pilot
        [HttpPut("{id:int}")]
        public IActionResult Put(int id,[FromBody]PilotDTO Pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id,Pilot);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Pilot
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveObject(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}