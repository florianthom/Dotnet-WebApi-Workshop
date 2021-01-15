using System.Linq;
using AutoMapper;
using dotnet5BackendProject.Contracts.V1.Responses;
using dotnet5BackendProject.Domain;

namespace dotnet5BackendProject.Mapping
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

            CreateMap<Document, DocumentResponse>()
                .ForMember(a => a.Tags, b =>
                    b.MapFrom(src => src.Tags.Select(d => new TagResponse()
                    {
                        Name = d.TagName,
                        CreatedOn = d.Tag.CreatedOn,
                        CreatorId = d.Tag.CreatorId,
                        UpdatedOn = d.Tag.UpdatedOn,
                        UpdaterId = d.Tag.UpdaterId
                    })));
        }
    }
}