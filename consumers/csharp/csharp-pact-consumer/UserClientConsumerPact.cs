using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace APIConsumer
{
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
}