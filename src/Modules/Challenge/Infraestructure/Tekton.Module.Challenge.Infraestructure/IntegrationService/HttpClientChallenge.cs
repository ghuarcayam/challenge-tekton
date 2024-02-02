using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Infraestructure.IntegrationService
{
    internal class HttpClientChallenge
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientChallenge(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            
        }

        public async Task<TResult> SendGet< TResult>(string url)
        {

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            
            using (HttpClient httpClient = httpClientFactory.CreateClient())
            {
                var response = await httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                return result;
            }
        }

        public async Task<TResult> sendPost<TResult,T>(T data, string url)
        {

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = new ObjectContent<T>(data, new JsonMediaTypeFormatter() { });
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                return result;
            }
        }
    }
}
