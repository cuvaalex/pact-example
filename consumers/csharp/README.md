# C# Consumer example with PactNet
This example show how to use Pact Foundation with a consumer in C#

## Configuration
The example is configured as follow:
> csharp-client -> API Client
> csharp-pact-consumer -> pact test for the consumer

Before start, we need to create a project with dotnet as follow:

> mkdir xxx-pact
> cd xxx-pact
> dotnet new xunit
> dotnet add package PactNet.Windows (1)

(1) There is different PactNet librairies, depends if you will run the test under a windows system, or Mac or Linux

### Client Librairy
We will need for this example create a .Net Core Rest Client API, to have a solid base for our example

> mkdir xxx-client
> cd xxx-client
> dotnet new classlib
> dotnet add package System.Text.Json
> dotnet add package Systen. 

We will add the following client class

~~~csharp
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
~~~

## Pact Test
### Describe and configuring the pact as a service consumer with a mock service
We create a pact configuration test case into our test consumer project with xUnit

~~~csharp
public class UserClientConsumerPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort => 9222;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public UserClientConsumerPact() 
        {
            PactBuilder = new PactBuilder(new PactConfig {LogDir = @"..\..\..\logs", PactDir = @"..\..\..\pacts", SpecificationVersion = "2.0.0"});
            PactBuilder.ServiceConsumer("cs-csharp")
                .HasPactWith("userservice");
            MockProviderService = PactBuilder.MockService(MockServerPort);
        }


        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
~~~

### Our test Case

~~~csharp

~~~


