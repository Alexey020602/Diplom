using DataBase.Data;

namespace MigrationService;
using System.Diagnostics;
using OpenTelemetry.Trace;
public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var seed = scope.ServiceProvider.GetRequiredService<ApplicationContextSeed>();
            await dbContext.EnsureDatabaseAsync(cancellationToken);
            await dbContext.RunMigrationAsync(cancellationToken);
            await seed.Seed(cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}