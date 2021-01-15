using dotnet5BackendProject.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace dotnet5BackendProject.SwaggerExamples.Requests
{
    public class CreateTagRequestExample : IExamplesProvider<CreateTagRequest>
    {
        public CreateTagRequest GetExamples()
        {
            return new CreateTagRequest()
            {
                TagName = "new tag"
            };
        }
    }
}