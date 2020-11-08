namespace CurrencyRateAgregator.Api.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;
    using Autofac.Features.Indexed;
    using Microsoft.Extensions.Caching.Memory;

    public class RateExtrator : IRateExtrator
    {
        private readonly IIndex<Country, IProvider> _provider;
        private readonly IMemoryCache _cache;
        
        public RateExtrator(IIndex<Country, IProvider> provider, IMemoryCache cache)
        {
            _provider = provider;
            _cache = cache;
        }

        public Dictionary<Country, IEnumerable<CurrencyRate>> GetRatesByCounries()
        {
            var rates = new Dictionary<Country, IEnumerable<CurrencyRate>>();

            var countries = Enum.GetValues(typeof(Country))
                .Cast<Country>()
                .ToList();

            Parallel.ForEach(countries, country =>
            {
                rates.Add(country, CountryRate(country));
            });


            return rates;
        }

        private IEnumerable<CurrencyRate> CountryRate(Country country)
        {
            IEnumerable<CurrencyRate> cacheEntry;

            if (!_cache.TryGetValue(country, out cacheEntry))
            {
                switch (country)
                {
                    case Country.BY:
                        cacheEntry = BYRate();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(3));

                _cache.Set(country, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        private IEnumerable<CurrencyRate> BYRate()
        {
            var p = _provider[Country.BY];
            return p.GetCurrencyRates();
        }
    }
}