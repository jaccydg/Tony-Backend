using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using Tony_Backend.API.Migrations;
using Tony_Backend.Application.Commands.GatewayCommands;
using System.Reflection.Metadata.Ecma335;

namespace Tony_Backend.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISender _sender;

        public GatewayController(ApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        [HttpGet(nameof(GetAllGateways))]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetAllGateways()
        {
            var gateways = await _sender.Send(new GetAllGatewaysCommand());

            if (gateways == null)
            {
                return NotFound();
            }

            return Ok(gateways);
        }

        [HttpGet(nameof(GetGatewayById))]
        public async Task<ActionResult<Gateway>> GetGatewayById(int id)
        {
            var gateway = await _sender.Send(new GetGatewayByIdCommand() { Id = id });

            if (gateway == null)
            {
                return NotFound();
            }

            return Ok(gateway);
        }

        [HttpPost(nameof(CreateGateway))]
        public async Task<IActionResult> CreateGateway(string? name, double? latitude, double? longitude)
        {
            if (string.IsNullOrEmpty(name) || latitude == null || longitude == null)
            {
                return BadRequest("Every parameter (name, latitude, longitude) must be provided.");
            }

            var gateway = await _sender.Send(new CreateGatewayCommand() { Name = name, Latitude = latitude, Longitude = longitude });

            return Ok(gateway);
        }

        [HttpPut(nameof(UpdateGateway))]
        public async Task<IActionResult> UpdateGateway(int id, string? name, double? longitude, double? latitude)
        {
            if (string.IsNullOrEmpty(name) && latitude == null && longitude == null)
            {
                return BadRequest("At least one parameter (name, latitude, longitude) must be provided for update.");
            }

            var gateway = await _sender.Send(new UpdateGatewayCommand() { Id = id, Name = name, Latitude = latitude, Longitude = longitude });
            
            if (gateway == null)
            {
                return NotFound();
            }
            
            return Ok(gateway);
        }

        [HttpDelete(nameof(DeleteGateway))]
        public async Task<IActionResult> DeleteGateway(int id)
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
    }
}
