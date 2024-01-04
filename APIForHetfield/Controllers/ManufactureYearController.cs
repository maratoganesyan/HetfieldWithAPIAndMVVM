using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufactureYearController : Controller
    {
        private ILogger<ManufactureYearController> _logger;

        public ManufactureYearController(ILogger<ManufactureYearController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ManufactureYear>> Get()
        {
            try
            {
                var manufactureYears = await Task.Run(() => DbUtils.db.ManufactureYears.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return manufactureYears;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ManufactureYear manufactureYear)
        {
            try
            {
                await Task.Run(() => DbUtils.db.ManufactureYears.Add(manufactureYear));
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
    }
}
