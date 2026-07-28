using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class MzadFinishedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<MzadFinished>
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<MzadFinished> context)
    {
        await _hubContext.Clients.All.SendAsync("MzadFinished", context.Message);
    }
}
