﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hetfield.Models;

public partial class CarsPassport : IDbModel
{
    public int IdCarPassport { get; set; }

    public int CarManufactureYear { get; set; }

    public int IdCarType { get; set; }

    public int IdCarColor { get; set; }

    public int IdOwner { get; set; }

    public string CarModel { get; set; }

    public string VinNumber { get; set; }

    public int CarPower { get; set; }

    public int EngineDisplacement { get; set; }

    public string PassportSeriasAndNumber { get; set; }

    public DateTime DateOfIssue { get; set; }

    public virtual ManufactureYear CarManufactureYearNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual CarColor IdCarColorNavigation { get; set; }

    public virtual CarType IdCarTypeNavigation { get; set; }

    public virtual User IdOwnerNavigation { get; set; }
}