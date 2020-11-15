using System;
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
        public IActionResult Get([FromRoute] Guid projectId)
        {
            var project = _projectService.GetProjectId(projectId);

            if (project == null)
                NotFound();

            return Ok(project);
        }

        [HttpPut]
        [Route(ApiRoutes.Projects.Update)]
        public IActionResult Update([FromRoute] Guid projectId, [FromBody] UpdateProjectRequest request)
        {
            var project = new Project
            {
                Id = projectId,
                name = request.Name
            };

            var updated = _projectService.UpdateProject(project);

            if (updated) return Ok(project);

            return NotFound();
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.Delete)]
        public IActionResult Delete([FromRoute] Guid projectId)
        {
            var deleted = _projectService.DeleteProject(projectId);

            if (deleted)
                return NoContent();

            return NotFound();
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