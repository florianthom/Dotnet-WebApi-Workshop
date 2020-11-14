using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Contracts.V1;
using homepageBackend.Domain;

namespace homepageBackend.Controllers
{
    public class ProjectsController : Controller
    {
        private List<Project> _projects;
        public ProjectsController()
        {
            _projects = new List<Project>();

            for (int i = 0; i < 5; i++)
            {
                _projects.Add(new Project{Id = Guid.NewGuid().ToString()});
            }   
        }
        
        [HttpGet]
        [Route(ApiRoutes.Projects.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_projects);
        }

        [HttpPost]
        [Route(ApiRoutes.Projects.Create)]
        public IActionResult Create([FromBody] Project project)
        {
            if(string.IsNullOrEmpty(project.Id))
                project.Id = Guid.NewGuid().ToString();
            
            _projects.Add(project);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Projects.Get.Replace("{projectId}", project.Id);
            return Created(locationUri, project);
        }
    }
}