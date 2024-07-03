﻿using MediatR;
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
    public class EditGatewayCommand : IRequest<Gateway>
    {
        public required Guid Id { get; init; }
        public required string? Name { get; init; }
        public required double? Longitude { get; init; }
        public required double? Latitude { get; init; }
    }

    internal class UpdateGatewayCommandHandler : IRequestHandler<EditGatewayCommand, Gateway>
    {
        private readonly ApplicationDbContext _context;
        public UpdateGatewayCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Gateway> Handle(EditGatewayCommand request, CancellationToken cancellationToken)
        {
            var gateway = await _context.Gateways.FindAsync(request.Id);

            if (gateway == null)
            {
                return null;
            }

            // Update properties if provided values are not null or empty
            if (!string.IsNullOrEmpty(request.Name))
            {
                gateway.Name = request.Name;
            }
            if (request.Latitude != null)
            {
                gateway.Latitude = request.Latitude;
            }
            if (request.Longitude != null)
            {
                gateway.Longitude = request.Longitude;
            }

            await _context.SaveChangesAsync();

            return gateway;
        }
    }
}
