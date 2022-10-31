using PlatformService.Application.Models;

namespace PlatformService.Application.Services
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}