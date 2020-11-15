using System;
using System.Collections.Generic;
using System.Linq;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public class ProjectService : IProjectService
    {
        private List<Project> _projects;

        public ProjectService()
        {
            _projects = new List<Project>();

            for (var i = 0; i < 5; i++) _projects.Add(new Project
            {
                Id = Guid.NewGuid(),
                name = "test name " + i,
            });
        }
        
        public List<Project> GetProjects()
        {
            return _projects;
        }

        public Project GetProjectId(Guid projectId)
        {
            return _projects.SingleOrDefault(a => a.Id == projectId);
        }
    }
}