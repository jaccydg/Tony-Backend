using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using Tony_Backend.API.Migrations;
using Tony_Backend.Application.Commands.GatewayCommands.CRUD;
using Tony_Backend.Application.Commands.GatewayCommands;
using System.Reflection.Metadata.Ecma335;
using Tony_Backend.Shared.DTO;

namespace Tony_Backend.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class GatewaysController : ControllerBase
    {
        private readonly ISender _sender;

        public GatewaysController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetAll()
        {
            var gateways = await _sender.Send(new GetAllGatewaysCommand());

            if (gateways == null)
            {
                return NotFound();
            }

            return Ok(gateways);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gateway>> GetById([FromRoute] int id)
        {
            var gateway = await _sender.Send(new GetGatewayByIdCommand() { Id = id });

            if (gateway == null)
            {
                return NotFound();
            }

            return Ok(gateway);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string? name, double? latitude, double? longitude)
        {
            if (string.IsNullOrEmpty(name) || latitude == null || longitude == null)
            {
                return BadRequest("Every parameter (name, latitude, longitude) must be provided.");
            }

            var gateway = await _sender.Send(new CreateGatewayCommand() { Name = name, Latitude = latitude, Longitude = longitude });

            return Ok(gateway);
        }

        [HttpPut("{id}/Edit")]
        public async Task<IActionResult> Edit([FromRoute] int id, string? name, double? longitude, double? latitude)
        {
            if (string.IsNullOrEmpty(name) && latitude == null && longitude == null)
            {
                return BadRequest("At least one parameter (name, latitude, longitude) must be provided for update.");
            }

            var gateway = await _sender.Send(new EditGatewayCommand() { Id = id, Name = name, Latitude = latitude, Longitude = longitude });

            if (gateway == null)
            {
                return NotFound();
            }

            return Ok(gateway);
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // TODO: 
            // questo è un accrocchio, sarebbe bello usare la gestione degli errori

            var found = await _sender.Send(new DeleteGatewayCommand() { Id = id });
            if (!found)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{id}/GetGatewayInfo")]
        public async Task<ActionResult<GatewayInfoDTO>> GetGatewayInfo([FromRoute] int id)
        {
            var gateways = await _sender.Send(new GetGatewayInfoCommand() { GatewayId = id });

            if (gateways == null)
            {
                return NotFound();
            }

            return Ok(gateways);
        }
    }
}
