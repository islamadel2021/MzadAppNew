using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MzadService.Data;

namespace MzadService;

public class MzadCreatedFaultConsumer(MzadDbContext context) : IConsumer<Fault<MzadCreated>>
{
    private readonly MzadDbContext _context = context;

    public async Task Consume(ConsumeContext<Fault<MzadCreated>> context)
    {
        var exception = context.Message.Exceptions.FirstOrDefault();
        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Name = "Palestine";
            await context.Publish(context.Message.Message);

            var mzad = await _context.Mzadat.Include(m => m.Horse).FirstOrDefaultAsync(m => m.Id == context.Message.Message.Id);
            mzad.Horse.Name = "Palestine";
            await _context.SaveChangesAsync();
        }
    }
}
