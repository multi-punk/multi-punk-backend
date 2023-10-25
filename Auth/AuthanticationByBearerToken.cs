using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Auth;

public class ApiKeyAuthantication: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private AppDbContext dbContext;

    public ApiKeyAuthantication
    (
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        AppDbContext dbContext
    ) : base(options, logger, encoder, clock)
    {
        this.dbContext = dbContext;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("MULTI-API-KEY", out var apiKeyHeaderValues))
            return AuthenticateResult.Fail("API Key was not provided.");

        string? providedApiKey = apiKeyHeaderValues.FirstOrDefault();
        Console.WriteLine(providedApiKey);

        if (FindApiKey(providedApiKey, out ApiKey apiKey))
        {
            var claims = new[] 
            {
                new Claim("KEY-TYPE", apiKey.type.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        Console.WriteLine("im there");
        return AuthenticateResult.Fail("Invalid API Key provided.");
    }

    private bool FindApiKey(string? providedApiKey, out ApiKey? apiKey)
    {
        apiKey = dbContext.ApiKeys.Find(providedApiKey);
        return dbContext.ApiKeys.Any(key => key.Key == providedApiKey);
    }
}
