using System.Diagnostics;
using DataBase.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace MigrationService;


public class Worker(
    IServiceProvider services,
    IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService 
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var applicationContextSeed = scope.ServiceProvider.GetRequiredService<ApplicationContextSeed>();
            await dbContext.EnsureDatabaseAsync(cancellationToken);
            await dbContext.RunMigrationAsync(cancellationToken);
            await applicationContextSeed.Seed(cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}