namespace CurrencyRateAgregator.Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;

    public interface IRateExtrator
    {
        Dictionary<Country, IEnumerable<CurrencyRate>> GetRatesByCounries();
    }
}