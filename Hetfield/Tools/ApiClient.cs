using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Hetfield.Models;
using System.Net;
using Newtonsoft.Json;

namespace APIForHetfield;

class ApiClient
{
    private static bool IsHttpClientCreated = false;
    private static HttpClient _httpClient;

    private readonly string baseUrl = "https://localhost:7081";

    public ApiClient()
    {
        if (IsHttpClientCreated)
            return;
        _httpClient = new HttpClient();
        IsHttpClientCreated = true;
    }

    public async Task<TEntity> GetSingleEntityData<TEntity>() where TEntity : DbModelBase
    {
        string request = baseUrl + "/" + typeof(TEntity).Name;
        var response = await _httpClient.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return (TEntity)Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(TEntity));
        }
        else
        {
            throw new Exception($"Ошибка: {response.StatusCode}");
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllEntityData<TEntity>() where TEntity : DbModelBase
    {
        string request = baseUrl + "/" + typeof(TEntity).Name;
        var response = await _httpClient.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TEntity>>(data);
        }
        else
        {
            throw new Exception($"Ошибка: {response.StatusCode}");
        }
    }

    public async Task<IEnumerable<TEntity>> GetSearchedEntityDataAsync<TEntity>(string searchText) where TEntity : DbModelBase
    {
        string request = baseUrl + "/" + typeof(TEntity).Name;
        var response = await _httpClient.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var ConvertedData = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TEntity>>(data);
            return ConvertedData.Where(m => m.ToString().Contains(searchText));
        }
        else
        {
            throw new Exception($"Ошибка: {response.StatusCode}");
        }
    }

    public async Task<bool> AddEntityDataAsync<TEntity>(TEntity entityData) where TEntity : DbModelBase
    {
        var entityDataInJsonFormat = System.Text.Json.JsonSerializer.Serialize(entityData);
        var content = new StringContent(entityDataInJsonFormat, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(baseUrl + "/" + typeof(TEntity).Name, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}");
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> ChangeEntityDataAsync<TEntity>(TEntity entityData) where TEntity : DbModelBase
    {
        var entityDataInJsonFormat = System.Text.Json.JsonSerializer.Serialize(entityData);
        var content = new StringContent(entityDataInJsonFormat, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(baseUrl + "/" + typeof(TEntity).Name, content);

        if(!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}");
        }
        else
        {
            return true;
        }
    }
    //public async void SaveUser(string endpoint, User user)
    //{

    //    user.Id = 0;
    //    var json = System.Text.Json.JsonSerializer.Serialize(user);
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");

    //    var response = await _httpClient.PostAsync(BaseUrl + endpoint, content);

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        throw new Exception($"Error: {response.StatusCode}");
    //    }
    //}

    //public async void ChangeUser(string endpoint, User user)
    //{
    //    var json = System.Text.Json.JsonSerializer.Serialize(user);
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");
    //    await _httpClient.PutAsync(BaseUrl + endpoint, content);
    //}
}
