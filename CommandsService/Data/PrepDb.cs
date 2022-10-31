using System;
using System.Collections.Generic;
using CommandsService.Models;
using CommandsService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpcClient.ReturnAllPlatforms();
                
                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepository>(), platforms);
                
            }
        }

        private static void SeedData(ICommandRepository repository, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platforms...");

            foreach (var platform in platforms)
            {
                if (!repository.IsExternalPlatformExists(platform.ExternalId))
                {
                    repository.CreatePlatform(platform);
                    Console.WriteLine($"{platform.Id} {platform.Name} {platform.ExternalId}");
                }

                repository.SaveChanges();
            }
        }
    }
}