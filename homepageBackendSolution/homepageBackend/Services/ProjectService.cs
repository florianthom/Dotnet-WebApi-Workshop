using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Data;
using homepageBackend.Domain;
using Microsoft.EntityFrameworkCore;

namespace homepageBackend.Services
{
    public class ProjectService : IProjectService
    { 
        private readonly DataContext _dataContext;

        public ProjectService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _dataContext.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectIdAsync(Guid projectId)
        {
            return await _dataContext.Projects.SingleOrDefaultAsync(a => a.Id == projectId);
        }

        public async Task<bool> CreateProjectAsync(Project project)
        {
            await _dataContext.Projects.AddAsync(project);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateProjectAsync(Project projectToUpdate)
        {
            _dataContext.Projects.Update(projectToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            var project = await GetProjectIdAsync(projectId);

            if (project == null)
                return false;
            
            _dataContext.Projects.Remove(project);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}