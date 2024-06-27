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
    public class GetChargingStationByIdCommand : IRequest<ChargingStation>
    {
        public required Guid Id { get; init; }
    }

    internal class GetChargingStationByIdCommandHandler : IRequestHandler<GetChargingStationByIdCommand, ChargingStation>
    {
        private readonly ApplicationDbContext _context;
        public GetChargingStationByIdCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingStation> Handle(GetChargingStationByIdCommand request, CancellationToken cancellationToken)
        {
            return await _context.ChargingStations.FindAsync(request.Id);
        }
    }
}
