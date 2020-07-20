using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using SpeakerMeet.Core.DTOs;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerService _speakerService;
        private readonly ILoggerAdapter<SpeakersController> _logger;

        public SpeakersController(
            ISpeakerService speakerService,
            ILoggerAdapter<SpeakersController> logger
        )
        {
            _logger = logger;
            _speakerService = speakerService;
        }

        // GET: api/Speakers
        [HttpGet]
        [ProducesResponseType(typeof(SpeakersResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _speakerService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Speakers");
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SpeakerResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _speakerService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Speaker");
        }

        // POST: api/Speakers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Speakers/5
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
