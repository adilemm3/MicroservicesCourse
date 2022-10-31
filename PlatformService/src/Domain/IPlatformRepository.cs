using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformService.Domain.Models;

namespace PlatformService.Domain;

public interface IPlatformRepository
{
    Task<PlatformAggregate> GetPlatformByIdAsync(int id);
    Task CreatePlatformAsync(PlatformAggregate plat);
    Task<bool> DoesPlatformNameExistAsync(string name);
}