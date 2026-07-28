using AutoMapper;
using Contracts;
using FilterService.Entities;
using MassTransit;
using MongoDB.Entities;

namespace FilterService;

public class MzadUpdatedConsumer(IMapper mapper) : IConsumer<MzadUpdated>
{
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<MzadUpdated> context)
    {
        Console.WriteLine($"==> Consuming MzadUpdated message with id: {context.Message.Id}");
        var mzad = _mapper.Map<Mzad>(context.Message);
        var result = await DB.Update<Mzad>()
        .Match(m => m.ID == context.Message.Id)
        .ModifyOnly(m => new
        {
            m.Name,
            m.Father,
            m.Mother,
            m.Breed,
            m.YearOfBirth,
            m.Color,
            m.ImageUrl,
            m.ReservePrice,
            m.UpdatedAt,
            m.MzadEnd
        }, mzad).ExecuteAsync();

        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(MzadUpdated), "Problem Updating Mzad In Filter Service");
        }
    }
}
