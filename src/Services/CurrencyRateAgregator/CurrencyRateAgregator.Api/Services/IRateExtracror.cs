namespace CurrencyRateAgregator.Api.Services
{
    using System.Collections.Generic;
    using CurrencyRateAgregator.Api.Models;

    public interface IRateExtrator
    {
        Dictionary<Country, IEnumerable<CurrencyRate>> GetRatesByCounries();
    }
}