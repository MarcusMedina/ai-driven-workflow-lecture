namespace MailingListManager.Services;

/// <summary>
/// Authentication service interface
/// Follows: SRP (only handles authentication), SoC (separated from UI)
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Validates admin credentials
    /// </summary>
    /// <param name="email">Admin email</param>
    /// <param name="password">Admin password</param>
    /// <returns>True if credentials are valid, false otherwise</returns>
    Task<bool> ValidateCredentialsAsync(string email, string password);

    /// <summary>
    /// Gets admin email if authenticated
    /// </summary>
    /// <returns>Admin email or null</returns>
    string? GetAuthenticatedUser();
}
