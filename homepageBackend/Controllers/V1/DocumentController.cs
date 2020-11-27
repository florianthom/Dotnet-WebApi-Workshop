using System.Threading.Tasks;
using AutoMapper;
using homepageBackend.Cache;
using homepageBackend.Contracts.V1;
using homepageBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace homepageBackend.Controllers
{
    public class DocumentController
    {
        private readonly DocumentService _documentService;
        private readonly UriService _uriService;
        private readonly IMapper _mapper;

        public DocumentController(DocumentService documentService, UriService uriService, IMapper mapper)
        {
            _documentService = documentService;
            _uriService = uriService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.Documents.GetAll)]
        [Cache(600)]
        public Task<IActionResult> GetAll()
        {
            // var documents = _documentService
            return null;
        }
    }
}