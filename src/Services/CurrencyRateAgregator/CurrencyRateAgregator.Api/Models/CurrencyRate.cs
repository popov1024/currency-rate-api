using System;

namespace CurrencyRateAgregator.Api.Models
{
    public class CurrencyRate
    {
        public DateTime Date { get; set; }
        public Currency Currency { get; set; }
        public uint Scale { get; set; }
        public decimal Rate { get; set; }
    }
}