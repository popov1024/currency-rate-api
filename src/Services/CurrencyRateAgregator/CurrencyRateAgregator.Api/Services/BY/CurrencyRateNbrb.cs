using System;

namespace CurrencyRateAgregator.Api.Services.BY
{
    public class CurrencyRateNbrb
    {
        public int Cur_ID { get; set; }
        public DateTime Date { get; set; }
        public string Cur_Abbreviation { get; set; }
        public uint Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public decimal Cur_OfficialRate { get; set; }
    }
}