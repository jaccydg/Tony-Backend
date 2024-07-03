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
    public class DeleteChargingStationByIdCommand : IRequest<bool>
    {
        public required Guid Id { get; init; }
    }

    internal class DeleteChargingStationByIdCommandHandler : IRequestHandler<DeleteChargingStationByIdCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteChargingStationByIdCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteChargingStationByIdCommand request, CancellationToken cancellationToken)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(request.Id);

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
