using System.Text.Json.Serialization;

namespace dotnet5BackendProject.Contracts.V1.Requests.Queries
{
    public class GetAllProjectsQuery
    {
        public string UserId { get; set; }
    }
}