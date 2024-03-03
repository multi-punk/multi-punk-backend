using App.Contracts;
using App.Contracts.Games;
using App.Contracts.Servers;
using App.Contracts.Statistics;
using App.Contracts.Users;
using App.Games;
using App.Queue;
using App.Servers;
using App.Statistics;
using App.Users;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace App;

public static class Extender
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddTransient<IQueueService, QueueService>() 
            .AddTransient<IGameWriter, GameWriter>()
            .AddTransient<IGameReader, GameReader>()
            .AddTransient<IServerReader, ServerReader>()
            .AddTransient<IServerWriter, ServerWriter>()
            .AddTransient<IStatisticReader, StatisticReader>()
            .AddTransient<IStatisticWriter, StatisticWriter>()
            .AddTransient<IUserReader, UserReader>()
            .AddTransient<IUserWriter, UserWriter>()
            .AddTransient<IServerWriter, ServerWriter>()
            .AddTransient<IServerReader, ServerReader>()
            .AddSingleton<TempDataProvider>();
        return services;
    }
}
