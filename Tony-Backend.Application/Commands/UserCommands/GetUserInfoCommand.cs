using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.DTO;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Application.Commands.UserCommands.CRUD
{
    public class GetUserInfoCommand : IRequest<ApplicationUserInfoDTO> 
    {
        public Guid Id { get; set; }
    }

    internal class GetUserInfoCommandHandler : IRequestHandler<GetUserInfoCommand, ApplicationUserInfoDTO>
    {
        private readonly ApplicationDbContext _context;
        public GetUserInfoCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUserInfoDTO> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id.ToString());
            if (user == null)
            {
                return null;
            }

            var DTOUser = new ApplicationUserInfoDTO
            {
                Email = user.Email
            };

            return DTOUser;
        }
    }
}
