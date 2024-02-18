using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Middleware;
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
using Infrastructure.Database;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddSwaggerGen()
            .AddTransient<IQueueService, QueueService>() 
            .AddTransient<GlobalExceptionHandlingMiddleware>()
            .AddTransient<SQLInjectionHandlingMiddleware>()
            .AddTransient<IGameWriter, GameWriter>()
            .AddTransient<IGameReader, GameReader>()
            .AddTransient<IServerReader, ServerReader>()
            .AddTransient<IServerWriter, ServerWriter>()
            .AddTransient<IStatisticReader, StatisticReader>()
            .AddTransient<IStatisticWriter, StatisticWriter>()
            .AddTransient<IUserReader, UserReader>()
            .AddTransient<IUserWriter, UserWriter>()
            .AddSingleton<TempDataProvider>();
        return services;
    }
}
