using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PlatformService.Application.Events;
using PlatformService.Application.Models;
using PlatformService.Domain;
using PlatformService.Domain.Exceptions;
using PlatformService.Domain.Models;

namespace PlatformService.Application.Commands.CreatePlatform;

public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, PlatformReadDto>
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public CreatePlatformCommandHandler(IPlatformRepository repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<PlatformReadDto> Handle(CreatePlatformCommand command, CancellationToken cancellationToken)
    {
        var platform = await _repository.GetPlatformByIdAsync(command.Platform.Id);
        if (platform != null)
            throw new DomainException($"Platform already exist with id: {command.Platform.Id}");

        if (await _repository.DoesPlatformNameExistAsync(command.Platform.Name))
            throw new DomainException($"Platform already exist with name: {command.Platform.Name}");
        
        var platformAggregate = _mapper.Map<PlatformAggregate>(command.Platform);
        await _repository.CreatePlatformAsync(platformAggregate);
        
        var platformReadDto = _mapper.Map<PlatformReadDto>(platformAggregate);

        await _mediator.Publish(new PlatformCreatedEvent(platformReadDto), cancellationToken);

        return platformReadDto;
    }
}