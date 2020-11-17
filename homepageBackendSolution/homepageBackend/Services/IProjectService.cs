using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();

        Task<Project> GetProjectIdAsync(Guid projectId);

        Task<bool> CreateProjectAsync(Project project);

        Task<bool> UpdateProjectAsync(Project projectToUpdate);

        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<bool> UserOwnsPostAsync(Guid projectId, string userId);
    }
}