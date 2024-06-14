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
    public class ChargingStationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISender _sender;

        public ChargingStationController(ApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        [HttpGet(nameof(GetAllChargingStation))]
        public async Task<ActionResult<IEnumerable<ChargingStation>>> GetAllChargingStation()
        {
            //return await _context.ChargingStations.ToListAsync();
            return Ok(await _sender.Send(new GetAllChargingStationCommand()));
        }

        [HttpGet(nameof(GetChargingStationById))]
        public async Task<ActionResult<ChargingStation>> GetChargingStationById(int number, int gatewayId)
        {
            var chargingStation = await _sender.Send(new GetChargingStationByIdCommand() { Number = number, GatewayId = gatewayId});
            if (chargingStation == null)
            {
                return NotFound();
            }
            return Ok(chargingStation);
        }

        [HttpPost(nameof(CreateChargingStation))]
        public async Task<IActionResult> CreateChargingStation(int number, int gatewayId, int? userConnectedId, int? lastLogId)
        {
            if (userConnectedId == null || lastLogId == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLogId) must be provided.");
            }

            var chargingStation = await _sender.Send(new CreateChargingStationCommand() { Number = number, GatewayId = gatewayId, UserConnectedId = userConnectedId, LastLogId = lastLogId });

            return Ok(chargingStation);
        }


        [HttpPut(nameof(UpdateChargingStation))]
        public async Task<IActionResult> UpdateChargingStation(int number, int gatewayId, int? userConnectedId, int? lastLogId)
        {
            if (userConnectedId == null || lastLogId == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLogId) must be provided for update.");
            }

            var chargingStation = await _sender.Send(new UpdateChargingStationCommand() { Number = number, GatewayId = gatewayId, UserConnectedId = userConnectedId, LastLogId = lastLogId });
            if (chargingStation == null)
            {
                return NotFound();
            }

            return Ok(chargingStation);
        }

        [HttpDelete(nameof(DeleteChargingStation))]
        public async Task<IActionResult> DeleteChargingStation(int number, int gatewayId)
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
    }
}
