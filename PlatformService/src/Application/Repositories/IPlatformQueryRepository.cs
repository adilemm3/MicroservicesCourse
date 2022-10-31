using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformService.Application.Models;

namespace PlatformService.Application.Repositories;

public interface IPlatformQueryRepository
{
    Task<IEnumerable<PlatformReadDto>> GetPlatformsAsync();
    Task<PlatformReadDto> GetPlatformByIdAsync(int id);
}