using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MSCaddie.Client.Services
{
    public abstract class ClientAPI
    {
        protected readonly HttpClient _client;
        protected readonly JsonSerializerOptions _jsonSerializerOptions;

        protected ClientAPI(HttpClient client)
        {
            _client = client;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected async Task<TReturn?> GetAsync<TReturn>(string relativeUri)
        {
            HttpResponseMessage res = await _client.GetAsync(relativeUri);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>(_jsonSerializerOptions);
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<TReturn?> PostAsync<TReturn, TRequest>(string relativeUri, TRequest request)
        {
            HttpResponseMessage res = await _client.PostAsJsonAsync<TRequest>(relativeUri, request);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>(_jsonSerializerOptions);
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<TReturn?> PutAsync<TReturn, TRequest>(string relativeUri, TRequest request)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync<TRequest>(relativeUri, request);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>(_jsonSerializerOptions);
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<HttpResponseMessage> OptionsAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Options, url);//There is no client.Options so we need to create a HttpRequestMessage
            return await _client.SendAsync(request);
        }
    }
}
