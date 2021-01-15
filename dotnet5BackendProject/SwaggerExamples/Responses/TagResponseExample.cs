using dotnet5BackendProject.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace dotnet5BackendProject.SwaggerExamples.Responses
{
    public class TagResponseExample : IExamplesProvider<TagResponse>
    {
        public TagResponse GetExamples()
        {
            return new TagResponse()
            {
                Name = "new tag"
            };
        }
    }
}