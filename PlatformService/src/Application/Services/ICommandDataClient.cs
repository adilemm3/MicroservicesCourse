using System.Threading.Tasks;
using PlatformService.Application.Models;

namespace PlatformService.Application.Services
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommandAsync(PlatformReadDto platform);
    }
}

