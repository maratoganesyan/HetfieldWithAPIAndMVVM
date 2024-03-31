using APIForHetfield.Models;
using APIForHetfield.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace APIForHetfield.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChartController : Controller
    {
        private ILogger<ChartController> _logger;

        public ChartController(ILogger<ChartController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<Chart>> Get()
        {
            try
            {
                Chart chart = new Chart();
                chart.SalesStatistic = await Task.Run(() => GetSalesStatistics());
                chart.AnnouncementStatistics = await Task.Run(() => GetAnnouncementStatistics());
                chart.OrderByDate = await Task.Run(() => GetOrdersByDates());
                string json = JsonConvert.SerializeObject(chart, Formatting.Indented);
                _logger.LogInformation($"{this.GetType().Name} Get request failed");
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} Get request failed");
                throw ex;
            }
        }

        private (double[] Values, double[] DateTimes) GetOrdersByDates()
        {
            Dictionary<double, double> statistics = new();
            DateTime First = DbUtils.db.Orders.Select(x => x.DateOfOrder).ToList().Min();
            int CountOfDays = (int)(DbUtils.db.Orders.Select(x => x.DateOfOrder).ToList().Max() - First).TotalDays + 1;
            List<Order> Orders = DbUtils.db.Orders.ToList();
            for (int i = 0; i < CountOfDays; i++)
            {
                DateTime temp = First.AddDays(i);
                statistics.Add(temp.ToOADate(),
                    Orders
                    .Where(o => DateOnly.FromDateTime(o.DateOfOrder) == DateOnly.FromDateTime(temp))
                    .Count());
            }
            return (statistics.Values.ToArray(), statistics.Keys.ToArray());
        }

        private static (double[] Sales, string[] ModelsNames) GetSalesStatistics()
        {
            Dictionary<string, double> statistics = new();
            foreach (CarsPassport CP in DbUtils.db.CarsPassports.ToList())
            {
                if (statistics.ContainsKey(CP.CarModel.Substring(0, CP.CarModel.IndexOf(' '))))
                    continue;
                int CountOfSales = DbUtils.db.Orders.ToList()
                    .Where(o => o.IdCarNavigation.IdCarPassportNavigation.CarModel.Substring(0, o.IdCarNavigation.IdCarPassportNavigation.CarModel.IndexOf(' '))
                    == CP.CarModel.Substring(0, CP.CarModel.IndexOf(' '))).
                    Where(o => o.IdOrderStatus == DbUtils.OrderStatuses.Finished).Count();
                if (CountOfSales != 0)
                    statistics.Add(CP.CarModel.Substring(0, CP.CarModel.IndexOf(' ')), CountOfSales);
            }
            return (statistics.Values.ToArray(), statistics.Keys.ToArray());
        }

        private static (double[] Sales, string[] ModelsNames) GetAnnouncementStatistics()
        {
            Dictionary<string, double> statistics = new();
            foreach (CarsPassport CP in DbUtils.db.CarsPassports.ToList())
            {
                if (statistics.ContainsKey(CP.CarModel.Substring(0, CP.CarModel.IndexOf(' '))))
                    continue;
                int CountOfSupplies = DbUtils.db.Cars.ToList()
                    .Where(g => g.IdCarPassportNavigation.CarModel.Substring(0, g.IdCarPassportNavigation.CarModel.IndexOf(' '))
                    == CP.CarModel.Substring(0, CP.CarModel.IndexOf(' '))).ToList()
                    .Where(g => g.IdCarStatus == DbUtils.CarStatuses.Exposed
                            || g.IdCarStatus == DbUtils.CarStatuses.InProcessing)
                    .Count();
                if (CountOfSupplies != 0)
                    statistics.Add(CP.CarModel.Substring(0, CP.CarModel.IndexOf(' ')), CountOfSupplies);
            }
            return (statistics.Values.ToArray(), statistics.Keys.ToArray());
        }
    }
}
