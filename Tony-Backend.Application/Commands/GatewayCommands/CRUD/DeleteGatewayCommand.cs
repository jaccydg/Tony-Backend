using MediatR;
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
    public class DeleteGatewayCommand : IRequest<bool>
    {
        public required int Id { get; init; }
    }

    internal class DeleteGatewayCommandHandler : IRequestHandler<DeleteGatewayCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteGatewayCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteGatewayCommand request, CancellationToken cancellationToken)
        {
            var gateway = await _context.Gateways.FindAsync(request.Id);

            if (gateway == null)
            {
                return false;
            }

            _context.Gateways.Remove(gateway);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
