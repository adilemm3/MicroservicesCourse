using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlatformService.Application.Services;
using PlatformService.Application.Events;

namespace PlatformService.Application.EventHandlers;

public class PlatformCreatedNotifyByHttpEventHandler : INotificationHandler<PlatformCreatedEvent>
{
    private readonly ICommandDataClient _commandDataClient;
    
    public PlatformCreatedNotifyByHttpEventHandler(ICommandDataClient commandDataClient)
    {
        _commandDataClient = commandDataClient;
    }
    public async Task Handle(PlatformCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _commandDataClient.SendPlatformToCommandAsync(notification.Platform);

        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not send synchronously: {e.Message}");
        }
    }
}