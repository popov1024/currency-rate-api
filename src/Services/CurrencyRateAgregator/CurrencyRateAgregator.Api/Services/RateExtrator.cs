namespace CurrencyRateAgregator.Api.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;
    using Autofac.Features.Indexed;

    public class RateExtrator : IRateExtrator
    {
        private readonly IIndex<Country, IProvider> _provider;
        
        public RateExtrator(IIndex<Country, IProvider> provider)
        {
            _provider = provider;
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
            switch (country)
            {
                case Country.BY:
                    return BYRate();
            }

            throw new Exception();
        }

        private IEnumerable<CurrencyRate> BYRate()
        {
            var p = _provider[Country.BY];
            return p.GetCurrencyRates();
        }
    }
}