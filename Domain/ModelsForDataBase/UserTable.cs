// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

namespace Migrations.TableDescriptors;

public static class UserTable
{
    public static readonly string TableName = "users";
    public static readonly string Id = "id";
    public static readonly string Email = "email";
    public static readonly string Password = "password";
    public static readonly string Salt = "salt";
}