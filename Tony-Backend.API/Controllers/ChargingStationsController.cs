using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tony_Backend.Application.Commands.ChargingStationCommands.CRUD;
using Tony_Backend.Application.Commands.ChargingStationCommands;
using System.Security.Claims;
using Tony_Backend.Shared.Helpers;


namespace Tony_Backend.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class ChargingStationsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IQueueOperations _queueOperations;


        public ChargingStationsController(ISender sender, IQueueOperations queueOperations)
        {
            _sender = sender;
            _queueOperations = queueOperations;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ChargingStation>>> GetAll()
        {
            return Ok(await _sender.Send(new GetAllChargingStationCommand()));
        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<ChargingStation>> GetById([FromRoute] Guid id)
        {
            var chargingStation = await _sender.Send(new GetChargingStationByIdCommand() {Id = id});
            if (chargingStation == null)
            {
                return NotFound();
            }
            return Ok(chargingStation);
        }

        [HttpGet("{number}/{gatewayId}/GetByNumber")]
        public async Task<ActionResult<ChargingStation>> GetByNumber([FromRoute] int number, [FromRoute] Guid gatewayId)
        {
            var chargingStation = await _sender.Send(new GetChargingStationByNumberCommand() { Number = number, GatewayId = gatewayId});
            if (chargingStation == null)
            {
                return NotFound();
            }
            return Ok(chargingStation);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(int number, Guid gatewayId, ChargingStationStatus status, string? userConnectedId, string? lastLog)
        {
            if (userConnectedId == null || lastLog == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLog) must be provided.");
            }

            var chargingStation = await _sender.Send(new CreateChargingStationCommand() { Number = number, GatewayId = gatewayId, Status = status, UserConnectedId = userConnectedId, LastLog = lastLog });

            return Ok(chargingStation);
        }

        [HttpPut("{id}/EditById")]
        public async Task<IActionResult> EditById([FromRoute] Guid id, int? number, Guid? gatewayId, ChargingStationStatus? status, string? userConnectedId, string? lastLog)
        {

            var chargingStation = await _sender.Send(new EditChargingStationByIdCommand() { Id = id, Number = number, GatewayId = gatewayId, Status = status, UserConnectedId = userConnectedId, LastLog = lastLog });
            if (chargingStation == null)
            {
                return NotFound();
            }

            return Ok(chargingStation);
        }

        [HttpPut("{number}/{gatewayId}/EditByNumber")]
        public async Task<IActionResult> Edit(Guid? id, [FromRoute] int number, [FromRoute] Guid gatewayId, ChargingStationStatus? status, string? userConnectedId, string? lastLog)
        {
            var chargingStation = await _sender.Send(new EditChargingStationByNumberCommand() { Number = number, GatewayId = gatewayId, Status = status, UserConnectedId = userConnectedId, LastLog = lastLog });
            if (chargingStation == null)
            {
                return NotFound();
            }

            return Ok(chargingStation);
        }

        [HttpDelete("{id}/DeleteById")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var found = await _sender.Send(new DeleteChargingStationByIdCommand() { Id = id});
            if (!found)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpDelete("{number}/{gatewayId}/DeleteByNumber")]
        public async Task<IActionResult> Delete([FromRoute] int number, [FromRoute] Guid gatewayId)
        {

            var found = await _sender.Send(new DeleteChargingStationByNumberCommand() { Number = number, GatewayId = gatewayId});
            if (!found)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("{number}/{gatewayId}/Check")]
        public async Task<ActionResult<ChargingStation>> Check([FromRoute] int number, [FromRoute] Guid gatewayId)
        {
            var chargingStation = await _sender.Send(new CheckChargingStationCommand() { Number = number, GatewayId = gatewayId });
            if (chargingStation == null)
            {
                return NotFound();
            }
            return Ok(chargingStation);
        }

        [Authorize]
        [HttpPost("{number}/{gatewayId}/Connect")]
        public async Task<IActionResult> Connect([FromRoute] int number, [FromRoute] Guid gatewayId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chargingStation = await _sender.Send(new ConnectChargingStationCommand() { Number = number, GatewayId = gatewayId, UserId = userId });
            if (chargingStation == null)
            {
                return NotFound();
            }

            return Ok(chargingStation);
        }
    }
}
