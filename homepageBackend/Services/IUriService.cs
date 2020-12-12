using System;
using homepageBackend.Contracts.V1.Requests.Queries;

namespace homepageBackend.Services
{
    public interface IUriService
    {
        Uri GetProjectUri(string projectId);

        Uri GetAllProjectsUri(PaginationQuery pagination = null);
        
        Uri GetDocumentUri(string documentId);

        Uri GetAllDocumentsUri(PaginationQuery pagination = null);
    }
}