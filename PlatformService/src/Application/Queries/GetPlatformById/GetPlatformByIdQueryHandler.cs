using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlatformService.Application.Models;
using PlatformService.Application.Repositories;

namespace PlatformService.Application.Queries.GetPlatformById;

public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, PlatformReadDto>
{
    private readonly IPlatformQueryRepository _repository;
    public GetPlatformByIdQueryHandler(IPlatformQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<PlatformReadDto> Handle(GetPlatformByIdQuery command, CancellationToken cancellationToken)
    {
        var platform = await _repository.GetPlatformByIdAsync(command.Id);
        if (platform is null)
        {
            throw new KeyNotFoundException($"Platform not found with id: {command.Id}");
        }

        return platform;
    }
}