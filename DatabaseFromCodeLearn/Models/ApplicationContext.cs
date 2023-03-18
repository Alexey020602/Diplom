using Microsoft.EntityFrameworkCore;

namespace Diploma.Models;
/// <summary>
/// Контекст приложения для подключения к БД
/// </summary>
public class ApplicationContext:DbContext
{
    public DbSet<Partner> Partners { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql( "Host=localhost;Port=5432;Database=Diplom;Username=postgres;Password=72637263");
    }
}

