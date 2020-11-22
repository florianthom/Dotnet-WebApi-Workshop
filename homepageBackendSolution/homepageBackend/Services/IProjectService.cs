using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();

        Task<Project> GetProjectByIdAsync(Guid projectId);

        Task<bool> CreateProjectAsync(Project project);

        Task<bool> UpdateProjectAsync(Project projectToUpdate);

        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<bool> UserOwnsPostAsync(Guid projectId, string userId);
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag> GetTagByNameAsync(string tagName);
        Task<bool> CreateTagAsync(Tag tag);
        Task<bool> DeleteTagAsync(string tagName);
    }
}