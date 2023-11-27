using Microsoft.EntityFrameworkCore;
using DataBase.Models;
namespace DataBase.Data;
/// <summary>
/// Контекст приложения для подключения к БД
/// </summary>
public class ApplicationContext:DbContext
{
    /// <summary>
    /// DbSet партнеров в база данных
    /// </summary>
    public DbSet<Partner> Partners { get; set; }
    /// <summary>
    /// DbSet типов партнеров
    /// </summary>
    public DbSet<PartnerType> PartnerTypes { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<Faculty> Faculties { get; set; }
    /// <summary>
    /// DbSet подразделений университета
    /// </summary>
    public DbSet<Division> Divisions { get; set; }
    /// <summary>
    /// DbSet взаимодействий между подразделениями и партнерами
    /// </summary>
    public DbSet<Interaction> Interactions { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<InteractionType> InteractionTypes { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<Direction> Directions { get; set; }

    public ApplicationContext():base()
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=Diploma;Username=postgres;Password=11111111")
            //.LogTo(Console.WriteLine)
            ;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Запрещаем каскадное удаление
        var onDelete = DeleteBehavior.Restrict;
        modelBuilder.Entity<Partner>()
            .HasOne(p => p.PartnerType)
            .WithMany(p => p.Partners)
            .OnDelete(onDelete);
        modelBuilder.Entity<Division>()
            .HasOne(d => d.Faculty)
            .WithMany(f => f.Divisions)
            .OnDelete(onDelete);
        modelBuilder.Entity<Agreement>()
            .HasOne(a => a.AgreementType)
            .WithMany(t => t.Agreements)
            .OnDelete(onDelete);
        modelBuilder.Entity<Agreement>()
            .HasOne(a => a.AgreementStatus)
            .WithMany(s => s.Agreements)
            .OnDelete(onDelete);
        modelBuilder.Entity<Interaction>()
            .HasOne(i => i.InteractionType)
            .WithMany(t => t.Interactions)
            .OnDelete(onDelete);
        modelBuilder.Entity<Interaction>()
            .HasOne(i => i.Division)
            .WithMany(d => d.Interactions)
            .OnDelete(onDelete);
        modelBuilder.Entity<Interaction>()
            .HasOne(i => i.Partner)
            .WithMany(p=>p.Interactions)
            .OnDelete(onDelete);
        modelBuilder.Entity<DivisionInAgreement>()
            .HasOne(d => d.Division)
            .WithMany(d => d.DivisionsInAgreement)
            .OnDelete(onDelete);
        modelBuilder.Entity<DivisionInAgreement>()
            .HasOne(d => d.Agreement)
            .WithMany(a => a.DivisionInAgreements)
            .OnDelete(onDelete);
        modelBuilder.Entity<PartnerInAgreement>()
            .HasOne(p => p.Partner)
            .WithMany(p => p.PartnersInAgreement)
            .OnDelete(onDelete);
        modelBuilder.Entity<PartnerInAgreement>()
            .HasOne(p => p.Agreement)
            .WithMany(a => a.PartnerInAgreements)
            .OnDelete(onDelete);
        
        //Устанавливаем конвертацию системных типов в типы PostgreSQL по-умолчанию
        
    }


    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<Agreement> Agreements { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<DivisionInAgreement> DivisionsInAgreement { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<PartnerInAgreement> PartnersInAgreement { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<AgreementType> AgreementType { get; set; }
    /// <summary>
    /// DbSet факультета университета
    /// </summary>
    public DbSet<AgreementStatus> AgreementStatus { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTime>()
            .HaveColumnType("date");

        configurationBuilder
           .Properties<string>()
           .HaveColumnType("varchar");

    }
    
}
