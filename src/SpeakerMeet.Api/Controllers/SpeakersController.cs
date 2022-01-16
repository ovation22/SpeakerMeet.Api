using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly ISpeakerPresentationService _speakerPresentationServiceService;

        public SpeakersController(
            ISpeakerService speakerService,
            ILoggerAdapter<SpeakersController> logger,
            ISpeakerPresentationService speakerPresentationServiceService
        )
        {
            _logger = logger;
            _speakerService = speakerService;
            _speakerPresentationServiceService = speakerPresentationServiceService;
        }

        // GET: api/Speakers
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpeakersResult>> GetAll(int pageIndex = 0, int itemsPage = 100, string? direction = null)
        {
            try
            {
                var result = await _speakerService.GetAll(pageIndex, itemsPage, direction);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Speakers");
        }

        // GET: api/Speakers/5
        [HttpGet("{id:Guid}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpeakerResult>> Get(Guid id)
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

        // GET: api/Speakers/slug-name
        [HttpGet("{slug}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpeakerResult>> GetBySlug(string slug)
        {
            try
            {
                var result = await _speakerService.Get(slug);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Speaker");
        }

        // GET: api/Speakers/5/Presentations
        [HttpGet("{id}/Presentations")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpeakerPresentationsResult>>> GetPresentations(Guid id)
        {
            try
            {
                var result = await _speakerPresentationServiceService.GetAll(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Speaker Presentations");
        }

        // GET: api/Speakers/Featured
        [HttpGet("Featured")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpeakerFeatured>>> GetFeatured()
        {
            try
            {
                var result = await _speakerService.GetFeatured();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Speakers");
        }

        // POST: api/Speakers
        [HttpPost]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // PUT: api/Speakers/5
        [HttpPut("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Put(Guid id, [FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        // DELETE: api/Speakers/5
        [HttpDelete("{id:Guid}")]
        [ProducesDefaultResponseType]
        public IActionResult Delete(Guid id)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }
    }
}
