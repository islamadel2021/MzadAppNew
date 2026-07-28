using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class MzadCreatedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<MzadCreated>
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<MzadCreated> context)
    {
        await _hubContext.Clients.All.SendAsync("MzadCreated", context.Message);
    }
}
