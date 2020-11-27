using homepageBackend.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace homepageBackend.SwaggerExamples.Requests
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