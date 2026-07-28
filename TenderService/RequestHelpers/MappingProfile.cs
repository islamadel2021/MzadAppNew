using AutoMapper;
using Contracts;

namespace TenderService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tender, TenderDTO>();
        CreateMap<Tender, TenderPlaced>();
    }
}
