using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using GeoCoordinatePortable;


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

        [HttpPost(nameof(Create))]
        public async Task Create(int Number, int GatewayId)
        {
            var chargingStation = new ChargingStation
            {
                Number = Number,
                GatewayId = GatewayId
            };
            _context.ChargingStations.Add(chargingStation);
            await _context.SaveChangesAsync();
        }
    }
}
