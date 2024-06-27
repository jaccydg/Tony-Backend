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
    public class GetChargingStationByNumberCommand : IRequest<ChargingStation>
    {
        public required int Number { get; init; }
        public required Guid GatewayId { get; init; }
    }

    internal class GetChargingStationByNumberCommandHandler : IRequestHandler<GetChargingStationByNumberCommand, ChargingStation>
    {
        private readonly ApplicationDbContext _context;
        public GetChargingStationByNumberCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingStation> Handle(GetChargingStationByNumberCommand request, CancellationToken cancellationToken)
        {
            return await _context.ChargingStations
                   .Where(cs => cs.Number == request.Number && cs.GatewayId == request.GatewayId)
                   .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
