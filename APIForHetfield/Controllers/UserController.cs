using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var users = await Task.Run(() => DbUtils.db.Users.AsEnumerable());
                _logger.Log(LogLevel.Information, "UsersController Get request succes");
                return users;
            }
            catch(Exception ex)
            {
                _logger.LogError("UsersController Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            Task.Run(() => DbUtils.db.Users.Add(user));
            await DbUtils.db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUser([FromBody] User updateuser)
        {
            if (DbUtils.db.Users.Any(u => u.IdUser == updateuser.IdUser))
            {
                User user = DbUtils.db.Users.First(u => u.IdUser == updateuser.IdUser);
                user.Surname = updateuser.Surname;
                user.Name = updateuser.Name;
                user.Patronymic = updateuser.Patronymic;
                user.Login = updateuser.Password;
                user.Password = updateuser.Password;
                user.DateOfBirth = updateuser.DateOfBirth;
                user.PhoneNumber = updateuser.PhoneNumber;
                user.Email = updateuser.Email;
                user.IdRole = updateuser.IdRole;
                user.IdGender = updateuser.IdGender;
                user.Photo = updateuser.Photo;
                await DbUtils.db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                throw new Exception("User Id Don't exist in DataBase");
            }
        }
    }
}
