using Contracts;
using MassTransit;
using MzadService.Data;

namespace MzadService;

public class TenderPlacedConsumer(MzadDbContext context) : IConsumer<TenderPlaced>
{
    private readonly MzadDbContext _context = context;

    public async Task Consume(ConsumeContext<TenderPlaced> context)
    {
        Console.WriteLine($"==>Consuming TenderPlaced message For Mzad with id:{context.Message.MzadId}");

        var mzad = await _context.Mzadat.FindAsync(Guid.Parse(context.Message.MzadId));

        if (mzad.CurrentHighTender == null
            || context.Message.TenderStatus.Contains("Accepted")
            && context.Message.Amount > mzad.CurrentHighTender)
        {
            mzad.CurrentHighTender = context.Message.Amount;
            await _context.SaveChangesAsync();
        }
    }
}
