using AutoMapper;
using Contracts;
using FilterService.Entities;

namespace FilterService.RequestHelpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MzadCreated, Mzad>();
        CreateMap<MzadUpdated, Mzad>();
    }
}
