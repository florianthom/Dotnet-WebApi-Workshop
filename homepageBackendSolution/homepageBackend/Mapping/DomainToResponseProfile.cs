using System.Linq;
using AutoMapper;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Domain;

namespace homepageBackend.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Project, ProjectResponse>()
                .ForMember(a => a.Tags, b =>
                    b.MapFrom(src => src.Tags.Select(d => new TagResponse()
                    {
                        Name = d.TagName
                    })));

            CreateMap<Tag, TagResponse>();
        }
    }
}