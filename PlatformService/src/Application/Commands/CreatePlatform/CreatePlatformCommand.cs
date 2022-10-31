using MediatR;
using PlatformService.Application.Models;

namespace PlatformService.Application.Commands.CreatePlatform;

public class CreatePlatformCommand : IRequest<PlatformReadDto>
{
    public PlatformCreateDto Platform { get; set; }
}

