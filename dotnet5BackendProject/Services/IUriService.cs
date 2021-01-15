using System;
using dotnet5BackendProject.Contracts.V1.Requests.Queries;

namespace dotnet5BackendProject.Services
{
    public interface IUriService
    {
        Uri GetProjectUri(string projectId);

        Uri GetAllProjectsUri(PaginationQuery pagination = null);
        
        Uri GetDocumentUri(string documentId);

        Uri GetAllDocumentsUri(PaginationQuery pagination = null);
    }
}