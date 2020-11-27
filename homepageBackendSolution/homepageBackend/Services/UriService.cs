using System;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;

namespace homepageBackend.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetProjectUri(string projectId)
        {
            return new Uri(_baseUri + ApiRoutes.Projects.Get.Replace("{projectId}", projectId));
        }

        public Uri GetAllProjectsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}