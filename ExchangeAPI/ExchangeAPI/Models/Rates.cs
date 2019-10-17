using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.Models
{
    public class Rates
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("base")]
        public string BaseCurrency { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, string> JsonRate { get; set; }

        public string TargetCurrency
        {
            get { return JsonRate.First().Key; }
        }
        public decimal Rate
        {
            get { return decimal.Parse(JsonRate.First().Value); }
        }
    }
}
