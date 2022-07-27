using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AlintaPoC.API.IntegrationSQLTests
{
    public class DataControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public DataControllerTests(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/Data/GetAllPeople")]
        public async Task GET_get_all_people(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
