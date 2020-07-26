using System;
using System.Collections.Generic;
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
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILoggerAdapter<SearchController> _logger;

        public SearchController(
            ISearchService searchService,
            ILoggerAdapter<SearchController> logger
        )
        {
            _logger = logger;
            _searchService = searchService;
        }
        
        // GET: api/Search
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SearchResults>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetResults(string terms)
        {
            try
            {
                var result = await _searchService.Search(terms);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return results");
        }
    }
}