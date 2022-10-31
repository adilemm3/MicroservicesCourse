using MediatR;
using PlatformService.Application.Models;

namespace PlatformService.Application.Queries.GetPlatformById;

public class GetPlatformByIdQuery : IRequest<PlatformReadDto>
{
    public GetPlatformByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }

}