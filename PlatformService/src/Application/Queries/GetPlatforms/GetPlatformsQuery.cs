using System.Collections.Generic;
using MediatR;
using PlatformService.Application.Models;

namespace PlatformService.Application.Queries.GetPlatforms;

public class GetPlatformsQuery : IRequest<IEnumerable<PlatformReadDto>>
{

}