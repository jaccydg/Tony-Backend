﻿using MediatR;
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
    public class GetAllUsersCommand : IRequest<IEnumerable<ApplicationUser>> { }

    internal class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, IEnumerable<ApplicationUser>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllUsersCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
