using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeAPI.BLL;
using ExchangeAPI.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery] List<string> Dates, string Base, string Target)
        {
            foreach(string date in Dates)
            {
                DateTime dateTime;
                if (!DateTime.TryParse(date, out dateTime)) { return BadRequest($"Date paremeters are incorrect!"); }
            }
            
            if(string.IsNullOrEmpty(Base)) { return BadRequest($"Base currency is not set!"); }
            if (string.IsNullOrEmpty(Target)) { return BadRequest($"Target currency is not set!"); }

            var result = await StatisticsManager.Instance.GetStatistics(Dates, Base, Target);
            return Ok(result);
        }
    }
}