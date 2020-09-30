namespace CurrencyRateAgregator.Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;

    public interface IProvider
    {
        Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync();
        IEnumerable<CurrencyRate> GetCurrencyRates();
    }
}