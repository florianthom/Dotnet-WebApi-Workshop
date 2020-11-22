using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Sdk;

namespace homepageBackend.IntegrationTests
{
    public class ProjectsControllerTests : IntegrationTest
    {
        public ProjectsControllerTests() : base()
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

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // Assert
            await AuthenticateAsync();
            var createdProject = await CreateProjectAsync(new CreateProjectRequest()
            {
                Name = "Test Project"
            });

            // Act
            var response =
                await TestClient.GetAsync(ApiRoutes.Projects.Get.Replace("{projectId}", createdProject.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var string1 = await response.Content.ReadAsStringAsync();
            var returnedProject = await response.Content.ReadFromJsonAsync<Project>();
            returnedProject.Id.Should().Be(createdProject.Id);
            returnedProject.Name.Should().Be("Test Project");
        }
    }
}