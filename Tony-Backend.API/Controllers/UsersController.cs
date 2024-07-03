
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
using Tony_Backend.Application.Commands.UserCommands.CRUD;
using Tony_Backend.Shared.DTO;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAll()
    {
        return Ok(await _sender.Send(new GetAllUsersCommand()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUser>> GetById([FromRoute] Guid id)
    {
        var user = await _sender.Send(new GetUserByIdCommand() { Id = id });
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [Authorize]
    [HttpGet("/Info")]
    public async Task<ActionResult<ApplicationUserInfoDTO>> GetInfo()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var user = await _sender.Send(new GetUserInfoCommand() { Id = userId });
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}