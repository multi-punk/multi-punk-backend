using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using multi_api.Database;

namespace multi_api.Auth;

public class AuthanticationByBearerToken: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private AppDbContext dbContext;

    public AuthanticationByBearerToken(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        AppDbContext dbContext) : base(options, logger, encoder, clock)
    {
        this.dbContext = dbContext;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var apiKeyHeaderValues))
        {
            return AuthenticateResult.Fail("API Key was not provided.");
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

        if (await IsValidApiKey(providedApiKey))
        {
            var claims = new[] { new Claim("Bearer", providedApiKey) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        return AuthenticateResult.Fail("Invalid API Key provided.");
    }

    private async Task<bool> IsValidApiKey(string apiKey)
    {
        return dbContext.ApiKeys.Any(key => key.Key == apiKey.Replace("Bearer ", ""));
    }
}
