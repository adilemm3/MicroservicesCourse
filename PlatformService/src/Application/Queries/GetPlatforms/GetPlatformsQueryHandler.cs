using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlatformService.Application.Models;
using PlatformService.Application.Repositories;

namespace PlatformService.Application.Queries.GetPlatforms;

public class GetPlatformsQueryHandler : IRequestHandler<GetPlatformsQuery, IEnumerable<PlatformReadDto>>
{
    private readonly IPlatformQueryRepository _repository;
    public GetPlatformsQueryHandler(IPlatformQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<PlatformReadDto>> Handle(GetPlatformsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetPlatformsAsync();
    }
}