using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.GatewayCommands
{
    public class GetGatewayByIdCommand : IRequest<Gateway>
    {
        public required int Id { get; init; }
    }

    internal class GetGatewayByIdCommandHandler : IRequestHandler<GetGatewayByIdCommand, Gateway>
    {
        private readonly ApplicationDbContext _context;
        public GetGatewayByIdCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Gateway> Handle(GetGatewayByIdCommand request, CancellationToken cancellationToken)
        {
            return await _context.Gateways.FindAsync(request.Id); 
        }
    }
}
