namespace ApiRequestModels;

public record EmailPasswordRequest(string Email, string Password);

public record EmailRequest(string Email);

public record ChangePasswordRequest(string Email, string OldPassword, string NewPassword);

public record ChangeEmailRequest(string OldEmail, string NewEmail);