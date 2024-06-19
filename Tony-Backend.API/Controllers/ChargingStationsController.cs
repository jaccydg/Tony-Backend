using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tony_Backend.Application.Commands.ChargingStationCommands;


namespace Tony_Backend.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class ChargingStationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISender _sender;

        public ChargingStationsController(ApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ChargingStation>>> GetAll()
        {
            //return await _context.ChargingStations.ToListAsync();
            return Ok(await _sender.Send(new GetAllChargingStationCommand()));
        }

        [HttpGet("{gatewayId}/{number}")]
        public async Task<ActionResult<ChargingStation>> GetById([FromRoute] int number, [FromRoute] int gatewayId)
        {
            var chargingStation = await _sender.Send(new GetChargingStationByIdCommand() { Number = number, GatewayId = gatewayId});
            if (chargingStation == null)
            {
                return NotFound();
            }
            return Ok(chargingStation);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(int number, int gatewayId, ChargingStationStatus status, int? userConnectedId, int? lastLogId)
        {
            if (userConnectedId == null || lastLogId == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLogId) must be provided.");
            }

            var chargingStation = await _sender.Send(new CreateChargingStationCommand() { Number = number, GatewayId = gatewayId, Status = status, UserConnectedId = userConnectedId, LastLogId = lastLogId });

            return Ok(chargingStation);
        }


        [HttpPut("{gatewayId}/{number}/Edit")]
        public async Task<IActionResult> Edit(int number, int gatewayId, ChargingStationStatus status, int? userConnectedId, int? lastLogId)
        {
            if (userConnectedId == null || lastLogId == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLogId) must be provided for edit.");
            }

            var chargingStation = await _sender.Send(new EditChargingStationCommand() { Number = number, GatewayId = gatewayId, Status = status, UserConnectedId = userConnectedId, LastLogId = lastLogId });
            if (chargingStation == null)
            {
                return NotFound();
            }

            return Ok(chargingStation);
        }

        [HttpDelete("{gatewayId}/{number}/Delete")]
        public async Task<IActionResult> Delete(int number, int gatewayId)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(number, gatewayId);
            if (chargingStation == null)
            {
                return NotFound();
            }

            //_context.ChargingStations.Remove(chargingStation);
            await _sender.Send(new DeleteChargingStationCommand() { Number = number, GatewayId = gatewayId });

            return Ok();
        }

        [HttpGet("{tewayId}/{number}/Check")]
        public async Task<ActionResult<ChargingStation>> Check([FromRoute] int number, [FromRoute] int gatewayId)
        {
            var chargingStation = await _sender.Send(new CheckChargingStationCommand() { Number = number, GatewayId = gatewayId });
            if (chargingStation == null)
            {
                return NotFound();
            }
            return Ok(chargingStation);
        }
    }
}
