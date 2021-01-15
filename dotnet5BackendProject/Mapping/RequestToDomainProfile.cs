using AutoMapper;
using dotnet5BackendProject.Contracts.V1.Requests.Queries;
using dotnet5BackendProject.Domain;

namespace dotnet5BackendProject.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllProjectsQuery, GetAllProjectsFilter>();
        }
    }
}