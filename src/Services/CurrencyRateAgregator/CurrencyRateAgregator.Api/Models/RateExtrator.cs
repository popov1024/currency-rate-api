namespace CurrencyRateAgregator.Api.Models
{
    using System.Collections.Generic;

    public class RateExtrator
    {
        public Dictionary<Country, IEnumerable<CurrencyRate>> CurrencyRates { get; set; }
    }
}