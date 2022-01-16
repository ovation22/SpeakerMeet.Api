using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        [ProducesDefaultResponseType]
        public IActionResult GetAll()
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // GET: api/Users/5
        [HttpGet("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Get(Guid id)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // POST: api/Users
        [HttpPost]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // PUT: api/Users/5
        [HttpPut("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Put(Guid id, [FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Delete(Guid id)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }
    }
}
