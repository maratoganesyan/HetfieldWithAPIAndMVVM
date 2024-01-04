using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {

        private ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            try
            {
                var roles = await Task.Run(() => DbUtils.db.Roles.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role role)
        {
            try
            {
                await Task.Run(() => DbUtils.db.Roles.Add(role));
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
        public async Task<IActionResult> Put([FromBody] Role updateRole)
        {
            try
            {
                if (DbUtils.db.Roles.Any(u => u.IdRole == updateRole.IdRole))
                {
                    var role = DbUtils.db.Roles.First(u => u.IdRole == updateRole.IdRole);
                    role.RoleName = updateRole.RoleName;
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
