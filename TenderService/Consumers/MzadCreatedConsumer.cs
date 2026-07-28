using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace TenderService;

public class MzadCreatedConsumer : IConsumer<MzadCreated>
{
    public async Task Consume(ConsumeContext<MzadCreated> context)
    {
        var mzad = new Mzad
        {
            ID = context.Message.Id.ToString(),
            Seller = context.Message.Seller,
            MzadEnd = context.Message.MzadEnd,
            ReservePrice = context.Message.ReservePrice
        };
        await mzad.SaveAsync();
    }
}
