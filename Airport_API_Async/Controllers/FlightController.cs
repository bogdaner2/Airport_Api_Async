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
        public IActionResult Get()
        {
            return Ok(_service.GetCollection());
        }

        // GET api/flight/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        // POST api/flight
        [HttpPost]
        public IActionResult Post([FromBody]FlightDTO flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(flight);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/flight/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]FlightDTO flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id,flight);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/flight/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveObject(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
