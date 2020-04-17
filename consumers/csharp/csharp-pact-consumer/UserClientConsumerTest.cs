using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace APIConsumer
{
    public class UserClientConsumerTest : IClassFixture<UserClientConsumerPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public UserClientConsumerTest(UserClientConsumerPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public void When_user_exist_it_return_the_user()
        {
            //Arrange
            _mockProviderService
                .Given("There is a user with the id 123456")
                .UponReceiving("A GET request to retrieve the user")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/api/users/123456",
                    Headers = new Dictionary<string, object>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new
                    {
                        id = 123456,
                        firstName = "Jean",
                        lastName = "du Jardin"
                    }
                });

            var httpClient = new HttpClient{ BaseAddress = new Uri(_mockProviderServiceBaseUri)};
            var consumer = new UserClient(httpClient);

            //Act
            var result = consumer.GetUser(123456);

            //Assert
            Assert.Equal(123456, result.Id);

            _mockProviderService.VerifyInteractions();
        }
    }
}