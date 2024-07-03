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

namespace Tony_Backend.Application.Commands.ChargingStationCommands.CRUD
{
    public class EditChargingStationByNumberCommand : IRequest<ChargingStation>
    {
        public required int Number { get; init; }
        public required Guid GatewayId { get; init; }
        public required ChargingStationStatus? Status { get; init; }
        public required string? UserConnectedId { get; init; }
        public required string? LastLog { get; init; }
    }

    internal class EditChargingStationByNumberCommandHandler : IRequestHandler<EditChargingStationByNumberCommand, ChargingStation>
    {
        private readonly ApplicationDbContext _context;
        public EditChargingStationByNumberCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingStation> Handle(EditChargingStationByNumberCommand request, CancellationToken cancellationToken)
        {
            var chargingStation = await _context.ChargingStations
                                 .Where(cs => cs.Number == request.Number && cs.GatewayId == request.GatewayId)
                                 .FirstOrDefaultAsync(cancellationToken);

            if (chargingStation == null)
            {
                return null;
            }

            // Update properties if provided values are not null or empty
            if (request.Status != null)
            {
                chargingStation.Status = request.Status;
            }
            if (request.UserConnectedId != null)
            {
                chargingStation.UserConnectedId = request.UserConnectedId;
            }
            
            await _context.SaveChangesAsync();

            return chargingStation;
        }
    }
}
