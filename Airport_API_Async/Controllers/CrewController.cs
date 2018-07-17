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
        public IActionResult GetAll()
        {
            return Ok(_service.GetCollection());
        }

        // GET api/Crew/:id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        // POSt api/Crew
        [HttpPost]
        public IActionResult Post([FromBody]CrewDTO crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(crew);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Crew
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody]CrewDTO crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id, crew);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Crew
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
             var result = _service.RemoveObject(id);
             return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}