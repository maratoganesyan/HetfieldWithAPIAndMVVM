using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarStatusController : Controller
    {
        private ILogger<CarStatusController> _logger;

        public CarStatusController(ILogger<CarStatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarStatus>> Get()
        {
            try
            {
                var carStatus = await Task.Run(() => DbUtils.db.CarStatuses.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarStatus carStatus)
        {
            try
            {
                await Task.Run(() => DbUtils.db.CarStatuses.Add(carStatus));
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
        public async Task<IActionResult> Put([FromBody] CarStatus updateCarStatus)
        {
            try
            {
                if (DbUtils.db.CarStatuses.Any(u => u.IdCarStatus == updateCarStatus.IdCarStatus))
                {
                    var carStatus = DbUtils.db.CarStatuses.First(u => u.IdCarStatus == updateCarStatus.IdCarStatus);
                    carStatus.CarStatusName = updateCarStatus.CarStatusName;
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
