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

        public List<Rates> RatesList = new List<Rates>();

        public UserResponse GetStatistics(List<string> DateList, string BaseCurrency, string TargetCurrency)
        {
            UserResponse result = new UserResponse();

            List<Rates> RateList = new List<Rates>();
            foreach(string Date in DateList)
            {
                Rates rate = RatesManager.GetRate(Date, BaseCurrency, TargetCurrency);
                RateList.Add(rate);
            }

            List<Rates> OrderedRateList = RateList.OrderBy(x => x.Rate).ToList();

            result.MinRateDate = OrderedRateList[0].Date;
            result.MinRate = OrderedRateList[0].Rate;

            result.MaxRateDate = OrderedRateList[OrderedRateList.Count - 1].Date;
            result.MaxRate = OrderedRateList[OrderedRateList.Count - 1].Rate;

            result.AverageRate = Math.Round(OrderedRateList.Average(x => x.Rate), 10, MidpointRounding.ToEven);

            return result;
        }
    }
}
