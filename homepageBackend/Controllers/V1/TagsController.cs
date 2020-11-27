using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    [Authorize]
    [Produces("application/json")]
    public class TagsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        
        
        public TagsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Returns all the tags in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _projectService.GetAllTagsAsync();
            return Ok(_mapper.Map<List<TagResponse>>(tags));
        }
        
        [HttpGet]
        [Route(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute]string tagName)
        {
            var tag = await _projectService.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<TagResponse>(tag));
        }
        
        /// <summary>
        /// Creates a tag in the system
        /// </summary>
        /// <response code="201">Creates a tag in the system</response>
        /// <response code="400">Unable to create the tag due to validation error</response>
        [HttpPost]
        [Route(ApiRoutes.Tags.Create)]
        [ProducesResponseType(typeof(TagResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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
            return Created(locationUri, _mapper.Map<TagResponse>(newTag));
        }
        
        [HttpDelete]
        [Route(ApiRoutes.Tags.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted = await _projectService.DeleteTagAsync(tagName);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}