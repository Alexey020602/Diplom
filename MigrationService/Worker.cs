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
            seed.Seed();
            // await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    

    private static async Task SeedDataAsync(ApplicationContext dbContext, CancellationToken cancellationToken)
    {
        // SupportTicket firstTicket = new()
        // {
        //     Title = "Test Ticket",
        //     Description = "Default ticket, please ignore!",
        //     Completed = true
        // };
        //
        // var strategy = dbContext.Database.CreateExecutionStrategy();
        // await strategy.ExecuteAsync(async () =>
        // {
        //     // Seed the database
        //     await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        //     await dbContext.Tickets.AddAsync(firstTicket, cancellationToken);
        //     await dbContext.SaveChangesAsync(cancellationToken);
        //     await transaction.CommitAsync(cancellationToken);
        // });
    }
}