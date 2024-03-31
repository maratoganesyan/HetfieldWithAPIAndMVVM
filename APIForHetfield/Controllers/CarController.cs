using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                if (car.IdCarPassport == null)
                    throw new Exception("CarPassport don't Init");
                if (car.CarPhotos.Count() == 0)
                    throw new Exception("CarPhotos don't Init");

                // Car Passport add start

                CarsPassport carsPassport = new CarsPassport();
                carsPassport = car.IdCarPassportNavigation;
                carsPassport.CarManufactureYearNavigation = null;
                carsPassport.IdCarColorNavigation = null;
                carsPassport.IdCarTypeNavigation = null;
                carsPassport.IdOwnerNavigation = null;
                await Task.Run(() => DbUtils.db.CarsPassports.Add(carsPassport));
                DbUtils.db.SaveChanges();

                // Car Passport add end 

                var CarPhotos = car.CarPhotos;
                //car add start
                car.CarPhotos = null;
                car.IdCarPassportNavigation = null;
                car.IdCarConfigurationNavigation = null;
                car.IdCarStatusNavigation = null;
                car.IdEngineNavigation = null;
                car.IdTranssmissionNavigation = null;
                car.IdCarPassport = DbUtils.db.CarsPassports.OrderBy(cp => cp.IdCarPassport).Last().IdCarPassport;
                await Task.Run(() => DbUtils.db.Cars.Add(car));
                DbUtils.db.SaveChanges();
                //car add end

                // car Photos add start


                foreach (var photo in CarPhotos)
                {
                    photo.IdCar = DbUtils.db.Cars.OrderBy(c => c.IdCar).Last().IdCar;
                    await Task.Run(() => DbUtils.db.CarPhotos.Add(photo));
                }

                DbUtils.db.SaveChanges();
                //car Photos add end
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Car updateCar)
        {
            if (DbUtils.db.Cars.Any(c => c.IdCar == updateCar.IdCar))
            {
                Car car = DbUtils.db.Cars.First(c => c.IdCar == updateCar.IdCar);
                CarsPassport carPassport= DbUtils.db.CarsPassports.First(c => c.IdCarPassport == updateCar.IdCarPassport);

                carPassport.CarPower = updateCar.IdCarPassportNavigation.CarPower;
                carPassport.VinNumber = updateCar.IdCarPassportNavigation.VinNumber;
                carPassport.PassportSeriasAndNumber = updateCar.IdCarPassportNavigation.PassportSeriasAndNumber;
                carPassport.CarManufactureYear = updateCar.IdCarPassportNavigation.CarManufactureYear;
                carPassport.CarModel = updateCar.IdCarPassportNavigation.CarModel;
                carPassport.EngineDisplacement = updateCar.IdCarPassportNavigation.EngineDisplacement;
                carPassport.DateOfIssue = updateCar.IdCarPassportNavigation.DateOfIssue;
                carPassport.IdCarType = updateCar.IdCarPassportNavigation.IdCarType;
                carPassport.IdCarColor = updateCar.IdCarPassportNavigation.IdCarColor;
                carPassport.IdOwner = updateCar.IdCarPassportNavigation.IdOwner;
                //car update start
                car.IdTranssmission = updateCar.IdTranssmission;
                car.IdEngine = updateCar.IdEngine;
                car.IdCarConfiguration = updateCar.IdCarConfiguration;
                car.IdCarStatus = updateCar.IdCarStatus;
                car.Price = updateCar.Price;
                car.Mileage = updateCar.Mileage;
                car.Description = updateCar.Description;
                car.CarNumber = updateCar.CarNumber;
                car.TankCapacity = updateCar.TankCapacity;
                // car update end

                foreach(var photo in updateCar.CarPhotos)
                {
                    if (DbUtils.db.CarPhotos.FirstOrDefault(cp => cp.IdPhoto == photo.IdPhoto && cp.IdCar == photo.IdCar) is CarPhoto carPhoto && carPhoto != null)
                        carPhoto.Photo = photo.Photo;
                    else
                    {
                        photo.IdCar = updateCar.IdCar;
                        DbUtils.db.CarPhotos.Add(photo);
                    }
                }
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
