using Domain.Auth;
using Domain.Users;

namespace ApiRequestModels;

// Requests
public record EmailPasswordRequest(string Email, string Password);
public record EmailRequest(string Email);
public record ChangePasswordRequest(string Email, string OldPassword, string NewPassword);
public record ChangeEmailRequest(string OldEmail, string NewEmail);
public record VerifyTokenRequest(int UserId, string Token);
public record RefreshTokenRequest(int UserId, string RefreshToken);

// Responses
public record UserTokenResponse(User User, Token Token);