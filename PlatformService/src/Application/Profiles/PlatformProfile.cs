using AutoMapper;
using PlatformService.Application.Commands.CreatePlatform;
using PlatformService.Application.Models;
using PlatformService.Domain.Models;

namespace PlatformService.Application.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<PlatformAggregate, PlatformReadDto>();
            CreateMap<PlatformCreateDto, PlatformAggregate>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
            CreateMap<PlatformReadDto, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}