using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.Models
{
    public class UserResponse
    {
        public decimal MinRate { get; set; }
        public string MinRateDate { get; set; }

        public decimal MaxRate { get; set; }
        public string MaxRateDate { get; set; }

        public decimal AverageRate { get; set; }
    }
}
