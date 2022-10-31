using System;
using System.IO;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlatformService.Application.Commands.CreatePlatform;
using PlatformService.Application.Repositories;
using PlatformService.Application.Services;
using PlatformService.Domain;
using PlatformService.Infrastructure.Db;
using PlatformService.Infrastructure.Repositories;
using PlatformService.Infrastructure.Services;
using PlatformService.Infrastructure.Tools;
using PlatformService.Infrastructure.Tools.Behaviors;
using Serilog;

namespace PlatformService
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDbContext(Configuration, _env)
                .AddCustomServices()
                .AddGrpc().Services
                .AddCustomMVC()
                .AddSwagger();

            Console.WriteLine($"--> CommandService Endpoint {Configuration["CommandService"]}");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlerMiddleware));
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GrpcPlatformService>();

                endpoints.MapGet("/src/protos/platforms.proto", async context =>
                {
                    await context.Response.WriteAsync(await File.ReadAllTextAsync("src/Protos/platforms.proto"));
                });
            });
            
            PrepDb.PrepPopulation(app, env.IsProduction());
        }
    }
}

public static class CustomExtensionMethods
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            Console.WriteLine("--> Using SqlServer Db");
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
        }
        else
        {
            Console.WriteLine("--> Using InMem Db");
            services.AddDbContext<AppDbContext>(opt=>
                opt.UseInMemoryDatabase("InMem"));
        }

        return services;
    }
    
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
        });
    }
    
    public static IServiceCollection AddCustomMVC(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreatePlatformCommand).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreatePlatformCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddTransient<IPlatformRepository, PlatformRepository>();
        services.AddTransient<IPlatformQueryRepository, PlatformQueryRepository>();
        services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
        services.AddSingleton<IMessageBusClient, MessageBusClient>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}