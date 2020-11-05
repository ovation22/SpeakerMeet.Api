using System;
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
    public class StatisticsController : ControllerBase
    {
        // GET: api/Search
        private readonly IStatisticsService _statisticsService;
        private readonly ILoggerAdapter<StatisticsController> _logger;

        public StatisticsController(
            IStatisticsService statisticsService,
            ILoggerAdapter<StatisticsController> logger
        )
        {
            _logger = logger;
            _statisticsService = statisticsService;
        }

        // GET: api/Statistics/Counts
        [HttpGet("Counts")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountResult>> GetCounts()
        {
            try
            {
                var result = await _statisticsService.GetCounts();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Counts");
        }
    }
}