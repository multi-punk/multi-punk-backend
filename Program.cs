namespace MultiApi;

public class Program
{
    static void Main()
        => CreateHostBuilder().Build().Run();

    private static IHostBuilder CreateHostBuilder() 
    {
        return Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webHost => {
            webHost.UseStartup<Startup>();
            webHost.UseKestrel(kestrelOptions => { kestrelOptions.ListenAnyIP(5005); });
        });
    }

    public static bool IsDebug()
    {
#if DEBUG
        return true;
#else
        return false;
#endif
    }
}

