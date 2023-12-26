using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Infrastructure.Database.Enums;

namespace Api.Auth;

public class ApiKeyAuthantication: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private AppDbContext ctx;

    public ApiKeyAuthantication (
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        AppDbContext ctx
    ) : base(options, logger, encoder, clock)
    {
        this.ctx = ctx;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("MULTI-API-KEY", out var apiKeyHeaderValues))
            return AuthenticateResult.Fail("API Key was not provided.");

        string? providedApiKey = apiKeyHeaderValues.FirstOrDefault();
        Console.WriteLine("APIKEY: "+providedApiKey);

        if (FindApiKey(providedApiKey, out ApiKey apiKey))
        {
            var claims = new[] 
            {
                new Claim("KEY-TYPE", apiKey.Type.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        return AuthenticateResult.Fail("Invalid API Key provided.");
    }

    private bool FindApiKey(string? providedApiKey, out ApiKey? apiKey)
    {
        apiKey = ctx.ApiKeys.Find(providedApiKey);
        return ctx.ApiKeys.Any(key => key.Key == providedApiKey);
    }
}
