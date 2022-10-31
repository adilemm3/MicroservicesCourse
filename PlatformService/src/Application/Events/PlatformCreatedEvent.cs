using MediatR;
using PlatformService.Application.Models;

namespace PlatformService.Application.Events;

public class PlatformCreatedEvent : INotification
{
    public PlatformCreatedEvent(PlatformReadDto platform)
    {
        Platform = platform;
    }

    public PlatformReadDto Platform { get; set; }
}