using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Tony_Backend.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class ChargingStationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChargingStationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(GetAllChargingStation))]
        public async Task<ActionResult<IEnumerable<ChargingStation>>> GetAllChargingStation()
        {
            return await _context.ChargingStations.ToListAsync();
        }

        [HttpGet(nameof(GetChargingStationById))]
        public async Task<ActionResult<ChargingStation>> GetChargingStationById(int number, int gatewayId)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(number, gatewayId);
            if (chargingStation == null)
            {
                return NotFound();
            }

            return chargingStation;
        }

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(int number, int gatewayId, int? userConnectedId, int? lastLogId)
        {
            if (userConnectedId == null || lastLogId == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLogId) must be provided.");
            }

            var chargingStation = new ChargingStation
            {
                Number = number,
                GatewayId = gatewayId
            };
            _context.ChargingStations.Add(chargingStation);
            await _context.SaveChangesAsync();

            return Ok(chargingStation);
        }


        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(int number, int gatewayId, int? userConnectedId, int? lastLogId)
        {
            if (userConnectedId == null || lastLogId == null)
            {
                return BadRequest("At least one parameter (userConnectedId, lastLogId) must be provided for update.");
            }

            var chargingStation = await _context.ChargingStations.FindAsync(number, gatewayId);
            if (chargingStation == null)
            {
                return NotFound();
            }

            // Update properties if provided values are not null or empty
            if (userConnectedId != null)
            {
                chargingStation.UserConnectedId = userConnectedId;
            }
            if (lastLogId != null)
            {
                chargingStation.LastLogId = lastLogId;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(int number, int gatewayId)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(number, gatewayId);
            if (chargingStation == null)
            {
                return NotFound();
            }

            _context.ChargingStations.Remove(chargingStation);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
