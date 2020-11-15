using System;
using System.Collections.Generic;
using System.Linq;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Domain;
using homepageBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace homepageBackend.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        { 
            _projectService = projectService;
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_projectService.GetProjects());
        }
        
        [HttpGet]
        [Route(ApiRoutes.Projects.Get)]
        public IActionResult Get(Guid projectId)
        {
            var project = _projectService.GetProjectId(projectId);

            if (project == null)
                NotFound();
            
            return Ok(project);
        }

        [HttpPost]
        [Route(ApiRoutes.Projects.Create)]
        public IActionResult Create([FromBody] CreateProjectRequest projectRequest)
        {
            var project = new Project {Id = projectRequest.Id};
            
            
            if (project.Id == Guid.Empty)
                project.Id = Guid.NewGuid();

            _projectService.GetProjects().Add(project);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Projects.Get.Replace("{projectId}", project.Id.ToString());

            
            
            var response = new ProjectResponse {Id = project.Id};
            return Created(locationUri, project);
        }
    }
}