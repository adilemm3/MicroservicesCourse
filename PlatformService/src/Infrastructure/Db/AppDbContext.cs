using Microsoft.EntityFrameworkCore;
using PlatformService.Domain.Models;

namespace PlatformService.Infrastructure.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<PlatformAggregate> Platforms { get; set; }
    }
}