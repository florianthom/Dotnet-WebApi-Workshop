using System;
using System.Threading.Tasks;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Sdk;
using Refit;

namespace homepageBackend.SdkSample
{
    // shows an example-flow (getall, create one, get, delete) for other programs
    // requirement: homepageBackend-server must run
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;
            
            // here 2 different httpClients are used, better use one
            //
            // this will generate the implementation for the identityApi
            var identityApi = RestService.For<IIdentityApi>("https://localhost:5001");
            
            // this will generate the implementation for the HomepageBackendApi
            var homepageBackendApi = RestService.For<IHomepageBackendApi>("https://localhost:5001", new RefitSettings
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

            var allProjects = await homepageBackendApi.GetAllAsync();

            var createdProject = await homepageBackendApi.CreateAsync(new CreateProjectRequest
            {
                Name = "This is created by the SDK",
                Tags = new []{"sdk"}
            });

            var retrievedProject = await homepageBackendApi.GetAsync(createdProject.Content.Id);

            var updatedProject = await homepageBackendApi.UpdateAsync(createdProject.Content.Id, new UpdateProjectRequest
            {
                Name = "This is updated by the SDK"
            });

            var deleteProject = await homepageBackendApi.DeleteAsync(createdProject.Content.Id);
        }

    }
}