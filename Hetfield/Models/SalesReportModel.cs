using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfield.Models
{
    internal class SalesReportModel
    {
        public DateTime DateOfDrawingUp { get; set; }
        public List<OrderReportModel> Orders { get; set; }
        public decimal TotalCost { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public SalesReportModel(List<Order> orders, DateOnly StartDate, DateOnly EndDate)
        {
            Orders = orders.Select(o => new OrderReportModel(o)).ToList();
            DateOfDrawingUp = DateTime.Now;
            CurrentDate = DateTime.Now;
            TotalCost = Orders.Sum(o => o.FinalPrice);
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
}
