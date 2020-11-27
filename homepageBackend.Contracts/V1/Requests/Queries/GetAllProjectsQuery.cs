using System.Text.Json.Serialization;

namespace homepageBackend.Contracts.V1.Requests.Queries
{
    public class GetAllProjectsQuery
    {
        public string UserId { get; set; }
    }
}