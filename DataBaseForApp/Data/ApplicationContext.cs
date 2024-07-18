using Microsoft.EntityFrameworkCore;
using DataBase.Models;
using DataBase.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataBase.Data;
/// <summary>
/// Контекст приложения для подключения к БД
/// </summary>
public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityUserContext<User>(options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const DeleteBehavior deleteBehavior = DeleteBehavior.Restrict;
        modelBuilder.Entity<Partner>()
            .HasOne(p => p.PartnerType)
            .WithMany(p => p.Partners)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<Division>()
            .HasOne(d => d.Faculty)
            .WithMany(f => f.Divisions)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<Agreement>()
            .HasOne(a => a.AgreementType)
            .WithMany(t => t.Agreements)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<Agreement>()
            .HasOne(a => a.AgreementStatus)
            .WithMany(s => s.Agreements)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<Interaction>()
            .HasOne(i => i.InteractionType)
            .WithMany(t => t.Interactions)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<Interaction>()
            .HasOne(i => i.Division)
            .WithMany(d => d.Interactions)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<Interaction>()
            .HasOne(i => i.Partner)
            .WithMany(p=>p.Interactions)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<DivisionInAgreement>()
            .HasOne(d => d.Division)
            .WithMany(d => d.DivisionsInAgreement)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<DivisionInAgreement>()
            .HasOne(d => d.Agreement)
            .WithMany(a => a.DivisionInAgreements)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<PartnerInAgreement>()
            .HasOne(p => p.Partner)
            .WithMany(p => p.PartnersInAgreement)
            .OnDelete(deleteBehavior);
        modelBuilder.Entity<PartnerInAgreement>()
            .HasOne(p => p.Agreement)
            .WithMany(a => a.PartnerInAgreements)
            .OnDelete(deleteBehavior);
        
        base.OnModelCreating(modelBuilder);
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
        //Устанавливаем конвертацию системных типов в типы PostgreSQL по-умолчанию
        configurationBuilder
            .Properties<DateTime>()
            .HaveColumnType("date");

        configurationBuilder
           .Properties<string>()
           .HaveColumnType("varchar");

    }
}
