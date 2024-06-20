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

namespace Tony_Backend.Application.Commands.GatewayCommands.CRUD
{
    public class GetAllGatewaysCommand : IRequest<IEnumerable<Gateway>> { }

    internal class GetAllGatewaysCommandHandler : IRequestHandler<GetAllGatewaysCommand, IEnumerable<Gateway>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllGatewaysCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gateway>> Handle(GetAllGatewaysCommand request, CancellationToken cancellationToken)
        {
            return await _context.Gateways.ToListAsync();
        }
    }
}
