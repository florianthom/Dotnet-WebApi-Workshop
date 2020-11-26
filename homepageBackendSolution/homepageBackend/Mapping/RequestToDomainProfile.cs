using AutoMapper;
using homepageBackend.Contracts.V1.Requests.Queries;
using homepageBackend.Domain;

namespace homepageBackend.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}