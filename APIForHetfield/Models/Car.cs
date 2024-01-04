﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIForHetfield.Models;

public partial class Car
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCar { get; set; }

    public int IdCarPassport { get; set; }

    public int IdTranssmission { get; set; }

    public int IdEngine { get; set; }

    public int IdCarConfiguration { get; set; }

    public int IdCarStatus { get; set; }

    public decimal Price { get; set; }

    public int Mileage { get; set; }

    public string Description { get; set; }

    public string CarNumber { get; set; }

    public int TankCapacity { get; set; }

    public virtual ICollection<CarPhoto> CarPhotos { get; set; } = new List<CarPhoto>();

    public virtual CarConfiguration IdCarConfigurationNavigation { get; set; }

    public virtual CarsPassport IdCarPassportNavigation { get; set; }

    public virtual CarStatus IdCarStatusNavigation { get; set; }

    public virtual CarEngine IdEngineNavigation { get; set; }

    public virtual CarTranssmission IdTranssmissionNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return "Mercedez-Benz " + IdCarPassportNavigation.CarModel
                + " " + IdCarPassportNavigation.VinNumber + " " +
                IdCarStatusNavigation.CarStatusName + " " +
                IdCarPassportNavigation.IdOwnerNavigation.Surname + " " +
                IdCarPassportNavigation.IdOwnerNavigation.Name + " " +
                IdCarPassportNavigation.IdOwnerNavigation.Patronymic + " " +
                IdCarPassportNavigation.IdOwnerNavigation.PhoneNumber;
    }
}