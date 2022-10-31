using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Application.Commands.CreatePlatform;
using PlatformService.Application.Models;
using PlatformService.Application.Queries.GetPlatformById;
using PlatformService.Application.Queries.GetPlatforms;

namespace PlatformService.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlatformsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlatformReadDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms.....");
            
            return Ok(await _mediator.Send(new GetPlatformsQuery()));
        }
        
        [HttpGet("{id:int}", Name= "GetPlatformById")]
        [ProducesResponseType(typeof(PlatformReadDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            Console.WriteLine("--> Getting Platform.....");
            return Ok(await _mediator.Send(new GetPlatformByIdQuery(id)));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto createDto)
        {
            Console.WriteLine("--> Create Platform.....");
            var platformReadDto = await _mediator.Send(new CreatePlatformCommand()
            {
                Platform = createDto
            });

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}