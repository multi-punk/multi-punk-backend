namespace Api;

public class Program
{
    static async Task Main()
        => await CreateHostBuilder().Build().RunAsync();

    private static IHostBuilder CreateHostBuilder() 
    {
        return Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webHost => {
            webHost.UseStartup<Startup>();
            webHost.UseKestrel(kestrelOptions => { kestrelOptions.ListenAnyIP(5005); });
        });
    }
}

