using APIConsumer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace APIConsumer
{
    public class UserApiClient : IUserClient, IDisposable
    {
        private readonly HttpClient _client;

        public HttpClient Client => _client;

        public UserApiClient(string baseUri, string authToken = null)
        {
            _client = new HttpClient {BaseAddress = new Uri(baseUri ?? "http://my.api/v2/capture")};

            if (authToken != null)
            {
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            }
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public User GetUserById(Guid id)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/users/{id}");
            request.Headers.Add("Accept", "application/json");

            var response = Client.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<User>(result.Content.ReadAsStringAsync().Result, _jsonSettings);        
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }
            
            
            return null;
        }

        public async Task<User> CreateUser(User receivedUser)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser(Guid id, User updatedUser)
        {                        
            throw new NotImplementedException();
        }

        private static void RaiseResponseError(HttpRequestMessage failedRequest, HttpResponseMessage failedResponse)
        {
            throw new HttpRequestException(
                String.Concat($"The Users API request for {failedRequest.Method.ToString().ToUpperInvariant()}"
                    , "{failedRequest.RequestUri} failed. Response Status: {(int) failedResponse.StatusCode},"
                    , "Response Body: {failedResponse.Content.ReadAsStringAsync().Result}"));
        }

        public void Dispose()
        {
            Dispose(_client);
        }

        private void Dispose(params IDisposable[] disposables)
        {
            foreach (var disposable in disposables.Where(d => d != null))
            {
                disposable.Dispose();
            }
        }
    }
}
