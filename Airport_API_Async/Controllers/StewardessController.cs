using System.Threading.Tasks;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class StewardessController : Controller
    {
        private readonly IStewardessService _service;
        public StewardessController(IStewardessService service)
        {
            _service = service;
        }
        // GET api/Stewardess
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/Stewardess/:id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POSt api/Stewardess
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StewardessDTO stewardess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(stewardess);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Stewardess
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody]StewardessDTO stewardess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id,stewardess);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/Stewardess
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}