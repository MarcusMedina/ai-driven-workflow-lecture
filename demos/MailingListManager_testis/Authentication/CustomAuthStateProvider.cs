using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MailingListManager.Authentication;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User;

        if (user?.Identity?.IsAuthenticated ?? false)
        {
            var claims = user.Claims;
            var identity = new ClaimsIdentity(claims, "Bearer");
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(principal));
        }

        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        return Task.FromResult(new AuthenticationState(anonymousUser));
    }

    public void NotifyUserAuthentication(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, email),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, "Bearer");
        var principal = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(principal);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public void NotifyUserLogout()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = new AuthenticationState(anonymousUser);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
}
