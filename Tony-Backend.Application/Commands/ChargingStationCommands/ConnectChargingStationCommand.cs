﻿using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using RestSharp;
using Tony_Backend.Application.Commands.ChargingStationCommands.CRUD;
using Tony_Backend.Shared.Helpers;


namespace Tony_Backend.Application.Commands.ChargingStationCommands
{
    public class ConnectChargingStationCommand : IRequest<bool?>
    {
        public required Guid ChargingStationId { get; init; }
        public required string UserId { get; init; }
    }

    internal class ConnectChargingStationCommandHandler : IRequestHandler<ConnectChargingStationCommand, bool?>
    {
        private readonly ISender _sender;
        private readonly ApplicationDbContext _context;
        private readonly IQueueOperations _queueOperations;

        public ConnectChargingStationCommandHandler(ISender sender, ApplicationDbContext context, IQueueOperations queueOperations)
        {
            _sender = sender;
            _context = context;
            _queueOperations = queueOperations;
        }

        public async Task<bool?> Handle(ConnectChargingStationCommand request, CancellationToken cancellationToken)
        {
            // Check if charging station exists ad is free
            var chargingStationStatus = await _sender.Send(new CheckChargingStationCommand() { ChargingStationId = request.ChargingStationId });
            if (chargingStationStatus == null || chargingStationStatus == false)
            {
                return null;
            }

            var gatewayId = _context.ChargingStations
                .Where(cs => cs.Id == request.ChargingStationId)
                .Select(cs => cs.GatewayId)
                .FirstOrDefault();

            var message = new
            {
                RequestType = RequestType.Connection,
                request.ChargingStationId,
                GatewayId = gatewayId,
                request.UserId
            }.ToString();

            _queueOperations.SendMessage(message);


            // TODO:
            // dequeue from from-gateway queue
            // if positive response set chargingstaiton status to idle

            // temporary solution
            return chargingStationStatus;
        }
    }
}
