using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using homepageBackend.Contracts.V1;
using homepageBackend.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace homepageBackend.IntegrationTests
{
    public class ProjectsControllerTests : IntegrationTest
    {
        
        public ProjectsControllerTests()
        {
        }
        
        
        // naming convention: methodname_Scenario_ExpectedReturn
        //
        // content of each test: "The triple A"
        //    - 1. Arrange
        //    - 2. Act
        //    - 3. Assert
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Projects.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadFromJsonAsync<List<Project>>()).Should().BeEmpty();
        }

    }
}