using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Services
{
    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}