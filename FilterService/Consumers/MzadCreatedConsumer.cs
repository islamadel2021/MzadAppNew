using AutoMapper;
using Contracts;
using FilterService.Entities;
using MassTransit;
using MongoDB.Entities;

namespace FilterService;

public class MzadCreatedConsumer(IMapper mapper) : IConsumer<MzadCreated>
{
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<MzadCreated> context)
    {
        Console.WriteLine($"==> Consuming MzadCreated message with id: {context.Message.Id}");
        var mzad = _mapper.Map<Mzad>(context.Message);
        if (mzad.Name == "Israel") throw new ArgumentException("Israel not Accepted");
        await mzad.SaveAsync();
    }
}
