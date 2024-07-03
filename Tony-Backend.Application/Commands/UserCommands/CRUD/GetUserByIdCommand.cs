using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.UserCommands.CRUD
{
    public class GetUserByIdCommand : IRequest<ApplicationUser>
    {
        public Guid Id { get; set; }
    }

    internal class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, ApplicationUser>
    {
        private readonly ApplicationDbContext _context;
        public GetUserByIdCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(request.Id);
        }
    }
}
