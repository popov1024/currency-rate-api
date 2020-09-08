namespace CurrencyRateAgregator.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class RateAgregatorController : ControllerBase
    {
        private readonly ILogger<RateAgregatorController> _logger;

        public RateAgregatorController(ILogger<RateAgregatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RateExtrator>> GetAsync()
        {
            return null;
        }
    }
}
