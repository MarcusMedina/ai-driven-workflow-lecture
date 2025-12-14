using MailingListManager.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace MailingListManager.Tests;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthServiceTests()
    {
        // Setup mock configuration
        var configDict = new Dictionary<string, string?>
        {
            { "AdminUser:Email", "admin@example.com" },
            { "AdminUser:Password", "Admin123!" }
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configDict!)
            .Build();

        _authService = new AuthService(_configuration);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var email = "admin@example.com";
        var password = "Admin123!";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Inloggning lyckades", result.Message);
        Assert.Equal("admin@example.com", result.Email);
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
    {
        // Arrange
        var email = "admin@example.com";
        var password = "WrongPassword";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Felaktig email eller lösenord", result.Message);
    }

    [Fact]
    public async Task Login_WithInvalidEmail_ReturnsUnauthorized()
    {
        // Arrange
        var email = "wrong@example.com";
        var password = "Admin123!";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Felaktig email eller lösenord", result.Message);
    }

    [Fact]
    public async Task Login_WithNullEmail_ReturnsValidationError()
    {
        // Arrange
        string? email = null;
        var password = "Admin123!";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Email-fältet får inte vara tomt", result.Message);
    }

    [Fact]
    public async Task Login_WithEmptyPassword_ReturnsValidationError()
    {
        // Arrange
        var email = "admin@example.com";
        string? password = "";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Lösenordsfältet får inte vara tomt", result.Message);
    }

    [Fact]
    public async Task Login_WithWhitespace_TrimsAndSucceeds()
    {
        // Arrange
        var email = "  admin@example.com  ";
        var password = "Admin123!";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("admin@example.com", result.Email);
    }

    [Fact]
    public async Task Login_CaseInsensitiveEmail_Succeeds()
    {
        // Arrange
        var email = "ADMIN@EXAMPLE.COM";
        var password = "Admin123!";

        // Act
        var result = await _authService.LoginAsync(email, password);

        // Assert
        Assert.True(result.Success);
    }
}
