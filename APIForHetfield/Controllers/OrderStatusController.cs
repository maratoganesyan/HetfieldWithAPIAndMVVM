using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderStatusController : Controller
    {
        private ILogger<OrderStatusController> _logger;

        public OrderStatusController(ILogger<OrderStatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderStatus>> Get()
        {
            try
            {
                var orderStatus = await Task.Run(() => DbUtils.db.OrderStatuses.AsEnumerable());
                _logger.Log(LogLevel.Information, $"{this.GetType().Name} Get request succes");
                return orderStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderStatus orderStatus)
        {
            try
            {
                await Task.Run(() => DbUtils.db.OrderStatuses.Add(orderStatus));
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
        public async Task<IActionResult> Put([FromBody] OrderStatus updateOrderStatus)
        {
            try
            {
                if (DbUtils.db.OrderStatuses.Any(u => u.IdOrderStatus == updateOrderStatus.IdOrderStatus))
                {
                    var orderStatus = DbUtils.db.OrderStatuses.First(u => u.IdOrderStatus == updateOrderStatus.IdOrderStatus);
                    orderStatus.OrderStatusName = updateOrderStatus.OrderStatusName;
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
