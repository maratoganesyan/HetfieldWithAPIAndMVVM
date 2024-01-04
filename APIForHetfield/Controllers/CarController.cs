using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Car>> Get()
        {
            var cars = await Task.Run(() => DbUtils.db.Cars.AsEnumerable());
            return cars;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car car)
        {
            Task.Run(() => DbUtils.db.Cars.Add(car));
            await DbUtils.db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Car updateCar)
        {
            if (DbUtils.db.Cars.Any(c => c.IdCar == updateCar.IdCar))
            {
                Car car = DbUtils.db.Cars.First(c => c.IdCar == updateCar.IdCar);
                car.IdCarPassport = updateCar.IdCarPassport;
                car.IdTranssmission = updateCar.IdTranssmission;
                car.IdEngine = updateCar.IdEngine;
                car.IdCarConfiguration = updateCar.IdCarConfiguration;
                car.IdCarStatus = updateCar.IdCarStatus;
                car.Price = updateCar.Price;
                car.Mileage = updateCar.Mileage;
                car.Description = updateCar.Description;
                car.CarNumber = updateCar.CarNumber;
                car.TankCapacity = updateCar.TankCapacity;
                car.CarPhotos = updateCar.CarPhotos;
                await DbUtils.db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                throw new Exception("Car Id Don't exist in DataBase");
            }
        }
    }
}
