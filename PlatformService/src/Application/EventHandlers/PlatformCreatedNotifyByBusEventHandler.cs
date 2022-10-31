using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PlatformService.Application.Services;
using PlatformService.Application.Events;
using PlatformService.Application.Models;

namespace PlatformService.Application.EventHandlers;

public class PlatformCreatedNotifyByBusEventHandler : INotificationHandler<PlatformCreatedEvent>
{
    private readonly IMessageBusClient _messageBusClient;
    private readonly IMapper _mapper;

    public PlatformCreatedNotifyByBusEventHandler(IMessageBusClient messageBusClient, IMapper mapper)
    {
        _messageBusClient = messageBusClient;
        _mapper = mapper;
    }
    public Task Handle(PlatformCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(notification.Platform);
            platformPublishedDto.Event = "Platform_Published";

            _messageBusClient.PublishNewPlatform(platformPublishedDto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not send asynchronously: {e.Message}");
        }
        return Task.CompletedTask;
    }
}