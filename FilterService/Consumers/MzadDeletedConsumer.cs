using Contracts;
using FilterService.Entities;
using MassTransit;
using MongoDB.Entities;

namespace FilterService;

public class MzadDeletedConsumer : IConsumer<MzadDeleted>
{
    public async Task Consume(ConsumeContext<MzadDeleted> context)
    {
        Console.WriteLine($"==> Consuming MzadDeleted message with id: {context.Message.Id}");
        var result = await DB.DeleteAsync<Mzad>(context.Message.Id);
        if (!result.IsAcknowledged) throw new MessageException(typeof(MzadDeleted), "Problem Deleting Mzad In Filter Service");
    }
}
