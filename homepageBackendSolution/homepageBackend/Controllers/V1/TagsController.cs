using System;
using System.Collections.Generic;
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

namespace homepageBackend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class TagsController : Controller
    {
        private readonly IProjectService _projectService;
        
        public TagsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        
        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _projectService.GetAllTagsAsync();
            var tagResponses = tags.Select(a => new TagResponse()
            {
                Name = a.Name
            }).ToList();
            return Ok(tagResponses);
        }
        
        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute]string tagName)
        {
            var tag = await _projectService.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                return NotFound();
            }
            
            return Ok(new TagResponse()
            {
                Name = tag.Name
            });
        }
        
        
        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var newTag = new Tag
            {
                Name = request.TagName,
                CreatorId = HttpContext.GetUserId(),
                CreatedOn = DateTime.UtcNow
            };

            var created = await _projectService.CreateTagAsync(newTag);
            if (!created)
            {
                return BadRequest(new ErrorResponse(new ErrorModel{Message = "Unable to create tag"}));
            }
                
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);
            return Created(locationUri, new TagResponse()
            {
                Name = newTag.Name
            });
        }
        
        [HttpDelete(ApiRoutes.Tags.Delete)] 
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted = await _projectService.DeleteTagAsync(tagName);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}