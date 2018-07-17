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
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetCollection());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]AircraftTypeDTO type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(type);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/aircrafttype/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AircraftTypeDTO type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id,type);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/aircrafttype/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveObject(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
