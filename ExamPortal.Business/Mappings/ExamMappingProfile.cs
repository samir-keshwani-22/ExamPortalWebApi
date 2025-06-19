using AutoMapper;
using ExamPortal.API.Models;

namespace ExamPortal.Business.Mappings;

public class ExamMappingProfile : Profile

{
    public ExamMappingProfile()
    {
        CreateMap<ExamCreate, API.Models.Entities.Exam>()
            .ForMember(dest => dest.Questions, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartDate, DateTimeKind.Utc)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndDate, DateTimeKind.Utc)));

        CreateMap<API.Models.Entities.Exam, Exam>()
           .ForMember(dest => dest.DurationMinutes,
           opt => opt.MapFrom(src => (int)src.DurationMinutes.TotalMinutes));
        CreateMap<ExamUpdate, API.Models.Entities.Exam>()
      .ForMember(
          dest => dest.DurationMinutes,
          opt => opt.MapFrom(src => src.DurationMinutes.HasValue
              ? TimeSpan.FromMinutes(src.DurationMinutes.Value)
              : (TimeSpan?)null))
      .ForMember(
          dest => dest.StartDate,
          opt => opt.Condition(src => src.StartDate != null))
      .ForMember(
          dest => dest.EndDate,
          opt => opt.Condition(src => src.EndDate != null))
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
