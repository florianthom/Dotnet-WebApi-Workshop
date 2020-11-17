using System;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Domain;
using homepageBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace homepageBackend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetProjectsAsync());
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid projectId)
        {
            var project = await _projectService.GetProjectIdAsync(projectId);

            if (project == null)
                NotFound();

            return Ok(project);
        }

        [HttpPut]
        [Route(ApiRoutes.Projects.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromBody] UpdateProjectRequest request)
        {
            HttpContext.User.Claims.Single(x => x.Type == "id");
            var project = new Project
            {
                Id = projectId,
                name = request.Name
            };

            var updated = await _projectService.UpdateProjectAsync(project);

            if (updated) return Ok(project);

            return NotFound();
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid projectId)
        {
            var deleted = await _projectService.DeleteProjectAsync(projectId);

            
            if (deleted)
                return NoContent(); // 204

            return NotFound();
        }

        [HttpPost]
        [Route(ApiRoutes.Projects.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest projectRequest)
        {
            var project = new Project {name = projectRequest.Name};
            
            await _projectService.CreateProjectAsync(project);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Projects.Get.Replace("{projectId}", project.Id.ToString());


            var response = new ProjectResponse {Id = project.Id};
            return Created(locationUri, project);
        }
    }
}