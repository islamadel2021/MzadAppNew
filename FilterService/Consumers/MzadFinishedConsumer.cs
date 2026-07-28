using Contracts;
using FilterService.Entities;
using MassTransit;
using MongoDB.Entities;

namespace FilterService;

public class MzadFinishedConsumer : IConsumer<MzadFinished>
{
    public async Task Consume(ConsumeContext<MzadFinished> context)
    {
        Console.WriteLine($"==>Consuming MzadFinished message with id:{context.Message.MzadId}");

        var mzad = await DB.Find<Mzad>().OneAsync(context.Message.MzadId);

        if (context.Message.HorseSold)
        {
            mzad.Winner = context.Message.Winner;
            mzad.SoldAmount = context.Message.Amount;
        }

        mzad.Status = "Finished";

        await mzad.SaveAsync();
    }
}
