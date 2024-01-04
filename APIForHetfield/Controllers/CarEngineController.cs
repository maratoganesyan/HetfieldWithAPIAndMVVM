using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarEngineController : Controller
    {

        private ILogger<CarEngineController> _logger;

        public CarEngineController(ILogger<CarEngineController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarEngine>> Get()
        {
            try
            {
                var carEngine = await Task.Run(() => DbUtils.db.CarEngines.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carEngine;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarEngine carEngine)
        {
            try
            {
                await Task.Run(() => DbUtils.db.CarEngines.Add(carEngine));
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
        public async Task<IActionResult> Put([FromBody] CarEngine updateCarEngine)
        {
            try
            {
                if (DbUtils.db.CarEngines.Any(u => u.IdCarEngine == updateCarEngine.IdCarEngine))
                {
                    var carEngine = DbUtils.db.CarEngines.First(u => u.IdCarEngine == updateCarEngine.IdCarEngine);
                    carEngine.EngineName = updateCarEngine.EngineName;
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
