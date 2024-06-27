using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.ChargingSessionCommands.CRUD
{
    public class CreateChargingSessionCommand : IRequest<ChargingSession>
    {
        public required Guid UserId { get; init; }
        public required int ChargingStationNumber { get; init; }
        public required Guid GatewayId { get; init; }
    }

    internal class CreateChargingSessionCommandHandler : IRequestHandler<CreateChargingSessionCommand, ChargingSession>
    {
        private readonly ApplicationDbContext _context;
        public CreateChargingSessionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingSession> Handle(CreateChargingSessionCommand request, CancellationToken cancellationToken)
        {

            var chargingSession = new ChargingSession
            {
                Id = Guid.NewGuid(),
                Status = 0,             // Ongoing
                UserId = request.UserId,
                ChargingStationNumber = request.ChargingStationNumber,
                GatewayId = request.GatewayId,
                StartingDate = DateTime.UtcNow,
            };

            _context.ChargingSessions.Add(chargingSession);
            await _context.SaveChangesAsync();

            return await _context.ChargingSessions.FindAsync(chargingSession.Id);
        }
    }
}
