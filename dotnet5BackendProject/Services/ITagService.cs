using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet5BackendProject.Domain;

namespace dotnet5BackendProject.Services
{
    public interface ITagService
    {
        public Task<List<Tag>> GetAllTagsAsync();

        public Task<Tag> GetTagByNameAsync(string tagName);

        public Task<bool> CreateTagAsync(Tag tag);

        public Task<bool> DeleteTagAsync(string tagName);
    }
}