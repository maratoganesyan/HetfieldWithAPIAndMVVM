﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIForHetfield.Models;

public partial class CarStatus
{
    public int IdCarStatus { get; set; }

    public string CarStatusName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}