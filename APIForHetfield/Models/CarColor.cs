﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIForHetfield.Models;

public partial class CarColor
{
    public int IdCarColors { get; set; }

    public string ColorName { get; set; }

    public string Hex { get; set; }

    [JsonIgnore]
    public virtual ICollection<CarsPassport> CarsPassports { get; set; } = new List<CarsPassport>();
}