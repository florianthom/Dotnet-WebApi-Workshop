﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync(GetAllProjectsFilter filter = null, PaginationFilter paginationFilter = null);

        Task<Project> GetProjectByIdAsync(Guid projectId);

        Task<bool> CreateProjectAsync(Project project);

        Task<bool> UpdateProjectAsync(Project projectToUpdate);

        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<bool> UserOwnsProjectAsync(Guid projectId, string userId);
    }
}