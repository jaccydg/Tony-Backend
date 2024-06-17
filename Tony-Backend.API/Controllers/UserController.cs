
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tony_Backend.API.Data;
using Tony_Backend.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Tony_Backend.Application.Commands.UserCommands;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(nameof(GetAllUsers))]
    public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
    {
        return Ok(await _sender.Send(new GetAllUsersCommand()));
    }
}