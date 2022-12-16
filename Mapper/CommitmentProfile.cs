using AutoMapper;
using CommitmentUI.Domain;
using CommitmentUI.Presentation;

namespace CommitmentUI.Mapper
{
    public class CommitmentProfile : Profile
    {
        public CommitmentProfile()
        {
            CreateMap<Commitment, CommitmentDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Template!.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Template!.Description))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline.ToString("g")));
        }
    }
}