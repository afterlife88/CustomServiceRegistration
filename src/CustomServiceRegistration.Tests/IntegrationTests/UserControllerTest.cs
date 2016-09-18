using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace CustomServiceRegistration.Tests.IntegrationTests
{
    public class UserControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UserControllerTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        [Fact]
        public async Task GetUser_ReturningUnathorize_WhenRequestingWithoutToken()
        {
            // Act
            var response = await _client.GetAsync("/api/user/getUser/exampleUser@gmail.com");

            // Assert
            Assert.Equal(response.StatusCode, HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task GetUser_ReturningSuccessfulStatusCode_WhenRequestingWithRightToken()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config.Token);
            var response = await _client.GetAsync("/api/user/getUser/test@devchallenge.it");

            // Assert
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }

    }
}
