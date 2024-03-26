using Application.Users.Repositories;
using Application.Users.Services;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController: ControllerBase
{
    public readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<User> Register(string email, string password)
    {
        return await _userService.CreateUser(email, password);
    }

    [HttpPost("login")]
    public async Task<User> Login(string email, string password)
    {
        return await _userService.Login(email, password);
    }

    [HttpGet("{email}")]
    public async Task<User> GetByEmail([FromRoute]string email)
    {
        return await _userService.GetUserByEmail(email);
    }

    [HttpPost("password_check")]
    public async Task<bool> CheckPassword(string email, string password)
    {
        return await _userService.CheckPassword(email, password);
    }

    [HttpPut("update/email")]
    public async Task<User> UpdateEmail(string oldEmail, string newEmail)
    {
        return await _userService.UpdateEmail(oldEmail, newEmail);
    }

    [HttpPut("update/password")]
    public async Task<User> UpdatePassword(string email, string oldPassword, string newPassword)
    {
        return await _userService.UpdatePassword(email, oldPassword, newPassword);
    }

    [HttpDelete("delete")]
    public async Task DeleteUser(string email)
    {
        await _userService.Delete(email);
    }
}