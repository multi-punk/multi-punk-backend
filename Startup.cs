using Microsoft.EntityFrameworkCore;
using MultiApi.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using MultiApi.Auth;
using MultiApi.Middleware;

namespace MultiApi;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup()
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("Settings/appsettings.json", optional: false)
            .Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddSwaggerGen()
            .AddTransient<GlobalExceptionHandlingMiddleware>()
            .AddTransient<SQLInjectionHandlingMiddleware>()
            .AddSingleton(configuration)
            .AddDbContext<AppDbContext>(c => c.UseNpgsql(configuration.GetValue<string>("connectionOnServer")))
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddScheme<AuthenticationSchemeOptions, AuthanticationByBearerToken>("Bearer", options => { });
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

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.UseMiddleware<SQLInjectionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}