using System.ComponentModel.DataAnnotations;
using ApiRequestModels;
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
    public async Task<User> Register([FromBody]EmailPasswordRequest request)
    {
        return await _userService.CreateUser(request.Email, request.Password);
    }

    [HttpPost("login")]
    public async Task<User> Login([FromBody]EmailPasswordRequest request)
    {
        return await _userService.Login(request.Email, request.Password);
    }

    [HttpGet]
    public async Task<User> GetByEmail([FromQuery] EmailRequest request)
    {
        return await _userService.GetUserByEmail(request.Email);
    }

    [HttpPost("password_check")]
    public async Task<bool> CheckPassword([FromBody]EmailPasswordRequest request)
    {
        return await _userService.CheckPassword(request.Email, request.Password);
    }

    [HttpPut("update/email")]
    public async Task<User> UpdateEmail(ChangeEmailRequest request)
    {
        return await _userService.UpdateEmail(request.OldEmail, request.NewEmail);
    }

    [HttpPut("update/password")]
    public async Task<User> UpdatePassword([FromBody]ChangePasswordRequest request)
    {
        return await _userService.UpdatePassword(request.Email, request.OldPassword, request.NewPassword);
    }

    [HttpDelete("delete")]
    public async Task DeleteUser([FromQuery]EmailRequest request)
    {
        await _userService.Delete(request.Email);
    }
}