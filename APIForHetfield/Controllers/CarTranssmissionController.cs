using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarTranssmissionController : Controller
    {
        private ILogger<CarTranssmissionController> _logger;

        public CarTranssmissionController(ILogger<CarTranssmissionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarTranssmission>> Get()
        {
            try
            {
                var carTranssmission = await Task.Run(() => DbUtils.db.CarTranssmissions.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carTranssmission;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarTranssmission carTranssmission)
        {
            try
            {
                await Task.Run(() => DbUtils.db.CarTranssmissions.Add(carTranssmission));
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
        public async Task<IActionResult> Put([FromBody] CarTranssmission updateCarTranssmission)
        {
            try
            {
                if (DbUtils.db.CarTranssmissions.Any(u => u.IdTranssmission == updateCarTranssmission.IdTranssmission))
                {
                    var carTranssmission = DbUtils.db.CarTranssmissions.First(u => u.IdTranssmission == updateCarTranssmission.IdTranssmission);
                    carTranssmission.TranssmissionName = updateCarTranssmission.TranssmissionName;
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
