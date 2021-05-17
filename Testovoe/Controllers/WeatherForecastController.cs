using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe.Data;
using Testovoe.Model;

namespace Testovoe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly TestAppDbContext _testAppDbContext;
        private readonly IUserTimeRepository _userTimeRepository;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TestAppDbContext testAppDbContext, IUserTimeRepository userTimeRepository)
        {
            _logger = logger;
            _testAppDbContext = testAppDbContext;
            _userTimeRepository = userTimeRepository;
        }

        [HttpGet]
        public IEnumerable<UserTime> Get()
        {
            return _testAppDbContext.UserTimes.AsEnumerable();
        }

        [HttpGet]
        [Route("[action]")]
        public string Clear()
        {
            _testAppDbContext.UserTimes.RemoveRange(_testAppDbContext.UserTimes.Select(x => x).AsEnumerable());
            _testAppDbContext.SaveChanges();
            return "Ok";
        }

        [HttpPost]
        public ActionResult Post(IEnumerable<UserTime> userTimes)
        {
            _userTimeRepository.AddUserTimes(userTimes);
            _testAppDbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public RollingRetentionResponseDTO RollingRetention (RollingRetentionRequestDTO dto)
        {
            var result = new RollingRetentionResponseDTO
            {
                RollingRetention = _userTimeRepository.GetRollingRetention(dto.Days),
                HistogramData = _userTimeRepository.GetHistogrammData(),
            };
            return result;
        }
    }


}
