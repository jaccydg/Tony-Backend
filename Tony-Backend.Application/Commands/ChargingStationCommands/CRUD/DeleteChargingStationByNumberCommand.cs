using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class DeleteChargingStationByNumberCommand : IRequest<bool>
    {
        public required int Number { get; init; }
        public required Guid GatewayId { get; init; }
    }

    internal class DeleteChargingStationByNumberCommandHandler : IRequestHandler<DeleteChargingStationByNumberCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteChargingStationByNumberCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteChargingStationByNumberCommand request, CancellationToken cancellationToken)
        {
            var chargingStation = await _context.ChargingStations
                                 .Where(cs => cs.Number == request.Number && cs.GatewayId == request.GatewayId)
                                 .FirstOrDefaultAsync(cancellationToken);
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
