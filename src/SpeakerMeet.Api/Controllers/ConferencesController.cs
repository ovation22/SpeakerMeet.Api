using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceService _conferenceService;
        private readonly ILoggerAdapter<ConferencesController> _logger;

        public ConferencesController(
            IConferenceService conferenceService,
            ILoggerAdapter<ConferencesController> logger
        )
        {
            _logger = logger;
            _conferenceService = conferenceService;
        }

        // GET: api/Conferences
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConferencesResult>> GetAll(int pageIndex = 0, int itemsPage = 100, string? direction = null)
        {
            try
            {
                var result = await _conferenceService.GetAll(pageIndex, itemsPage, direction);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conferences");
        }

        // GET: api/Conferences/5
        [HttpGet("{id:Guid}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConferenceResult>> Get(Guid id)
        {
            try
            {
                var result = await _conferenceService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conference");
        }

        // GET: api/Conferences/slug-name
        [HttpGet("{slug}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConferenceResult>> GetBySlug(string slug)
        {
            try
            {
                var result = await _conferenceService.Get(slug);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conference");
        }

        // GET: api/Conferences/Featured
        [HttpGet("Featured")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConferenceFeatured>> GetFeatured()
        {
            try
            {
                var result = await _conferenceService.GetFeatured();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conferences");
        }

        // POST: api/Conferences
        [HttpPost]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // PUT: api/Conferences/5
        [HttpPut("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Put(Guid id, [FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // DELETE: api/Conferences/5
        [HttpDelete("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Delete(Guid id)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }
    }
}
