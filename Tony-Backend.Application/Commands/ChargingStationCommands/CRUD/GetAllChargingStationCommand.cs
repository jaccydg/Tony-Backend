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
    public class GetAllChargingStationCommand : IRequest<IEnumerable<ChargingStation>> { }


    internal class GetAllChargingStationCommandHandler : IRequestHandler<GetAllChargingStationCommand, IEnumerable<ChargingStation>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllChargingStationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChargingStation>> Handle(GetAllChargingStationCommand request, CancellationToken cancellationToken)
        {
            return await _context.ChargingStations.ToListAsync();

        }
    }
}

