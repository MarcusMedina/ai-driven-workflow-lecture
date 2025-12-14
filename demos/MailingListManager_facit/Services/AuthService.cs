namespace MailingListManager.Services;

/// <summary>
/// Authentication service with hardcoded admin credentials (MVP)
/// Follows: SRP (only auth logic), KISS (simple implementation), DRY (no duplication)
///
/// SECURITY NOTE: Hardcoded credentials are for DEMO purposes only.
/// Production should use ASP.NET Core Identity with hashed passwords.
/// </summary>
public class AuthService : IAuthService
{
    private string? _authenticatedUser;

    // Hardcoded for demo - in production, use Identity with bcrypt
    private const string ADMIN_EMAIL = "admin@example.com";
    private const string ADMIN_PASSWORD = "Admin123!";

    public AuthService() { }

    public async Task<bool> ValidateCredentialsAsync(string email, string password)
    {
        // Input validation - prevent XSS and injection
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return false;

        // Sanitize inputs (basic XSS prevention)
        email = email.Trim();
        password = password.Trim();

        // Validate credentials (case-insensitive email)
        await Task.Delay(100); // Simulate async DB call

        bool isValid = email.Equals(ADMIN_EMAIL, StringComparison.OrdinalIgnoreCase)
                    && password == ADMIN_PASSWORD;

        if (isValid)
        {
            _authenticatedUser = email;
        }

        return isValid;
    }

    public string? GetAuthenticatedUser()
    {
        return _authenticatedUser;
    }
}
