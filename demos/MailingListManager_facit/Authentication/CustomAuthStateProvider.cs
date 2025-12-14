using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MailingListManager.Authentication;

/// <summary>
/// Custom authentication state provider for Blazor
/// Manages user authentication state across the application
/// Follows: SRP (only manages auth state), SoC (separated from auth logic)
/// </summary>
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private ClaimsPrincipal? _currentUser;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var httpUser = _httpContextAccessor.HttpContext?.User;

        if (httpUser?.Identity?.IsAuthenticated == true)
        {
            return Task.FromResult(new AuthenticationState(httpUser));
        }

        var user = _currentUser ?? _anonymous;
        return Task.FromResult(new AuthenticationState(user));
    }

    /// <summary>
    /// Marks user as authenticated
    /// </summary>
    /// <param name="email">Admin email</param>
    public void MarkUserAsAuthenticated(string email)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Admin")
        }, "Custom Authentication");

        _currentUser = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    /// <summary>
    /// Marks user as logged out
    /// </summary>
    public void MarkUserAsLoggedOut()
    {
        _currentUser = _anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
