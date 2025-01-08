using MSCaddie.Client.Model;
using MSCaddie.Shared.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace MSCaddie.Client.Services;

public class UserService : IUserService
{
    public HttpClient _httpClient { get; }

    public UserService(HttpClient httpClient)
    {

        //httpClient.BaseAddress = new Uri(_appSettings.BookStoresBaseAddress);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

        _httpClient = httpClient;
    }

    public async Task<User> LoginAsync(User user)
    {
        //user.Password = Utility.Encrypt(user.Password);
        string serializedUser = JsonConvert.SerializeObject(user);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

        return await Task.FromResult(returnedUser);

    }

    public async Task<User> RegisterUserAsync(User user)
    {
        //user.Password = Utility.Encrypt(user.Password);
        string serializedUser = JsonConvert.SerializeObject(user);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RegisterUser");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

        return await Task.FromResult(returnedUser);
    }

    public async Task<User> RefreshTokenAsync(RefreshRequest refreshRequest)
    {
        string serializedUser = JsonConvert.SerializeObject(refreshRequest);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RefreshToken");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

        return await Task.FromResult(returnedUser);
    }

    public async Task<User> GetUserByAccessTokenAsync(string accessToken)
    {
        string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
        requestMessage.Content = new StringContent(serializedRefreshRequest);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

        return await Task.FromResult(returnedUser);
    }
}

