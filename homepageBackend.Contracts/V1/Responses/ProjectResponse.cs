using System;
using System.Collections;
using System.Collections.Generic;

namespace homepageBackend.Contracts.V1.Responses
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public IEnumerable<TagResponse> Tags { get; set; }
    }
}