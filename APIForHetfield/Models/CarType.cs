﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIForHetfield.Models;

public partial class CarType
{
    public int IdCarType { get; set; }

    public string TypeName { get; set; }

    [JsonIgnore]
    public virtual ICollection<CarsPassport> CarsPassports { get; set; } = new List<CarsPassport>();
}