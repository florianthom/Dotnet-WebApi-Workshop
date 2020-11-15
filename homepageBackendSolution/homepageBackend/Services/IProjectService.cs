using System;
using System.Collections.Generic;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public interface IProjectService
    {
        List<Project> GetProjects();

        Project GetProjectId(Guid projectId);

        bool UpdateProject(Project projectToUpdate);

        bool DeleteProject(Guid projectId);
    }
}