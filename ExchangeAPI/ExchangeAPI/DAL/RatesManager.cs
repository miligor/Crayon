using ExchangeAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExchangeAPI.DAL
{
    public static class RatesManager
    {
        public static Rates GetRate(string Date, string Base, string Target)
        {
            Rates result = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.exchangeratesapi.io/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"{Date}?base={Base}&symbols={Target}").Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Rates>(jsonResponse);
            }

            return result;
        }
    }
}
