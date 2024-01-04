﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace APIForHetfield.Models;

public partial class Role
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRole { get; set; }

    public string RoleName { get; set; }


    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public override string ToString() => RoleName;
}