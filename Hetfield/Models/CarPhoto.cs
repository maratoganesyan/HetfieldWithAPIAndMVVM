﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hetfield.Models;

public partial class CarPhoto : IDbModel
{
    public int IdCar { get; set; }

    public int IdPhoto { get; set; }

    public byte[] Photo { get; set; }

    public virtual Car IdCarNavigation { get; set; }
}