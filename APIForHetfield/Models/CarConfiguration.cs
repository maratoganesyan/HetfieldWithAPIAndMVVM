﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIForHetfield.Models;

public partial class CarConfiguration
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCarConfiguration { get; set; }

    public string CarConfigurationName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public override string ToString() => CarConfigurationName;
}