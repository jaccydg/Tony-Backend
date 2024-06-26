using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Application.Commands.ChargingStationCommands.CRUD;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.ChargingSessionCommands.CRUD
{
    public class GetAllChargingSessionCommand : IRequest<IEnumerable<ChargingSession>> { }

    internal class GetAllChargingSessionCommandHandler : IRequestHandler<GetAllChargingSessionCommand, IEnumerable<ChargingSession>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllChargingSessionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChargingSession>> Handle(GetAllChargingSessionCommand request, CancellationToken cancellationToken)
        {
            return await _context.ChargingSessions.ToListAsync();

        }
    }
}
