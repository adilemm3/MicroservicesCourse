using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlatformService.Application.Models;
using PlatformService.Application.Repositories;
using PlatformService.Infrastructure.Db;

namespace PlatformService.Infrastructure.Repositories;

public class PlatformQueryRepository : IPlatformQueryRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PlatformQueryRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlatformReadDto> GetPlatformByIdAsync(int id)
    {
        var platform = await _context.Platforms.FirstOrDefaultAsync(x => x.Id == id);
        if (platform is null)
            return null;

        return _mapper.Map<PlatformReadDto>(platform);
    }

    public async Task<IEnumerable<PlatformReadDto>> GetPlatformsAsync()
    {
        var platforms = await _context.Platforms.ToListAsync();
        return _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
    }
}