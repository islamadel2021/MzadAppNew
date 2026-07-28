using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class TenderPlacedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<TenderPlaced>
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<TenderPlaced> context)
    {
        await _hubContext.Clients.All.SendAsync("TenderPlaced", context.Message);
    }
}
