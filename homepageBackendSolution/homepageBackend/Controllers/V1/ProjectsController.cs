using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using homepageBackend.Cache;
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
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.GetAll)]
        [Cache(600)]
        // [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetProjectsAsync();
            // mapping from domain to the contract
            return Ok(_mapper.Map<List<ProjectResponse>>(projects));
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);

            if (project == null)
                NotFound();

            return Ok(_mapper.Map<ProjectResponse>(project));
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
                return Ok(_mapper.Map<ProjectResponse>(project));

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
            return Created(locationUri, _mapper.Map<ProjectResponse>(project));
        }
    }
}