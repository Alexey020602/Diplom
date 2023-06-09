﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataBase.Data;
/// <summary>
/// Контекст приложения для подключения к БД
/// </summary>
public class ApplicationContext:DbContext
{
    public DbSet<Partner> Partners { get; set; }
    public DbSet<PartnerType> PartnerTypes { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    public DbSet<InteractionType> InteractionTypes { get; set; }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<Agreement> Agreements { get; set; }
    public DbSet<DivisionInAgreement> DivisionsInAgreement { get; set; }
    public DbSet<PartnerInAgreement> PartnersInAgreement { get; set; }
    public DbSet<AgreementType> AgreementType { get; set; }
    public DbSet<AgreementStatus> AgreementStatus { get; set; }

    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=Diploma;Username=postgres;Password=11111111")
            .LogTo(Console.WriteLine)
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

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTime>()
            .HaveColumnType("date");
        
         configurationBuilder
            .Properties<string>()
            .HaveColumnType("char");
            
    }
    
}
