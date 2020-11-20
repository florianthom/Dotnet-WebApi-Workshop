using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace homepageBackend.IntegrationTests
{
    public class UnitTest1
    {

        private readonly HttpClient _client;

        public UnitTest1()
        {
            // is the in-memory-server
            // the in-memory-server (stores in a property) gets created with the CreateClient()-Method
            // the CreateClient()-Method
            //    1. initializes the in-memory-server
            //    2. creates the client
            var appFactory = new WebApplicationFactory<Startup>();
            // initializes the server + creates the corresponding client
            _client = appFactory.CreateClient();
        }
        
        [Fact]
        public void Test1()
        {
        }
    }
}