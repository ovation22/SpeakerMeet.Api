using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        private readonly ILoggerAdapter<CommunitiesController> _logger;

        public CommunitiesController(
            ICommunityService communityService,
            ILoggerAdapter<CommunitiesController> logger
        )
        {
            _logger = logger;
            _communityService = communityService;
        }

        // GET: api/Communities
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _communityService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Communities");
        }

        // GET: api/Communities/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _communityService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Community");
        }

        // POST: api/Communities
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Communities/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
