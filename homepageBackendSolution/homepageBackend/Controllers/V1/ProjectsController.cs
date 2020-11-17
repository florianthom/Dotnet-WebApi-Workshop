using System;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Domain;
using homepageBackend.Extensions;
using homepageBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

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
            var userOwnsProject = await _projectService.UserOwnsPostAsync(projectId, HttpContext.GetUserId());

            if (!userOwnsProject)
            {
                return BadRequest(new {error = "You do not own this post"});
            }

            var project = await _projectService.GetProjectIdAsync(projectId);
            project.Name = request.Name;

            var updated = await _projectService.UpdateProjectAsync(project);

            if (updated) return Ok(project);

            return NotFound();
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid projectId)
        {
            var userOwnsProject = await _projectService.UserOwnsPostAsync(projectId, HttpContext.GetUserId());

            if (!userOwnsProject)
            {
                return BadRequest(new {error = "You do not own this post"});
            }

            var deleted = await _projectService.DeleteProjectAsync(projectId);

            if (deleted)
                return NoContent(); // 204

            return NotFound();
        }

        [HttpPost]
        [Route(ApiRoutes.Projects.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest projectRequest)
        {
            var project = new Project
            {
                Name = projectRequest.Name,
                UserId = HttpContext.GetUserId()
            };

            await _projectService.CreateProjectAsync(project);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Projects.Get.Replace("{projectId}", project.Id.ToString());


            var response = new ProjectResponse {Id = project.Id};
            return Created(locationUri, response);
        }
    }
}