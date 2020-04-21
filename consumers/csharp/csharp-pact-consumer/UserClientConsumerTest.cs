using System;
using System.Collections.Generic;
using System.Net.Http;
using APIConsumer.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace APIConsumer.Tests
{
    public class UserClientConsumerTest : IClassFixture<UserClientConsumerPact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public UserClientConsumerTest(UserClientConsumerPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
            _mockProviderService.ClearInteractions();
        }

        [Fact]
        public void GetUserById_WhenTheUserExists_ReturnsUser()
        {
            //Arrange
            var guidRegex = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
            var userId = Guid.Parse("83F9262F-28F1-4703-AB1A-8CFD9E8249C9");
            _mockProviderService
                .Given($"There is an user with id {userId}")
                .UponReceiving($"a request to retrieve user id {userId}")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = Match.Regex($"/api/users/{userId}", $"^\\/api\\/users\\/{guidRegex}$"),
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
                        {"Content-Type", "application/json; charset=utf-8"},
                        { "Server", Match.Type("RubyServer") }
                    },
                    Body = new
                    {
                        userid = userId,
                        firstName = "Jean",
                        lastName = "du Jardin"
                    }
                });
            
            var consumer = new UserApiClient(_mockProviderServiceBaseUri);

            //Act
            var result = consumer.GetUserById(userId);

            //Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal("Jean", result.FirstName);
            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void WhenCreateUser_ReturnUser()
        {
         //TODO
        }

        [Fact]
        public void WhenUpdateExistingUser_ReturnUser()
        {
            //TODO
        }
    }
}