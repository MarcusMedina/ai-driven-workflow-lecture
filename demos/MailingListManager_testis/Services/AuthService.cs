using MailingListManager.Models;
using Microsoft.Extensions.Options;

namespace MailingListManager.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<AuthResult> LoginAsync(string? email, string? password)
    {
        // Validering av inputs
        if (string.IsNullOrWhiteSpace(email))
        {
            return new AuthResult
            {
                Success = false,
                Message = "Email-fältet får inte vara tomt"
            };
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return new AuthResult
            {
                Success = false,
                Message = "Lösenordsfältet får inte vara tomt"
            };
        }

        // Hämta admin-credentials från config
        var adminEmail = _configuration["AdminUser:Email"];
        var adminPassword = _configuration["AdminUser:Password"];

        // Trim och lowercase för email-jämförelse (case-insensitive)
        email = email.Trim().ToLower();

        // Jämför med hårdkodad admin
        if (email == adminEmail?.ToLower() && password == adminPassword)
        {
            return new AuthResult
            {
                Success = true,
                Message = "Inloggning lyckades",
                Email = email
            };
        }

        return new AuthResult
        {
            Success = false,
            Message = "Felaktig email eller lösenord"
        };
    }

    public async Task LogoutAsync()
    {
        // Logout-logik hanteras av AuthenticationStateProvider
        await Task.CompletedTask;
    }
}
