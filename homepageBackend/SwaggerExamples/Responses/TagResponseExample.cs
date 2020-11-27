using homepageBackend.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace homepageBackend.SwaggerExamples.Responses
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