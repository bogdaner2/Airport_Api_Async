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
        public IActionResult GetAll()
        {
            return Ok(_service.GetCollection());
        }

        // GET api/ticket/:id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetObject(id));
        }

        // POSt api/ticket
        [HttpPost]
        public IActionResult Post([FromBody]TicketDTO ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Add(ticket);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // PUT api/ticket
        [HttpPut("{id:int}")]
        public IActionResult Put(int id,[FromBody]TicketDTO ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _service.Update(id,ticket);
            return result == true ? StatusCode(200) : StatusCode(500);
        }

        // Delete api/ticket
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveObject(id);
            return result == true ? StatusCode(200) : StatusCode(500);
        }
    }
}