using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenderController : Controller
    {

        private ILogger<GenderController> _logger;

        public GenderController(ILogger<GenderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Gender>> Get()
        {
            try
            {
                var genders = await Task.Run(() => DbUtils.db.Genders.AsEnumerable());
                _logger.Log(LogLevel.Information, "GendersController Get request succes");
                return genders;
            }
            catch (Exception ex)
            {
                _logger.LogError("GendersController Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Gender gender)
        {
            try
            {
                await Task.Run(() => DbUtils.db.Genders.Add(gender));
                await DbUtils.db.SaveChangesAsync();
                _logger.Log(LogLevel.Information, "GendersController POST request succes");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"GendersController POST request failed {ex.Message}");
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeGender([FromBody] Gender updateGender)
        {
            try
            {
                if (DbUtils.db.Genders.Any(u => u.IdGender == updateGender.IdGender))
                {
                    Gender gender = DbUtils.db.Genders.First(u => u.IdGender == updateGender.IdGender);
                    gender.GenderName = updateGender.GenderName;
                    await DbUtils.db.SaveChangesAsync();
                    _logger.Log(LogLevel.Information, "GendersController PUT request succes");
                    return Ok();
                }
                else
                {
                    _logger.LogError("User Id Don't exist in DataBase");
                    throw new Exception("User Id Don't exist in DataBase");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("GendersController PUT request failed");
                throw ex;
            }
        }
    }
}
