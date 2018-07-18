using System.Threading.Tasks;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly IFlightService _service;
        public FlightController(IFlightService service)
        {
            _service = service;
        }
        // GET api/flight
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/flight/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POST api/flight
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FlightDTO flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(flight);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/flight/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]FlightDTO flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id,flight);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
