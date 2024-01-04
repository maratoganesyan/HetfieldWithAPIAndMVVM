using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarTypeController : Controller
    {
        private ILogger<CarTypeController> _logger;

        public CarTypeController(ILogger<CarTypeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarType>> Get()
        {
            try
            {
                var carTypes = await Task.Run(() => DbUtils.db.CarTypes.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarType carType)
        {
            try
            {
                await Task.Run(() => DbUtils.db.CarTypes.Add(carType));
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
        public async Task<IActionResult> Put([FromBody] CarType updateCarType)
        {
            try
            {
                if (DbUtils.db.CarTypes.Any(u => u.IdCarType == updateCarType.IdCarType))
                {
                    var carType = DbUtils.db.CarTypes.First(u => u.IdCarType == updateCarType.IdCarType);
                    carType.TypeName = updateCarType.TypeName;
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
