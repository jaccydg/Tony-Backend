using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.DTO;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.GatewayCommands
{
    public class GetGatewayInfoCommand : IRequest<GatewayInfoDTO>
    {
        public Guid GatewayId { get; set; }
    }

    internal class GetGatewayInfoCommandHandler : IRequestHandler<GetGatewayInfoCommand, GatewayInfoDTO>
    {
        private readonly ApplicationDbContext _context;

        public GetGatewayInfoCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GatewayInfoDTO> Handle(GetGatewayInfoCommand request, CancellationToken cancellationToken)
        {
            var gateway = await _context.Gateways.FindAsync(request.GatewayId);

            if (gateway == null)
            {
                return null;
            }

            var chargingStations = _context.ChargingStations.Where(x => x.GatewayId == request.GatewayId);

            var totalChargingStations = chargingStations.Count();
            var freeChargingStations = chargingStations.Where(x => x.Status == ChargingStationStatus.Free).Count(); 

            var gatewayInfo = new GatewayInfoDTO
            {
                Name = gateway.Name,
                Latitude = gateway.Latitude,
                Longitude = gateway.Longitude,
                FreeChargingStations = freeChargingStations,
                TotalChargingStations = totalChargingStations
            };

            return gatewayInfo;
        }
    }
}
