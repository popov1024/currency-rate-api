namespace CurrencyRateAgregator.Api.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;

    public class RateExtrator : IRateExtrator
    {
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
            return null;
        }
    }
}