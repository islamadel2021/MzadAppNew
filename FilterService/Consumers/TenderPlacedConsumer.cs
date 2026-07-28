using Contracts;
using FilterService.Entities;
using MassTransit;
using MongoDB.Entities;

namespace FilterService;

public class TenderPlacedConsumer : IConsumer<TenderPlaced>
{
    public async Task Consume(ConsumeContext<TenderPlaced> context)
    {
        Console.WriteLine($"==>Consuming TenderPlaced message For Mzad with id:{context.Message.MzadId}");

        var mzad = await DB.Find<Mzad>().OneAsync(context.Message.MzadId);

        if (context.Message.TenderStatus.Contains("Accepted")
            && context.Message.Amount > mzad.CurrentHighTender)
        {
            mzad.CurrentHighTender = context.Message.Amount;
            await mzad.SaveAsync();
        }
    }
}
