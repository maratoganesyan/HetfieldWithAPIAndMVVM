using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarColorController : Controller
    {
        private ILogger<CarColorController> _logger;

        public CarColorController(ILogger<CarColorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarColor>> Get()
        {
            try
            {
                var carColor = await Task.Run(() => DbUtils.db.CarColors.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carColor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarColor carColor)
        {
            try
            {
                await Task.Run(() => DbUtils.db.CarColors.Add(carColor));
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
        public async Task<IActionResult> Put([FromBody] CarColor updateCarColor)
        {
            try
            {
                if (DbUtils.db.CarColors.Any(u => u.IdCarColors == updateCarColor.IdCarColors))
                {
                    CarColor carColor = DbUtils.db.CarColors.First(u => u.IdCarColors == updateCarColor.IdCarColors);
                    carColor.ColorName = updateCarColor.ColorName;
                    carColor.Hex = updateCarColor.Hex;
                    await DbUtils.db.SaveChangesAsync();
                    _logger.Log(LogLevel.Information, $"{this.GetType().Name} PUT request succes");
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
