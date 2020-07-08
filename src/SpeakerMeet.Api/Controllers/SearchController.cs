using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // GET: api/Conferences
        [HttpGet]
        public IEnumerable<string> GetAll([FromQuery] string term)
        {
            return new string[] { "value1", "value2" };
        }
    }
}