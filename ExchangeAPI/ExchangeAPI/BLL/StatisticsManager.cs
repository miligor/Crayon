using ExchangeAPI.DAL;
using ExchangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.BLL
{
    public sealed class StatisticsManager
    {
        private static StatisticsManager _Instance = null;
        public static StatisticsManager Instance
        {
            get
            {
                if(_Instance == null) { _Instance = new StatisticsManager(); }
                return _Instance;
            }
        }
        private StatisticsManager() {  }

        public async Task<UserResponse> GetStatistics(List<string> DateList, string BaseCurrency, string TargetCurrency)
        {
            UserResponse result = new UserResponse();

            //List<Rates> RateList = new List<Rates>();
            //foreach(string Date in DateList)
            //{
            //    Rates rate = RatesManager.GetRate(Date, BaseCurrency, TargetCurrency);
            //    RateList.Add(rate);
            //}

            var RateList = await GetRates(DateList, BaseCurrency, TargetCurrency);   
            List<Rates> OrderedRateList = RateList.OrderBy(x => x.Rate).ToList();

            result.MinRateDate = OrderedRateList[0].Date;
            result.MinRate = OrderedRateList[0].Rate;

            result.MaxRateDate = OrderedRateList[OrderedRateList.Count - 1].Date;
            result.MaxRate = OrderedRateList[OrderedRateList.Count - 1].Rate;

            result.AverageRate = Math.Round(OrderedRateList.Average(x => x.Rate), 10, MidpointRounding.ToEven);

            return result;
        }

        private async Task<List<Rates>> GetRates(List<string> DateList, string BaseCurrency, string TargetCurrency)
        {
            var Tasks = DateList.Select(x => RatesManager.GetRate(x, BaseCurrency, TargetCurrency));
            var Result = await Task.WhenAll(Tasks);

            return Result.ToList();
        }
    }
}
