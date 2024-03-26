// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 24.3.2024

using Domain.Users;

namespace Application.Users.Repositories;

public interface IUserRepository
{
    public Task<User> CreateUser(string email, string password);

    public Task<User?> GetUserByEmail(string email);

    public Task<bool> CheckPassword(string email, string password);

    public Task<User?> UpdateEmail(string oldEmail, string newEmail);

    public Task<User?> UpdatePassword(string email, string newPassword);

    public Task Delete(string email);
}