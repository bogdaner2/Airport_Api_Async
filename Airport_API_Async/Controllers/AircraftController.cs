using System.Threading.Tasks;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/aircraft/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        //  POST api/aircraft/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AircraftDTO aircraft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(aircraft);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/aircraft/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AircraftDTO aircraft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id,aircraft);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/aircraft/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}
