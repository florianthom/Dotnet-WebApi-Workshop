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
    // this authorization attribute checks if the user is authenticated
    // because we defined our default AuthenticationScheme in MvcInstaller
    // we can simple write [Authorize] instead of:
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.GetAll)]
        // [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetProjectsAsync();
            var projectResponses = projects.Select(a => new ProjectResponse
            {
                Id = a.Id,
                Name = a.Name,
                UserId = a.UserId,
                Tags = a.Tags.Select(a => new TagResponse()
                {
                    Name = a.TagName
                }).ToList()
            }).ToList();
            return Ok(projectResponses);
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);

            if (project == null)
                NotFound();

            return Ok(new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                UserId = project.UserId,
                Tags = project.Tags.Select(a => new TagResponse()
                {
                    Name = a.TagName
                }).ToList()
            });
        }

        [HttpPut]
        [Route(ApiRoutes.Projects.Update)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromBody] UpdateProjectRequest request)
        {
            var userOwnsProject = await _projectService.UserOwnsPostAsync(projectId, HttpContext.GetUserId());

            if (!userOwnsProject)
            {
                return BadRequest(new {error = "You do not own this post"});
            }

            var project = await _projectService.GetProjectByIdAsync(projectId);
            project.Name = request.Name;

            var updated = await _projectService.UpdateProjectAsync(project);

            if (updated)
                return Ok(new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    UserId = project.UserId,
                    Tags = project.Tags.Select(a => new TagResponse()
                    {
                        Name = a.TagName
                    }).ToList()
                });

            return NotFound();
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.Delete)]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "MustWorkForDotCom")]
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
            var newProjectId = Guid.NewGuid();
            var project = new Project
            {
                Name = projectRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = projectRequest.Tags.Select(a => new ProjectTag()
                {
                    ProjectId = newProjectId,
                    TagName = a
                }).ToList()
            };

            await _projectService.CreateProjectAsync(project);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Projects.Get.Replace("{projectId}", project.Id.ToString());


            var response = new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                UserId = project.UserId,
                Tags = project.Tags.Select(a => new TagResponse()
                {
                    Name = a.TagName
                }).ToList()
            };
            return Created(locationUri, response);
        }
    }
}