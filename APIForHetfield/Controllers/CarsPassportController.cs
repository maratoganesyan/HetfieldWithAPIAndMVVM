using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsPassportController : Controller
    {
        private ILogger<CarsPassportController> _logger;

        public CarsPassportController(ILogger<CarsPassportController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarsPassport>> Get()
        {
            try
            {
                var carsPassports = await Task.Run(() => DbUtils.db.CarsPassports.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return carsPassports;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }
    }
}
