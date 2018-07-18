using System.Threading.Tasks;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class AircraftTypeController : Controller
    {
        private readonly IAircraftTypeService _service;
        public AircraftTypeController(IAircraftTypeService service)
        {
            _service = service;
        }
        // GET api/aircrafttype
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/aircrafttype/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POST api/aircrafttype
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AircraftTypeDTO type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(type);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/aircrafttype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AircraftTypeDTO type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id,type);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/aircrafttype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
