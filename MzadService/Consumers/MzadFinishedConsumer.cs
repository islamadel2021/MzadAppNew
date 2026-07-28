using Contracts;
using MassTransit;
using MzadService.Data;
using MzadService.Entities;

namespace MzadService;

public class MzadFinishedConsumer(MzadDbContext context) : IConsumer<MzadFinished>
{
    private readonly MzadDbContext _context = context;

    public async Task Consume(ConsumeContext<MzadFinished> context)
    {
        Console.WriteLine($"==>Consuming MzadFinished message with id:{context.Message.MzadId}");
        var mzad = await _context.Mzadat.FindAsync(Guid.Parse(context.Message.MzadId));

        if (context.Message.HorseSold)
        {
            mzad.Winner = context.Message.Winner;
            mzad.SoldAmount = context.Message.Amount;
        }

        mzad.Status = mzad.SoldAmount > mzad.ReservePrice
            ? Status.Finished : Status.ReserveNotMet;

        await _context.SaveChangesAsync();

    }
}
