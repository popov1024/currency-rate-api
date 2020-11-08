namespace CurrencyRateAgregator.Api.Controllers
{
    using System.Collections.Generic;
    using CurrencyRateAgregator.Api.Models;
    using CurrencyRateAgregator.Api.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRateExtrator _rateExtrator;

        public RatesController(ILogger<RatesController> logger, IRateExtrator rateExtrator)
        {
            _logger = logger;
            _rateExtrator = rateExtrator;
        }

        [HttpGet]
        public Dictionary<Country, IEnumerable<CurrencyRate>> GetAsync()
        {
            return _rateExtrator.GetRatesByCounries();
        }
    }
}
