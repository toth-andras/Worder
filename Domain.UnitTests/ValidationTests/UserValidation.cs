// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 3.3.2024

using Domain.Users;
using Domain.Validation;

namespace Domain.UnitTests.ValidationTests;

public class UserValidation
{
    private UserValidator validator = new UserValidator();

    [Fact(DisplayName = "Email validation")]
    public void EmailValidationTest()
    {
        // Email is null.
        {
            var user = new User() {Email = null, PasswordHash = "hash"};
            var validationResult = validator.Validate(user);
            
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        }
        // Email is empty.
        {
            var user = new User() {Email = string.Empty, PasswordHash = "hash"};
            var validationResult = validator.Validate(user);
            
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        }
        // Email is not of correct format.
        {
            var user = new User() {Email = "null", PasswordHash = "hash"};
            var validationResult = validator.Validate(user);
            
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal("EmailValidator", validationResult.Errors[0].ErrorCode);
        }
        // Email is correct.
        {
            var user = new User() {Email = "user@mail.com", PasswordHash = "hash"};
            var validationResult = validator.Validate(user);
            
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
    }

    [Fact(DisplayName = "Password validation")]
    public void PasswordValidation()
    {
        // Password is null.
        {
            var user = new User() {Email = "user@email.com", PasswordHash = null};
            var validationResult = validator.Validate(user);
            
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        }
        // Password is empty.
        {
            var user = new User() {Email = "user@email.com", PasswordHash = string.Empty};
            var validationResult = validator.Validate(user);
            
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        }
        // Password is correct.
        {
            var user = new User() {Email = "user@email.com", PasswordHash = "hash"};
            var validationResult = validator.Validate(user);
            
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
    }
}