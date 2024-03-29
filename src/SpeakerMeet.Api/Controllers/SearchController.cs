﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Models.DTOs;

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
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpeakerMeetSearchResults>> GetResults(string terms)
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