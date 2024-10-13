namespace Diploma.DataSeed;

public class Worker(IServiceProvider serviceProvider): BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => serviceProvider
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<IdentitySeed>()
            .Seed(stoppingToken);
}