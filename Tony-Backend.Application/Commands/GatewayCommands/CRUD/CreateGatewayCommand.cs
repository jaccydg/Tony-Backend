using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.GatewayCommands.CRUD
{
    public class CreateGatewayCommand : IRequest<Gateway>
    {
        public required string Name { get; init; }
        public required double? Latitude { get; init; }
        public required double? Longitude { get; init; }
    }

    internal class CreateGatewayCommandHandler : IRequestHandler<CreateGatewayCommand, Gateway>
    {
        private readonly ApplicationDbContext _context;
        public CreateGatewayCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Gateway> Handle(CreateGatewayCommand request, CancellationToken cancellationToken)
        {
            var gateway = new Gateway
            {
                Name = request.Name,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
            _context.Gateways.Add(gateway);
            await _context.SaveChangesAsync();

            return gateway;
        }
    }
}
