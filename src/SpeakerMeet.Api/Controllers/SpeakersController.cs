using System;
using System.Collections.Generic;
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
        [ProducesResponseType(typeof(IEnumerable<SpeakersResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int itemsPage = 100)
        {
            try
            {
                var result = await _speakerService.GetAll(pageIndex, itemsPage);

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

        // GET: api/Speakers/5/Presentations
        [HttpGet("{id}/Presentations")]
        [ProducesResponseType(typeof(IEnumerable<SpeakerPresentationsResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPresentations(Guid id)
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

        // GET: api/Speakers/slug/slug
        [HttpGet("Slug/{slug}")]
        [ProducesResponseType(typeof(SpeakerResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBySlug(string slug)
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

        // GET: api/Speakers/Featured
        [HttpGet("Featured")]
        [ProducesResponseType(typeof(IEnumerable<SpeakersResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFeatured()
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
