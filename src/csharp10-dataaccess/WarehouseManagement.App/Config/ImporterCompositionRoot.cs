using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WarehouseManagement.EF.App.Config.Log;
using WarehouseManagement.EF.App.DAL;
using WarehouseManagement.EF.App.Entities;

namespace WarehouseManagement.EF.App.Config;

internal static class ImporterCompositionRoot
{
    public static IServiceCollection AddProductImporter(this IServiceCollection services)
    {
        services.AddProductImporterLogic();

        return services;
    }
}

internal static class DIRegistrations
{
    public static IServiceCollection AddProductImporterLogic(this IServiceCollection serviceCollection)
    {
        var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //var connectionStringSqlite = appSettings.GetValue<string>("ConnectionStrings:SQLiteDB");
        //var connectionStringSqlServer = appSettings.GetValue<string>("ConnectionStrings:SQLServerDB");

        //serviceCollection.AddDbContext<SQLiteContextOld>(opt =>
        //    opt.UseSqlite(connectionStringSqlServer));

        //serviceCollection.AddDbContext<SQLiteContext>(opt =>
        //    opt.UseSqlite(connectionDB));

        //serviceCollection.AddTransient<IRepositoryBase<Customer>, RepositoryBaseEF<Customer>>();
        //serviceCollection.AddTransient<IRepositoryBase<Warehouse>, RepositoryBaseEF<Warehouse>>();
        //serviceCollection.AddTransient<IRepositoryBase<Item>, RepositoryBaseEF<Item>>();
        //serviceCollection.AddTransient<IRepositoryBase<LineItem>, RepositoryBaseEF<LineItem>>();
        //serviceCollection.AddTransient<IRepositoryBase<Order>, RepositoryBaseEF<Order>>();
        //serviceCollection.AddTransient<IRepositoryBase<ShippingProvider>, RepositoryBaseEF<ShippingProvider>>();

        serviceCollection.AddOptions<LogSettings>()
           .Bind(appSettings.GetSection(LogSettings.SectionName))
           .ValidateDataAnnotations()
           .ValidateOnStart();
        serviceCollection.AddSingleton(services => services
            .GetRequiredService<IOptions<LogSettings>>().Value);
        serviceCollection.AddTransient<ISettingsLogger, SettingsLogger>();
        serviceCollection.AddHostedService<ProgramService>();
                
        return serviceCollection;
    }
}

internal sealed class ProgramService : IHostedService
{
    private readonly IHostApplicationLifetime appLifetime;
    private ISettingsLogger settingsLogger;
    private readonly DateTime logStart;

    public ProgramService(IHostApplicationLifetime appLifetime, ISettingsLogger settingsLogger)
    {
        this.appLifetime = appLifetime;
        this.settingsLogger = settingsLogger;
        this.logStart = DateTime.Now;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                Console.WriteLine("Press any key to Set Up Services");
                Console.ReadKey();
            }
            finally
            {
                appLifetime.StopApplication();
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        settingsLogger = new SettingsLogger(new LogSettings
        {
            LogName = "Stop",
            LogDescription = "Stop application",
            LogStart = logStart,
            LogStop = DateTime.Now
        });
        settingsLogger.PrintLogSettings();
        Console.WriteLine("Press any key to Init App");
        Console.ReadKey();

        return Task.CompletedTask;
    }
}