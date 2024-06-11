using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands
{
    public class GetGatewayCommand : IRequest<Gateway>
    {
        public required string Id { get; init; }
    }

    internal class GetGatewayCommandHandler : IRequestHandler<GetGatewayCommand, Gateway>
    {
        private readonly string _connectionString;
        public GetGatewayCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("postgres");
        }

        public Task<Gateway> Handle(GetGatewayCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
