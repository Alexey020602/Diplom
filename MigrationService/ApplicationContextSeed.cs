using DataBase.Data;
using DataBase.Extensions;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

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
    private Task SeedThrows(CancellationToken cancellationToken) => !environment.IsDevelopment() ? Task.CompletedTask : SeedData(cancellationToken);

    private async Task SeedData(CancellationToken cancellationToken)
    {
        AddFaculties();
        AddPartnerTypes();
        AddDirections();
        AddInteractionTypes();
        AddAgreementStatuses();
        AddAgreementTypes();
        AddPartners();
        AddDivisions();
        AddAgreements();
        AddInteractions();
        
        await context.SaveChangesAsync(cancellationToken);
    }

    private void AddInteractionTypes()
    {
        var interactionTypes = new List<InteractionType>
        {
            new() { Id = 1, Name = "Первый" },
            new() { Id = 2, Name = "Второй" },
            new() { Id = 3, Name = "Третий" },
            new() { Id = 4, Name = "Четвертый" }
        };

        foreach (var interactionType in interactionTypes) Add(interactionType);
    }

    private void AddAgreementTypes()
    {
        var agreementTypes = new List<AgreementType>
        {
            new() { Id = 1, Name = "Первый" },
            new() { Id = 2, Name = "Вторый" },
            new() { Id = 3, Name = "Третий" },
            new() { Id = 4, Name = "Четвертый" }
        };

        foreach (var agreementType in agreementTypes) Add(agreementType);
    }

    private void AddAgreementStatuses()
    {
        var agreementsStatuses = new List<AgreementStatus>
        {
            new() { Id = 1, Name = "Ожидает" },
            new() { Id = 2, Name = "Действует" },
            new() { Id = 3, Name = "Приостановлено" },
            new() { Id = 4, Name = "Завершено" }
        };

        foreach (var agreementStatus in agreementsStatuses) Add(agreementStatus);
    }

    private void Add(InteractionType interactionType)
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

    private void AddPartnerTypes()
    {
        var dictionary = new Dictionary<int, string>
        {
            { 1, "НИИ" },
            { 2, "ВУЗ" },
            { 3, "НПК" },
            { 4, "ЦНИИ" }
        };

        foreach (var pair in dictionary) AddPartnerType(pair.Key, pair.Value);
    }

    private void AddPartnerType(int id, string name)
    {
        var storedPartnerType = context.PartnerTypes.FirstOrDefault(t => t.Id == id);
        if (storedPartnerType is not null) return;
        context.PartnerTypes.Add(new PartnerType
        {
            Id = id,
            Name = name
        });
    }

    private void AddDirections()
    {
        var dictionary = new Dictionary<int, string>
        {
            { 1, "АСУ ТП" },
            { 2, "МВЭ" },
            { 3, "САПР" },
            { 4, "ЭТПТ" }
        };

        foreach (var pair in dictionary) AddDirection(pair.Key, pair.Value);
    }

    private void AddDirection(int id, string name)
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

    private void AddFaculties()
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
    }

    private void AddPartners()
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddPartner(number);
        }
    }

    private void AddPartner(int number)
    {
        if (context.Partners.Any(p => p.Id == number)) return;
        
        var partner = Partner.Default(number);
        partner.Directions = context.Directions.Local.Where(d => d.Id == number.GetId(4, 1)).ToList();
        context.Partners.Add(partner);
    }

    private void AddDivisions()
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddDivision(number);
        }
    }

    private void AddDivision(int number)
    {
        if (context.Divisions.Any(d => d.Id == number)) return;
        var division = Division.Default(number);
        division.Directions = context.Directions.Local.Where(d => d.Id == number.GetId(4, 1)).ToList();
        division.Faculty = context.Faculties.Local.First(f => f.Id == number.GetId(8, 1));
        
        context.Divisions.Add(division);
    }

    private void AddAgreements()
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddAgreement(number);
        }
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

    private void AddInteractions()
    {
        foreach (var number in 10.GetEnumerable())
        {
            AddInteraction(number);
        }
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