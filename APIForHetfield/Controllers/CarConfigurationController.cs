using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarConfigurationController : Controller
    {

        private ILogger<CarConfigurationController> _logger;

        public CarConfigurationController(ILogger<CarConfigurationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarConfiguration>> Get()
        {
            try
            {
                var carConfiguration = await Task.Run(() => DbUtils.db.CarConfigurations.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carConfiguration;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarConfiguration carConfiguration)
        {
            try
            {
                await Task.Run(() => DbUtils.db.CarConfigurations.Add(carConfiguration));
                await DbUtils.db.SaveChangesAsync();
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} POST request succes");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} POST request failed");
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CarConfiguration updateCarConfiguration)
        {
            try
            {
                if (DbUtils.db.CarConfigurations.Any(u => u.IdCarConfiguration == updateCarConfiguration.IdCarConfiguration))
                {
                    var carConfiguration = DbUtils.db.CarConfigurations.First(u => u.IdCarConfiguration == updateCarConfiguration.IdCarConfiguration);
                    carConfiguration.CarConfigurationName = updateCarConfiguration.CarConfigurationName;
                    await DbUtils.db.SaveChangesAsync();
                    _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                    return Ok();
                }
                else
                {
                    _logger.LogError($"{this.GetType().Name} Id Don't exist in DataBase");
                    throw new Exception($"{this.GetType().Name} Id Don't exist in DataBase");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} PUT request failed");
                throw ex;
            }
        }
    }
}
