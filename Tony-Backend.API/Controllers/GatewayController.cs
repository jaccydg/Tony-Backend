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
    public class GatewayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GatewayController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(Gateway))]
        [HttpGet(Name = "Create Gateway")]
        public async Task Create(string Name, double latitude, double longitude)
        {
            var gateway = new Gateway
            {
                Name = Name,
                Location = new GeoCoordinate(longitude, latitude)
            };
            _context.Gateways.Add(gateway);
            await _context.SaveChangesAsync();
        }
    }
}
