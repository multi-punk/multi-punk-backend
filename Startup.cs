using Microsoft.EntityFrameworkCore;
using multi_api.Database;
using Microsoft.Extensions.DependencyInjection;

namespace multi_api;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup()
    {
        configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "m.")
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddSwaggerGen()
            .AddSingleton(configuration)
            .AddDbContext<AppDbContext>(c => c.UseNpgsql(configuration.GetValue<string>("connectionOnServer")));
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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}