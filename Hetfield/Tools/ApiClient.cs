using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Hetfield.Models;

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

    public async Task<IEnumerable<TEntity>> GetAllEntityData<TEntity>() where TEntity : IDbModel
    {
        string request = baseUrl + "/" + typeof(TEntity).Name;
        var response = await _httpClient.GetAsync(request); 

        if(response.IsSuccessStatusCode) 
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TEntity>>(data);
        }
        else
        {
            throw new Exception($"Ошибка: {response.StatusCode}");
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

    //public async void DeleteUser(string endpoint, int id)
    //{
    //    var response = await _httpClient.DeleteAsync($"{BaseUrl}{endpoint}/{id}");

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        throw new Exception($"Error: {response.StatusCode}");
    //    }
    //}
}
