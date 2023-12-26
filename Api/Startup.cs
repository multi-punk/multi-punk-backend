using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication;
using Api.Auth;
using Api.Middleware;
using Api.Controllers.Hubs;

namespace Api;

public class Startup
{
    private readonly IConfiguration configuration;
    private List<string> users = new List<string>();

    public Startup()
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("Settings/appsettings.json", optional: false)
            .Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSignalR();

        services
            .AddSwaggerGen()
            .AddTransient<GlobalExceptionHandlingMiddleware>()
            .AddTransient<SQLInjectionHandlingMiddleware>()
            .AddSingleton<TempDataProvider>()
            .AddSingleton(configuration)
            .AddDbContext<AppDbContext>(c => c.UseNpgsql(configuration.GetValue<string>("connectionOnServer")))
            .AddAuthorization(options => 
            {
                options.AddPolicy("MULTI-API-KEY-PRIVATE", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireClaim("KEY-TYPE", "Private");
                });
                options.AddPolicy("MULTI-API-KEY-PUBLIC", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireClaim("KEY-TYPE", "Public");
                });
            })
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MULTI-API-KEY";
                options.DefaultChallengeScheme = "MULTI-API-KEY";
            }).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthantication>("MULTI-API-KEY", options => { });
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
            });
        }

        app.UseRouting();
        app.UseCors();

        // app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        // app.UseMiddleware<SQLInjectionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<QueueHub>("api/hubs/queue-hub");
        });
    }
}