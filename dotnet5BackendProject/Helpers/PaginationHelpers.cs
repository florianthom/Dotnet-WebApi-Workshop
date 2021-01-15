using System.Collections.Generic;
using System.Linq;
using dotnet5BackendProject.Contracts.V1.Requests.Queries;
using dotnet5BackendProject.Contracts.V1.Responses;
using dotnet5BackendProject.Domain;
using dotnet5BackendProject.Services;

namespace dotnet5BackendProject.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(
            IUriService uriService,
            PaginationFilter paginationFilter,
            List<T> response
        )
        {
            // caluclate next/previous page
            var nextPage = paginationFilter.PageNumber >= 1
                ? uriService
                    .GetAllProjectsUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize))
                    .ToString()
                : null;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? uriService
                    .GetAllProjectsUri(new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize))
                    .ToString()
                : null;

            // mapping from domain to the contract
            return new PagedResponse<T>()
            {
                Data = response,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?) null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?) null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}