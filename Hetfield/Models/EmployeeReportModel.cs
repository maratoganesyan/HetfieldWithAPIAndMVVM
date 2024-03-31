﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfield.Models
{
    internal class EmployeeReportModel
    {
        public DateTime DateOfDrawingUp { get; set; }
        public string Staff { get; set; }
        public List<OrderReportModel> Orders { get; set; }
        public decimal TotalCost { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime CurrentDate { get; set; }

        public EmployeeReportModel(User Staff, List<Order> orders, DateOnly StartDate, DateOnly EndDate)
        {
            DateOfDrawingUp = DateTime.Now;
            CurrentDate = DateTime.Now;
            this.Staff = $"{Staff.Surname} {Staff.Name} {OrderReportModel.GetStringOfEmpty(Staff.Patronymic)}";
            Orders = orders.Select(o => new OrderReportModel(o)).ToList();
            TotalCost = Orders.Sum(o => o.FinalPrice);
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
}
