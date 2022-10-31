using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Domain;
using PlatformService.Domain.Models;
using PlatformService.Infrastructure.Db;

namespace PlatformService.Infrastructure.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PlatformAggregate> GetPlatformByIdAsync(int id)
        {
            return await _context.Platforms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreatePlatformAsync(PlatformAggregate plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            await _context.Platforms.AddAsync(plat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesPlatformNameExistAsync(string name)
        {
            return await _context.Platforms.AnyAsync(x => x.Name == name);
        }
    }
}