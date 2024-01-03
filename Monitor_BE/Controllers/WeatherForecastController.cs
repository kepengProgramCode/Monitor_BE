using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Response;
using Monitor_BE.LogManage;
using Monitor_BE.ServerBusiness;
using Monitor_BE.ServiceBuiness;

namespace Monitor_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly UserService _userService;
        private readonly LogService _logger;

        public WeatherForecastController(UserService userService, LogService logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ResponseResult<IEnumerable<WeatherForecast>> GetAll()
        {
            //ILogs log = new Loggers(@"C:\Users\kepe1\Documents\Leaen", "Study");
            _logger.Info("gsergbthhhhhhhhhhhhhhhhhhh");
            _logger.Info("gsergbthhhhhhhhhhhhhhhhhhh", new Exception("dssssssssssssssvrgerg"));

            _logger.Fatal("fdsbvb,vakjehguherhguyerghorge");
            _logger.Fatal("fdsbvb,vakjehguherhguyerghorge", new Exception("fdhgsdgerigkljvlejr"));


            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
            return data.ToList();
        }
    }
}