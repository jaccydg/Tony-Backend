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
    public class CreateChargingStationCommand : IRequest<ChargingStation>
    {
        public required int Number { get; init; }
        public required int GatewayId { get; init; }
        public required int? UserConnectedId { get; init; }
        public required int? LastLogId { get; init; }
    }

    internal class CreateChargingStationCommandHandler : IRequestHandler<CreateChargingStationCommand, ChargingStation>
    {
        private readonly ApplicationDbContext _context;
        public CreateChargingStationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingStation> Handle(CreateChargingStationCommand request, CancellationToken cancellationToken)
        {

            var chargingStation = new ChargingStation
            {
                Number = request.Number,
                GatewayId = request.GatewayId,
                UserConnectedId = request.UserConnectedId,
                LastLogId = request.LastLogId
            };

            _context.ChargingStations.Add(chargingStation);
            await _context.SaveChangesAsync();

            return await _context.ChargingStations.FindAsync(request.Number, request.GatewayId);
        }
    }
}
