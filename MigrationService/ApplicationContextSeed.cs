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
        await AddDivisions(cancellationToken);
        await AddAgreements(cancellationToken);
        await AddInteractions(cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
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

        foreach (var interactionType in interactionTypes) Add(interactionType, cancellationToken);

        // await context.SaveChangesAsync(cancellationToken);
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

        foreach (var agreementType in agreementTypes) Add(agreementType);
        // await context.SaveChangesAsync(cancellationToken);
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

        foreach (var agreementStatus in agreementsStatuses) Add(agreementStatus);

        // await context.SaveChangesAsync(cancellationToken);
    }

    private void Add(InteractionType interactionType, CancellationToken cancellationToken)
    {
        var storedInteractionType = context.InteractionTypes.FirstOrDefault(type => type.Id == interactionType.Id);
        if (storedInteractionType is not null)
            return;
        context.InteractionTypes.Add(interactionType);
    }

    private void Add(AgreementType agreementType)
    {
        var storedAgreementType = context.AgreementType.FirstOrDefault(type => type.Id == agreementType.Id);
        if (storedAgreementType is not null)
            return;
        context.AgreementType.Add(agreementType);
    }

    private void Add(AgreementStatus agreementStatus)
    {
        var storedAgreementStatus = context.AgreementStatus.FirstOrDefault(status => status.Id == agreementStatus.Id);
        if (storedAgreementStatus is not null)
            return;
        context.AgreementStatus.Add(agreementStatus);
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

        foreach (var pair in dictionary) AddPartnerType(pair.Key, pair.Value, cancellationToken);
        // await context.SaveChangesAsync(cancellationToken);
    }

    private void AddPartnerType(int id, string name, CancellationToken cancellationToken)
    {
        var storedPartnerType = context.PartnerTypes.FirstOrDefault(t => t.Id == id);
        if (storedPartnerType is not null) return;
        context.PartnerTypes.Add(new PartnerType
        {
            Id = id,
            Name = name
        });
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

        foreach (var pair in dictionary) AddDirection(pair.Key, pair.Value, cancellationToken);
        // await context.SaveChangesAsync(cancellationToken);
    }

    private void AddDirection(int id, string name, CancellationToken cancellationToken)
    {
        var storedDirection = context.Directions.FirstOrDefault(d => d.Id == id);
        if (storedDirection is not null)
            return;
        context.Directions.Add(new Direction
        {
            Id = id,
            Name = name
        });
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
            context.Faculties.Add(new Faculty { Id = faculty.Key, Name = faculty.Value });
        }

        // await context.SaveChangesAsync(cancellationToken);
    }

    private async Task AddPartners(CancellationToken cancellationToken)
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddPartner(number);
        }

        // await context.SaveChangesAsync(cancellationToken);
    }

    private void AddPartner(int number)
    {
        if (context.Partners.Any(p => p.Id == number)) return;
        
        var partner = Partner.Default(number);
        partner.Directions = context.Directions.Local.Where(d => d.Id == number.GetId(4, 1)).ToList();
        context.Partners.Add(partner);
    }

    private async Task AddDivisions(CancellationToken cancellationToken)
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddDivision(number);
        }

        // await context.SaveChangesAsync(cancellationToken);
    }

    private void AddDivision(int number)
    {
        if (context.Divisions.Any(d => d.Id == number)) return;
        var division = Division.Default(number);
        division.Directions = context.Directions.Local.Where(d => d.Id == number.GetId(4, 1)).ToList();
        division.Faculty = context.Faculties.Local.First(f => f.Id == number.GetId(8, 1));
        
        context.Divisions.Add(division);
    }

    private async Task AddAgreements(CancellationToken cancellationToken)
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddAgreement(number);
        }

        // await context.SaveChangesAsync(cancellationToken);
    }

    private void AddAgreement(int number)
    {
        if (context.Agreements.Any(a => a.Id == number)) return;
        
        var agreement = Agreement.Default(number);
        
        agreement.AgreementType = context.AgreementType.Local.First(type => type.Id == number.GetId(4, 1));
        agreement.AgreementStatus = context.AgreementStatus.Local.First(type => type.Id == number.GetId(4, 1));
        agreement.PartnerInAgreements =
        [
            new()
            {
                AgreementId = number,
                PartnerId = number,
                ContactPersons = $"Контактные данные лица от партнера {number}"
            },
        ];
        agreement.DivisionInAgreements =
        [
            new()
            {
                AgreementId = number,
                DivisionId = number,
                ContactPersons = $"Контактные данные лица от подразделения {number}",
            }
        ];
        
        context.Agreements.Add(agreement);
    }

    private async Task AddInteractions(CancellationToken cancellationToken)
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddInteraction(number);
        }

        // await context.SaveChangesAsync(cancellationToken);
    }

    private void AddInteraction(int number)
    {
        if (context.Interactions.Any(i => i.Id == number)) return;
        
        var interaction = Interaction.Default(number);
        
        interaction.Directions = context.Directions.Local.Where(d => d.Id == number.GetId(4, 1)).ToList();
        interaction.InteractionType = context.InteractionTypes.Local.First(type => type.Id == number.GetId(4, 1));
        interaction.Partner = context.Partners.Local.First(p => p.Id == number);
        interaction.Division = context.Divisions.Local.First(d => d.Id == number);
        context.Interactions.Add(interaction);
    }
}