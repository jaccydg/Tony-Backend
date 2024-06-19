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
    public class UpdateChargingStationCommand : IRequest<ChargingStation>
    {
        public required int Number { get; init; }
        public required int GatewayId { get; init; }
        public required int? UserConnectedId { get; init; }
        public required int? LastLogId { get; init; }
    }

    internal class UpdateChargingStationCommandHandler : IRequestHandler<UpdateChargingStationCommand, ChargingStation>
    {
        private readonly ApplicationDbContext _context;
        public UpdateChargingStationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingStation> Handle(UpdateChargingStationCommand request, CancellationToken cancellationToken)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(request.Number, request.GatewayId);

            if (chargingStation == null)
            {
                return null;
            }

            // Update properties if provided values are not null or empty
            if (request.UserConnectedId != null)
            {
                chargingStation.UserConnectedId = request.UserConnectedId;
            }

            if (request.LastLogId != null)
            {
                chargingStation.LastLogId = request.LastLogId;
            }

            return chargingStation;

        }
    }
}
