using AutoMapper;
using Contracts;
using MzadService.DTOs;
using MzadService.Entities;

namespace MzadService.RequestHelpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Mzad, MzadDTO>().IncludeMembers(m => m.Horse);
        CreateMap<Horse, MzadDTO>();
        CreateMap<MzadForCreateDTO, Mzad>()
        .ForMember(m => m.Horse, o => o.MapFrom(m => m));
        CreateMap<MzadForCreateDTO, Horse>();
        CreateMap<MzadDTO, MzadForCreateDTO>();
        CreateMap<Mzad, MzadForUpdateDTO>().IncludeMembers(m => m.Horse).ReverseMap();
        CreateMap<Horse, MzadForUpdateDTO>().ReverseMap();
        CreateMap<MzadDTO, MzadCreated>();
        CreateMap<Mzad, MzadUpdated>().IncludeMembers(m => m.Horse);
        CreateMap<Horse, MzadUpdated>();
    }
}
