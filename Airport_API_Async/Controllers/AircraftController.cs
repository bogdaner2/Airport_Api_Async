using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class AircraftController : Controller
    {
        private readonly IAircraftService _service;
        public AircraftController(IAircraftService service)
        {
            _service = service;
        }
        // GET api/aircraft
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetCollection());
        }

        // GET api/aircraft/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        //  POST api/aircraft/5
        [HttpPost]
        public IActionResult Post([FromBody]AircraftDTO aircraft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(aircraft);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/aircraft/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AircraftDTO aircraft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id,aircraft);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/aircraft/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveObject(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
