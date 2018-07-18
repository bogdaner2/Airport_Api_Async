using System.Threading.Tasks;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Airport_API_Async.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService _service;
        public TicketController(ITicketService service)
        {
            _service = service;
        }
        // GET api/ticket
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        // GET api/ticket/:id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetObjectAsync(id));
        }

        // POSt api/ticket
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TicketDTO ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateObjectAsync(ticket);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/ticket
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody]TicketDTO ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateObjectAsync(id,ticket);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // Delete api/ticket
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteObjectAsync(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}