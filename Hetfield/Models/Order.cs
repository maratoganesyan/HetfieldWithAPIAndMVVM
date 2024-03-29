﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hetfield.Models;

public partial class Order : DbModelBase
{
    public int IdOrder { get; set; }

    public int IdBuyer { get; set; }

    public int IdCar { get; set; }

    public int IdStaff { get; set; }

    public int IdOrderStatus { get; set; }

    public DateTime DateOfOrder { get; set; }

    public decimal FinalPrice { get; set; }

    public virtual User IdBuyerNavigation { get; set; }

    public virtual Car IdCarNavigation { get; set; }

    public virtual OrderStatus IdOrderStatusNavigation { get; set; }

    public virtual User IdStaffNavigation { get; set; }
}