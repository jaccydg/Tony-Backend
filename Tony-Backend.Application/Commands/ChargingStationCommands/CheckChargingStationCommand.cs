using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tony_Backend.Application.Commands.ChargingStationCommands
{
    public class CheckChargingStationCommand : IRequest<bool?>
    {
        public required int Number { get; init; }
        public required Guid GatewayId { get; init; }
    }

    internal class CheckChargingStationByIdCommandHandler : IRequestHandler<CheckChargingStationCommand, bool?>
    {
        private readonly ApplicationDbContext _context;
        public CheckChargingStationByIdCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool?> Handle(CheckChargingStationCommand request, CancellationToken cancellationToken)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(request.Number, request.GatewayId);

            if (chargingStation == null)
            {
                return null;
            }

            if (chargingStation.Status != ChargingStationStatus.Free )
            {
                return false;
            }

            return true;
        }
    }
}
