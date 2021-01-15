using System;
using System.Threading.Tasks;
using dotnet5BackendProject.Contracts.V1.Requests;
using dotnet5BackendProject.Sdk;
using Refit;

namespace dotnet5BackendProject.SdkSample
{
    // shows an example-flow (getall, create one, get, delete) for other programs
    // requirement: dotnet5BackendProject-server must run
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;
            
            // here 2 different httpClients are used, better use one
            //
            // this will generate the implementation for the identityApi
            var identityApi = RestService.For<IIdentityApi>("https://localhost:5001");
            
            // this will generate the implementation for the Dotnet5BackendApi
            var dotnet5BackendProjectApi = RestService.For<IDotnet5BackendApi>("https://localhost:5001", new RefitSettings
            {
                // to set the bearer-jwt-token, since for access to this api we need to be authenticated
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var registerResponse = await identityApi.RegisterAsync(new UserRegistrationRequest
            {
                Email = "test@test.com",
                Password = "Test1234!"
            });
            
            var loginResponse = await identityApi.LoginAsync(new UserLoginRequest
            {
                Email = "test12@test.com",
                Password = "Test1234!!"
            });

            cachedToken = loginResponse.Content.Token;

            var allProjects = await dotnet5BackendProjectApi.GetAllAsync();

            var createdProject = await dotnet5BackendProjectApi.CreateAsync(new CreateProjectRequest
            {
                Name = "This is created by the SDK",
                Tags = new []{"sdk"}
            });

            var retrievedProject = await dotnet5BackendProjectApi.GetAsync(createdProject.Content.Id);

            var updatedProject = await dotnet5BackendProjectApi.UpdateAsync(createdProject.Content.Id, new UpdateProjectRequest
            {
                Name = "This is updated by the SDK"
            });

            var deleteProject = await dotnet5BackendProjectApi.DeleteAsync(createdProject.Content.Id);
        }

    }
}