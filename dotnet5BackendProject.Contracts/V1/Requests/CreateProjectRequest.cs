using System;
using System.Collections.Generic;

namespace dotnet5BackendProject.Contracts.V1.Requests
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }
        
        public IEnumerable<string> Tags { get; set; }

    }
}