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
using static System.Runtime.InteropServices.JavaScript.JSType;
using RestSharp;
using Tony_Backend.Application.Commands.ChargingStationCommands.CRUD;


namespace Tony_Backend.Application.Commands.ChargingStationCommands
{
    public class ConnectChargingStationCommand : IRequest<ChargingStation?>
    {
        public required int Number { get; init; }
        public required int GatewayId { get; init; }
        public required string UserId { get; init; }
    }

    internal class ConnectChargingStationCommandHandler : IRequestHandler<ConnectChargingStationCommand, ChargingStation?>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISender _sender;
        private readonly string _connectionString;

        public ConnectChargingStationCommandHandler(ISender sender, ApplicationDbContext context, IConfiguration configuration)
        {
            _sender = sender;
            _context = context;
            _connectionString = configuration.GetConnectionString("tony_backend_queueapi");
        }

        public async Task<ChargingStation?> Handle(ConnectChargingStationCommand request, CancellationToken cancellationToken)
        {
            // Check if charging station exists ad is free
            var chargingStationStatus = await _sender.Send(new CheckChargingStationCommand() { Number = request.Number, GatewayId = request.GatewayId });
            if (chargingStationStatus == null || chargingStationStatus == false)
            {
                return null;
            }

            var chargingStation = await _sender.Send(new GetChargingStationByIdCommand() { Number = request.Number, GatewayId = request.GatewayId });

            // API request to queue api server
            var client = new RestClient(_connectionString);
            var httpRequest = new RestRequest($"/ToGatewayQueue/{request.Number}/{request.GatewayId}/Connect", Method.Post);
            httpRequest.AddQueryParameter("userId", request.UserId);

            var response = client.Execute(httpRequest);


            // TODO: dequeue from from-gateway queue

            // temporary solution
            if (response.IsSuccessful)
            {
                return chargingStation;
            }
            return chargingStation;
        }
    }
}
