using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class DepartureController : Controller
    {
        private readonly IDepartureService _service;
        public DepartureController(IDepartureService service)
        {
            _service = service;
        }
        // GET api/departure
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetCollection());
        }

        // GET api/departure/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        // POST api/departure
        [HttpPost]
        public IActionResult Post([FromBody]DeparturesDTO departure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(departure);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/departure/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DeparturesDTO departure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id, departure);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/departure/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveObject(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
