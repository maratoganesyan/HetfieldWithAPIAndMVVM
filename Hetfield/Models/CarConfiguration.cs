﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using APIForHetfield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hetfield.Models;

public partial class CarConfiguration : DbModelBase
{
    public int IdCarConfiguration { get; set; }

    public string CarConfigurationName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public override async Task<bool> Validate(bool addMode)
    {
        string message = string.Empty;
        if (this == null)
        {
            message = "Ошибка определения типа";
        }
        if (CarConfigurationName.Length == 0)
        {
            message = "Поля не заполнены";
        }
        if (CarConfigurationName.Length > 20)
        {
            message = "Назване комплектации не может быть больше 20 символов";
        }
        ApiClient apiClient = new ApiClient();
        if (await apiClient.GetAllEntityData<CarConfiguration>() is IEnumerable<CarConfiguration> carConfigurations && carConfigurations.Any(cc => cc.CarConfigurationName == this.CarConfigurationName) && addMode)
        {
            message = "Такая запись уже есть в базе";
        }
        return message != string.Empty ? ValidateResult(message) : true;
    }

    public override string ToString()
    {
        return CarConfigurationName;
    }
}