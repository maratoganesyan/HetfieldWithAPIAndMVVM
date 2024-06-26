﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hetfield.Models;

public partial class Car : DbModelBase
{
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

    public override async Task<bool> Validate(bool addMode)
    {
        string message = string.Empty;
        if (Price == null || Price == 0)
            message = "Цена автомобиля не указана";
        if(Mileage == null)
            message = "Пробег не указан";
        if(Description == null || Description == string.Empty)
            message = "Введите небольшое описание для автомобиля";
        if (CarNumber == null || CarNumber.Length < 8)
            message = "Номер автомобиля должен состоять из самого номера и региона";
        if(TankCapacity == null || TankCapacity == 0)
            message = "Объем бензобака не введен";
        if(CarPhotos.Count == 0)
            message = "Фотографии автомобиля не добавлены";
        if (IdCarConfigurationNavigation == null)
            message = "Комплектация автомобиля не выбрана";
        else
            IdCarConfiguration = IdCarConfigurationNavigation.IdCarConfiguration;
        if (IdCarStatusNavigation == null)
            message = "Статус автомобиля не выбран";
        else
            IdCarStatus = IdCarStatusNavigation.IdCarStatus;
        if (IdEngineNavigation == null)
            message = "Двигатель автомобиля не выбран";
        else
            IdEngine = IdEngineNavigation.IdCarEngine;
        if (IdTranssmissionNavigation == null)
            message = "Коробка передач автомобиля не выбрана";
        else
            IdTranssmission = IdTranssmissionNavigation.IdTranssmission;
        if(IdCarPassportNavigation == null)
            message = "Данные ПТС не введены";
        else if (!(await IdCarPassportNavigation.Validate(addMode)))
            return false;
        if (message != string.Empty)
            return ValidateResult(message);
        else
            return true;

    }
}