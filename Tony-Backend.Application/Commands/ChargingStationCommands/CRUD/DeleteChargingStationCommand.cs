using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.ChargingStationCommands.CRUD
{
    public class DeleteChargingStationCommand : IRequest<bool>
    {
        public required int Number { get; init; }
        public required int GatewayId { get; init; }
    }

    internal class DeleteChargingStationCommandHandler : IRequestHandler<DeleteChargingStationCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteChargingStationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteChargingStationCommand request, CancellationToken cancellationToken)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(request.Number, request.GatewayId);

            if (chargingStation == null)
            {
                return false;
            }

            _context.ChargingStations.Remove(chargingStation);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
