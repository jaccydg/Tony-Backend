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
    public class GetChargingSessionByIdCommand : IRequest<ChargingSession>
    {
        public Guid Id { get; set; }
    }

    internal class GetChargingSessionByIdCommandHendler : IRequestHandler<GetChargingSessionByIdCommand,  ChargingSession> 
    {
        private readonly  ApplicationDbContext _context;

        public GetChargingSessionByIdCommandHendler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChargingSession> Handle(GetChargingSessionByIdCommand request, CancellationToken cancellationToken)
        {
            return await _context.ChargingSessions.FindAsync(request.Id);
        }
    }
}
