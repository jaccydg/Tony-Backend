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

namespace Tony_Backend.Application.Commands.UserCommands
{
    public class GetAllUsersCommand : IRequest<IEnumerable<IdentityUser>> { }

    internal class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, IEnumerable<IdentityUser>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllUsersCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IdentityUser>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
