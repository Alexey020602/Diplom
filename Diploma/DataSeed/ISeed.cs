namespace Diploma.DataSeed;

public interface ISeed
{
    Task Seed(CancellationToken cancellationToken = default);
}