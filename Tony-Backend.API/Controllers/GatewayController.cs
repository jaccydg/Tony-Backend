using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using GeoCoordinatePortable;
using Tony_Backend.API.Migrations;


namespace Tony_Backend.API.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GatewayController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(GetAllGateways))]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetAllGateways()
        {
            return await _context.Gateways.ToListAsync();
        }

        [HttpGet(nameof(GetGatewayById))]
        public async Task<ActionResult<Gateway>> GetGatewayById(int id)
        {
            var gateway = await _context.Gateways.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            return gateway;
        }


        //TODO: CHIEDERE SE FRONTEND MODIFICA CON TUTTI I CAPI PREIMPOSTATI OPPURE NO
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(string? name, double? latitude, double? longitude)
        {
            if (string.IsNullOrEmpty(name) || latitude == null || longitude == null)
            {
                return BadRequest("Every parameter (name, latitude, longitude) must be provided.");
            }

            var gateway = new Gateway
            {
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            };
            _context.Gateways.Add(gateway);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(int id, string? name, double? longitude, double? latitude)
        {
            if (string.IsNullOrEmpty(name) && latitude == null && longitude == null)
            {
                return BadRequest("At least one parameter (name, latitude, longitude) must be provided for update.");
            }

            var gateway = await _context.Gateways.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            // Update properties if provided values are not null or empty
            if (!string.IsNullOrEmpty(name))
            {
                gateway.Name = name;
            }
            if (latitude != null)
            {
                gateway.Latitude = latitude;
            }
            if (longitude != null)
            {
                gateway.Longitude = longitude;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            var gateway = await _context.Gateways.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateways.Remove(gateway);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
