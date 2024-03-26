// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 25.3.2024

using Application.Exceptions;
using Application.Users.Repositories;
using Application.Users.Services;
using Domain.Users;

namespace Infrastructure.Users.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
        => _repository = repository;

    private async Task ThrowIfUserNotFound(string email)
    {
        if (await _repository.GetUserByEmail(email) is null)
        {
            throw new NotFoundException($"No user with such email: {email}");
        }
    }

    private async Task ThrowIfEmailTaken(string email)
    {
        if (await _repository.GetUserByEmail(email) is not null)
        {
            throw new EmailAlreadyInUserException($"Email {email} is already in use");
        }
    }
    
    
    public async Task<User> CreateUser(string email, string password)
    {
        await ThrowIfEmailTaken(email);
        
        return await _repository.CreateUser(email, password);
    }

    public async Task<User> Login(string email, string password)
    {
        var user = await _repository.GetUserByEmail(email) 
                   ?? throw new NotFoundException($"No user with such email: {email}");
        
        if (await _repository.CheckPassword(email, password) is false)
        {
            throw new IncorrectPasswordException($"Incorrect password for {email}");
        }

        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _repository.GetUserByEmail(email) 
               ?? throw new NotFoundException($"No user with such email: {email}");
    }

    public async Task<bool> CheckPassword(string email, string password)
    {
        await ThrowIfUserNotFound(email);

        return await _repository.CheckPassword(email, password);
    }

    public async Task<User> UpdateEmail(string oldEmail, string newEmail)
    {
        await ThrowIfUserNotFound(oldEmail);

        await ThrowIfEmailTaken(newEmail);

        return (await _repository.UpdateEmail(oldEmail, newEmail))!;
    }

    public async Task<User> UpdatePassword(string email, string oldPassword, string newPassword)
    {
        await ThrowIfUserNotFound(email);

        if (await _repository.CheckPassword(email, oldPassword) is false)
        {
            throw new IncorrectPasswordException("Incorrect password");
        }

        return (await _repository.UpdatePassword(email, newPassword))!;
    }

    public async Task Delete(string email)
    {
        await ThrowIfUserNotFound(email);

        await _repository.Delete(email);
    }
}