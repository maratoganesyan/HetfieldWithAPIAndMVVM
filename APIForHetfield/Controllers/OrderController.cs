using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            try
            {
                var order = await Task.Run(() => DbUtils.db.Orders.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            try
            {
                Car car = DbUtils.db.Cars.Single(c => c.IdCar == order.IdCar);
                if (order.IdOrderStatus == DbUtils.OrderStatuses.InProcessing)
                    car.IdCarStatus = DbUtils.CarStatuses.InProcessing;
                else if (order.IdOrderStatus == DbUtils.OrderStatuses.Finished)
                    car.IdCarStatus = DbUtils.CarStatuses.Saled;
                else if (order.IdOrder == DbUtils.OrderStatuses.Deleted)
                    car.IdCarStatus = DbUtils.CarStatuses.Exposed;

                order.IdCarNavigation = null;
                order.IdOrderStatusNavigation = null;
                order.IdBuyerNavigation = null;
                order.IdStaffNavigation = null;
                DbUtils.db.Orders.Add(order);
                DbUtils.db.SaveChanges();
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
        public async Task<IActionResult> Put([FromBody] Order updateOrder)
        {
            try
            {
                if(DbUtils.db.Orders.Any(o => o.IdOrder == updateOrder.IdOrder))
                {
                    Order order = DbUtils.db.Orders.Single(o => o.IdOrder == updateOrder.IdOrder);
                    if(order.IdCar != updateOrder.IdCar)
                    {
                        Car PreviewCar = DbUtils.db.Cars.Single(c => c.IdCar == order.IdCar);
                        PreviewCar.IdCarStatus = DbUtils.CarStatuses.Exposed;
                    }

                    order.IdCar = updateOrder.IdCar;
                    order.IdOrderStatus = updateOrder.IdOrderStatus;
                    order.DateOfOrder = updateOrder.DateOfOrder;
                    order.IdStaff = updateOrder.IdStaff;
                    order.FinalPrice = updateOrder.FinalPrice;
                    order.IdBuyer = updateOrder.IdBuyer;
                    Car car = DbUtils.db.Cars.Single(c => c.IdCar == order.IdCar);
                    if (order.IdOrderStatus == DbUtils.OrderStatuses.InProcessing)
                        car.IdCarStatus = DbUtils.CarStatuses.InProcessing;
                    else if (order.IdOrderStatus == DbUtils.OrderStatuses.Finished)
                        car.IdCarStatus = DbUtils.CarStatuses.Saled;
                    else if (order.IdOrderStatus == DbUtils.OrderStatuses.Deleted)
                        car.IdCarStatus = DbUtils.CarStatuses.Exposed;
                    DbUtils.db.SaveChanges();
                }
                else
                {
                    throw new Exception("Order Id Don't exist in DataBase");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} PUT request failed");
                throw ex;
            }
        }
    }
}
