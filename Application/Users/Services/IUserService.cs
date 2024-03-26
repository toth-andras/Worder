// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 25.3.2024

using Domain.Users;

namespace Application.Users.Services;

public interface IUserService
{
    public Task<User> CreateUser(string email, string password);

    public Task<User> Login(string email, string password);

    public Task<User> GetUserByEmail(string email);

    public Task<bool> CheckPassword(string email, string password);

    public Task<User> UpdateEmail(string oldEmail, string newEmail);

    public Task<User> UpdatePassword(string email, string oldPassword, string newPassword);

    public Task Delete(string email);
}