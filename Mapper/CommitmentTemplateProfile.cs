using AutoMapper;
using CommitmentUI.Domain;
using CommitmentUI.Presentation;

namespace CommitmentUI.Mapper
{
    public class CommitmentTemplateProfile : Profile
    {
        public CommitmentTemplateProfile()
        {
            CreateMap<CommitmentTemplate, CommitmentTemplateDto>()
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => DateTime.Now.Add(src.DefaultOffset).ToString("g")));
        }
    }
}