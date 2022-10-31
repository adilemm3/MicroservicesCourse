using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Application.Repositories;

namespace PlatformService.Infrastructure.Services
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformQueryRepository _repository;
        private readonly IMapper _mapper;
        
        public GrpcPlatformService(IMapper mapper, IPlatformQueryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = await _repository.GetPlatformsAsync();

            foreach (var platform in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
            }

            return response;
        }
    }
}