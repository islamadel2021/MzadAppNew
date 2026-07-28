
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace TenderService;

public class CheckMzadFinished(ILogger<CheckMzadFinished> logger, IServiceProvider services) : BackgroundService
{
    private readonly ILogger<CheckMzadFinished> _logger = logger;
    private readonly IServiceProvider _services = services;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting check for finished Mzadat");

        stoppingToken.Register(() => _logger.LogInformation("==> Mzad check is stopping"));

        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckMzadat(stoppingToken);

            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task CheckMzadat(CancellationToken stoppingToken)
    {
        var finishedMzadat = await DB.Find<Mzad>()
            .Match(m => m.MzadEnd <= DateTime.UtcNow)
            .Match(m => !m.Finished)
            .ExecuteAsync(stoppingToken);

        if (finishedMzadat.Count == 0) return;

        _logger.LogInformation("==> Found {count} mzad that have completed", finishedMzadat.Count);

        using var scope = _services.CreateScope();
        var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        foreach (var Mzad in finishedMzadat)
        {
            Mzad.Finished = true;
            await Mzad.SaveAsync(null, stoppingToken);

            var winningTender = await DB.Find<Tender>()
                .Match(t => t.MzadId == Mzad.ID)
                .Match(b => b.TenderStatus == TenderStatus.Accepted)
                .Sort(x => x.Descending(s => s.Amount))
                .ExecuteFirstAsync(stoppingToken);

            await endpoint.Publish(new MzadFinished
            {
                HorseSold = winningTender != null,
                MzadId = Mzad.ID,
                Winner = winningTender?.TenderOwner,
                Amount = winningTender?.Amount ?? 0,
                Seller = Mzad.Seller
            }, stoppingToken);
        }
    }
}
