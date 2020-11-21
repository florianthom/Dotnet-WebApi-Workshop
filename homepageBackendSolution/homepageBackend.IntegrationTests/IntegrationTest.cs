﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace homepageBackend.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        protected readonly WebApplicationFactory<homepageBackend.Startup> AppFactory;
        
        // - this tests use xUnit
        // - xUnit.net offers 3 basic methods for setup and cleanup code
        //    - Constructor and Dispose (shared setup/cleanup code without sharing object instances)
        //    - Class Fixtures (shared object instance across tests in a single class)
        //    - Collection Fixtures (shared object instances across multiple test classes)
        //
        // - here we use 1.: Constructor and Dispose
        // - because of that:
        // - xUnit.net creates a new instance of the test class for every test that is run
        //     so any code which is placed into the constructor of the test class will be run
        //     for every single test
        // - to make this work:
        //    For context cleanup, add the IDisposable interface to your test class, and put
        //    the cleanup code in the Dispose() method
        //
        // https://xunit.net/docs/shared-context
        protected IntegrationTest()
        {
            AppFactory = new InMemoryWebApplicationFactory<homepageBackend.Startup>();
            TestClient = AppFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "test@test.com",
                Password = "Florian1234!!"
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }
        
        public void Dispose()
        {
            TestClient.Dispose(); 
            AppFactory.Dispose();
            
        }
    }
}



