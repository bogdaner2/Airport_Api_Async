using System.Threading.Tasks;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/departure/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POST api/departure
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DeparturesDTO departure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(departure);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/departure/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DeparturesDTO departure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id, departure);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/departure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await  _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
