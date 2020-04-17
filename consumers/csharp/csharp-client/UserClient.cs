using APIConsumer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIConsumer
{
    public class UserClient : IUserClient
    {
        private const string BASEURI = "http://localhost:4200";
        private readonly HttpClient _client;

        public HttpClient Client => _client;

        public UserClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<User> GetUser(long id)
        {

            var response = await Client.GetAsync($"{BASEURI}/api/users/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }

        public async Task<User> CreateUser(User receivedUser)
        {
            var content = JsonConvert.SerializeObject(receivedUser);

            var response = await Client.PostAsync($"{BASEURI}/api/users"
                , new StringContent(content, Encoding.Default, "application/json"));
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return user;
        }

        public async Task<User> UpdateUser(long id, User updatedUser)
        {                        
       
            var content = JsonConvert.SerializeObject(updatedUser);

            var response = await Client.PutAsync($"{BASEURI}/api/users/{id}"
                , new StringContent(content, Encoding.Default, "application/json"));
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return user;
        }
    }
}
