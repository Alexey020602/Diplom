using DataBase.Data;
using DataBase.Extensions;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MigrationService;

public class ApplicationContextSeed(
    ApplicationContext context,
    ILogger<ApplicationContextSeed> logger,
    IHostEnvironment environment
)
{
    public async Task Seed(CancellationToken cancellationToken)
    {
        try
        {
            await SeedThrows(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", ex.Message);
            throw;
        }
    }

    private void Migrate()
    {
        if (!context.Database.IsRelational()) return;
        context.Database.Migrate();
    }

    private Task SeedThrows(CancellationToken cancellationToken)
    {
        // Migrate();
        if (!environment.IsDevelopment()) return Task.CompletedTask;
        return SeedData(cancellationToken);
    }

    private async Task SeedData(CancellationToken cancellationToken)
    {
        await AddFaculties(cancellationToken);
        await AddPartnerTypes(cancellationToken);
        await AddDirections(cancellationToken);
        await AddInteractionTypes(cancellationToken);
        await AddAgreementStatuses(cancellationToken);
        await AddAgreementTypes(cancellationToken);
        await AddPartners(cancellationToken);
        // AddUsers();
    }

    private async Task AddInteractionTypes(CancellationToken cancellationToken)
    {
        var interactionTypes = new List<InteractionType>
        {
            new() { Id = 1, Name = "Первый" },
            new() { Id = 2, Name = "Второй" },
            new() { Id = 3, Name = "Третий" },
            new() { Id = 4, Name = "Четвертый" }
        };

        foreach (var interactionType in interactionTypes) await Add(interactionType, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task AddAgreementTypes(CancellationToken cancellationToken)
    {
        var agreementTypes = new List<AgreementType>
        {
            new() { Id = 1, Name = "Первый" },
            new() { Id = 2, Name = "Вторый" },
            new() { Id = 3, Name = "Третий" },
            new() { Id = 4, Name = "Четвертый" }
        };

        foreach (var agreementType in agreementTypes) await Add(agreementType, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task AddAgreementStatuses(CancellationToken cancellationToken)
    {
        var agreementsStatuses = new List<AgreementStatus>
        {
            new() { Id = 1, Name = "Ожидает" },
            new() { Id = 2, Name = "Действует" },
            new() { Id = 3, Name = "Приостановлено" },
            new() { Id = 4, Name = "Завершено" }
        };

        foreach (var agreementStatus in agreementsStatuses) await Add(agreementStatus, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    private Task Add(InteractionType interactionType, CancellationToken cancellationToken)
    {
        var storedInteractionType = context.InteractionTypes.FirstOrDefault(type => type.Id == interactionType.Id);
        if (storedInteractionType is not null)
            return Task.CompletedTask;
        return context.InteractionTypes.AddAsync(interactionType, cancellationToken).AsTask();
    }

    private Task Add(AgreementType agreementType, CancellationToken cancellationToken)
    {
        var storedAgreementType = context.AgreementType.FirstOrDefault(type => type.Id == agreementType.Id);
        if (storedAgreementType is not null)
            return Task.CompletedTask;
        return context.AgreementType.AddAsync(agreementType, cancellationToken).AsTask();
    }

    private Task Add(AgreementStatus agreementStatus, CancellationToken cancellationToken)
    {
        var storedAgreementStatus = context.AgreementStatus.FirstOrDefault(status => status.Id == agreementStatus.Id);
        if (storedAgreementStatus is not null)
            return Task.CompletedTask;
        return context.AgreementStatus.AddAsync(agreementStatus, cancellationToken).AsTask();
    }

    private async Task AddPartnerTypes(CancellationToken cancellationToken)
    {
        var dictionary = new Dictionary<int, string>
        {
            { 1, "НИИ" },
            { 2, "ВУЗ" },
            { 3, "НПК" },
            { 4, "ЦНИИ" }
        };

        foreach (var pair in dictionary) await AddPartnerType(pair.Key, pair.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private Task AddPartnerType(int id, string name, CancellationToken cancellationToken)
    {
        var storedPartnerType = context.PartnerTypes.FirstOrDefault(t => t.Id == id);
        if (storedPartnerType is not null) return Task.CompletedTask;
        return context.PartnerTypes.AddAsync(new PartnerType
        {
            Id = id,
            Name = name
        }, cancellationToken).AsTask();
    }

    private async Task AddDirections(CancellationToken cancellationToken)
    {
        var dictionary = new Dictionary<int, string>
        {
            { 1, "АСУ ТП" },
            { 2, "МВЭ" },
            { 3, "САПР" },
            { 4, "ЭТПТ" }
        };

        foreach (var pair in dictionary) await AddDirection(pair.Key, pair.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private Task AddDirection(int id, string name, CancellationToken cancellationToken)
    {
        var storedDirection = context.Directions.FirstOrDefault(d => d.Id == id);
        if (storedDirection is not null)
            return Task.CompletedTask;
        return context.Directions.AddAsync(new Direction
        {
            Id = id,
            Name = name
        }, cancellationToken).AsTask();
    }

    private async Task AddFaculties(CancellationToken cancellationToken)
    {
        var dictionary = new Dictionary<int, string>
        {
            { 1, "ИФИО" },
            { 2, "ИНПРОТЕХ" },
            { 3, "ФРТ" },
            { 4, "ФЭЛ" },
            { 5, "ФКТИ" },
            { 6, "ФЭА" },
            { 7, "ФИБС" },
            { 8, "ГФ" }
        };

        foreach (
            var faculty in
            from faculty in
                dictionary
            let storedFaculty = context.Faculties.FirstOrDefault(f => f.Id == faculty.Key)
            where storedFaculty is null
            select faculty
        )
        {
            await context.Faculties.AddAsync(new Faculty { Id = faculty.Key, Name = faculty.Value },
                cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task AddPartners(CancellationToken cancellationToken)
    {
        var partners = 10.GetEnumerable().Select(Partner.Default);
        
        var partnersForSave = from partner in partners
            let storedPartner = context.Partners.FirstOrDefault(p => p.Id == partner.Id)
            where storedPartner is null
            select partner;

        foreach (var partner in partnersForSave)
        {
            // context.Attach(partner.PartnerType);
            context.Partners.Add(partner);
        }
        
        await context.SaveChangesAsync(cancellationToken);
    }
    
    
}