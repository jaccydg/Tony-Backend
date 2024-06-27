using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tony_Backend.Application.Commands.ChargingSessionCommands.CRUD;
using Tony_Backend.Application.Commands.ChargingSessionCommands;
using System.Security.Claims;


namespace Tony_Backend.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ChargingSessionsController : ControllerBase
    {
        private readonly ISender _sender;
        public ChargingSessionsController (ISender sender)
        {
            _sender = sender;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ChargingSession>>> GetAll()
        {
            return Ok(await _sender.Send(new GetAllChargingSessionCommand()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChargingSession>> GetById([FromRoute] Guid id)
        {
            var chargingSession = await _sender.Send(new GetChargingSessionByIdCommand() {Id = id});
            if (chargingSession == null)
            {
                return NotFound();
            }
            return Ok(chargingSession);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Guid userId, int chargingStationNumber, Guid gatewayId)
        {
            if (userId == null || chargingStationNumber == null || gatewayId == null)
            {
                return BadRequest("At least one parameter (userId, chargingStationNumber, gatewayId) must be provided.");
            }

            var chargingSession = await _sender.Send(new CreateChargingSessionCommand() { UserId = userId, ChargingStationNumber = chargingStationNumber, GatewayId = gatewayId });

            return Ok(chargingSession);
        }


        //[HttpPut("{gatewayId}/{number}/Edit")]
        //public async Task<IActionResult> Edit([FromRoute] int number, [FromRoute] Guid gatewayId, ChargingSessionStatus? status, int? userConnectedId, int? lastLogId)
        //{
        //    if (status == null && userConnectedId == null && lastLogId == null)
        //    {
        //        return BadRequest("At least one parameter (status, userConnectedId, lastLogId) must be provided for edit.");
        //    }

        //    var chargingSession = await _sender.Send(new EditChargingSessionCommand() { Number = number, GatewayId = gatewayId, Status = status, UserConnectedId = userConnectedId, LastLogId = lastLogId });
        //    if (chargingSession == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(chargingSession);
        //}

        //[HttpDelete("{gatewayId}/{number}/Delete")]
        //public async Task<IActionResult> Delete([FromRoute] int number, [FromRoute] Guid gatewayId)
        //{

        //    var found = await _sender.Send(new DeleteChargingSessionCommand() { Number = number, GatewayId = gatewayId });
        //    if (!found)
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}

        //[HttpGet("{gatewayId}/{number}/Check")]
        //public async Task<ActionResult<ChargingSession>> Check([FromRoute] int number, [FromRoute] Guid gatewayId)
        //{
        //    var chargingSession = await _sender.Send(new CheckChargingSessionCommand() { Number = number, GatewayId = gatewayId });
        //    if (chargingSession == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(chargingSession);
        //}

        //[Authorize]
        //[HttpPost("{gatewayId}/{number}/Connect")]
        //public async Task<IActionResult> Connect([FromRoute] int number, [FromRoute] Guid gatewayId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var chargingSession = await _sender.Send(new ConnectChargingSessionCommand() { Number = number, GatewayId = gatewayId, UserId = userId });
        //    if (chargingSession == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(chargingSession);
        //}

    }
}



