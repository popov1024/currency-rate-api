namespace CurrencyRateAgregator.Api.Services.BY
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using CurrencyRateAgregator.Api.Models;

    public class ProviderBY : IProvider
    {
        private const string _apiurl = "https://www.nbrb.by/api/exrates/rates?periodicity=0";
        private readonly HttpClient _client = new HttpClient();

        public IEnumerable<CurrencyRate> GetCurrencyRates()
        {
            var t = Task.Run<IEnumerable<CurrencyRate>>(async () => await GetCurrencyRatesAsync());
            return t.Result;
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync()
        {
            var streamTask = _client.GetStreamAsync(_apiurl);
            var repositories = await JsonSerializer.DeserializeAsync<List<CurrencyRateNbrb>>(await streamTask);

            var r = new List<CurrencyRate>();
            foreach (var currencyBY in repositories)
            {
                Currency currency;
                try
                {
                    currency = (Currency)Enum.Parse(
                        typeof(Currency),
                        currencyBY.Cur_Abbreviation,
                        ignoreCase: true
                    );
                }
                catch (ArgumentNullException)
                {
                    continue;
                }
                catch (ArgumentException)
                {
                    continue;
                }
                catch (OverflowException)
                {
                    continue;
                }

                r.Add(new CurrencyRate
                {
                    Currency = currency,
                    Date = currencyBY.Date,
                    Rate = currencyBY.Cur_OfficialRate,
                    Scale = currencyBY.Cur_Scale
                });
            }

            return r;
        }
    }
}