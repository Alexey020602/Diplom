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
            new() { Id = 1, Name = "Лицензионное соглашение" },
            new() { Id = 2, Name = "НИОКР" },
            new() { Id = 3, Name = "Поставка" },
            new() { Id = 4, Name = "Продажа" },
            new() { Id = 5, Name = "Услуга" },
        };

        foreach (var interactionType in interactionTypes) Add(interactionType);
    }

    private void AddAgreementTypes()
    {
        var agreementTypes = new List<AgreementType>
        {
            new() { Id = 1, Name = "Договор о сотрудничестве" },
            new() { Id = 2, Name = "Договор о стратегическом партнерстве" },
            new() { Id = 3, Name = "Соглашение о сотрудничестве" },
            // new() { Id = 4, Name = "Четвертый" }
        };

        foreach (var agreementType in agreementTypes) Add(agreementType);
    }

    private void AddAgreementStatuses()
    {
        var agreementsStatuses = new List<AgreementStatus>
        {
            new() { Id = 2, Name = "Действует" },
            new() { Id = 3, Name = "Пролонгирован" },
            new() { Id = 1, Name = "Завершено" }
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
            { 4, "ЦНИИ" },
            {5, "Предприятие"},
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
        var directions = new List<Direction>
        {
            new(){ Id = 1, Name = "Автоматика и управление" },
            new() { Id = 2, Name = "Биомедицинская инженерия" },
            new() { Id = 3, Name = "Измерительные системы" },
            new() { Id = 4, Name = "Информационные системы и технологии" },
            new() { Id = 5, Name = "Радиотехника и телекоммуникация" },
            new() { Id = 6, Name = "Электроника" },
            new() { Id = 7, Name = "Электротехнологии" },
        };

        foreach (var direction in directions) AddDirection(direction);
    }

    private void AddDirection(Direction direction)
    {
        var storedDirection = context.Directions.FirstOrDefault(d => d.Id == direction.Id);
        if (storedDirection is not null)
            return;
        context.Directions.Add(direction);
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
        new List<Partner>
        {
            new ()
            {
                Id = 1,
                ShortName = """ОАО НИИ "Вектор" """,
                FullName = """ОАО НИИ "Вектор" """,
                PartnerType = context.PartnerTypes.Find(1)!,
                Site = "example.com",
                City = "Санкт-Петербург",
                Address = "197012, ул. Академика Павлова, д. 14 А",
                Directions = context.Directions.Local.Where(d => d.Id is 3 or 4 ).ToList(),
                ContactData = "Александров А.А.\n+79039847273\nsome@example.com"
            },
            new()
            {
                Id = 2,
                ShortName = """ЗАО "Светлана-Электроприбор" """,
                FullName = """ЗАО "Светлана-Электроприбор" """,
                PartnerType = context.PartnerTypes.Find(5)!,
                Site = "example.com",
                City = "Санкт-Петербург",
                Address = "197012, ул. Академика Павлова, д. 14 А",
                Directions = context.Directions.Local.Where(d => d.Id is 5 or 4 ).ToList(),
                ContactData = "Александров А.А.\n+79039847273\nsome@example.com"
            },
            new()
            {
                Id = 3,
                ShortName = """АО ПНН "Радар ММС" """,
                FullName = """АО ПНН "Радар ММС" """,
                PartnerType = context.PartnerTypes.Find(5)!,
                Site = "example.com",
                City = "Санкт-Петербург",
                Address = "197012, ул. Академика Павлова, д. 14 А",
                Directions = context.Directions.Local.Where(d => d.Id is 2 ).ToList(),
                ContactData = "Александров А.А.\n+79039847273\nsome@example.com"
            },
            new()
            {
                Id = 4,
                ShortName = """АО РИРВ """,
                FullName = """АО "Российский институт радионавигации и времени" """,
                PartnerType = context.PartnerTypes.Find(5)!,
                Site = "example.com",
                City = "Санкт-Петербург",
                Address = "197012, ул. Академика Павлова, д. 14 А",
                Directions = context.Directions.Local.Where(d => d.Id is 5 or 4 ).ToList(),
                ContactData = "Александров А.А.\n+79039847273\nsome@example.com"
            },
            new()
            {
                Id = 5,
                ShortName = """ОАО Холдинговая компания "Ленинец" """,
                FullName = """ОАО Холдинговая компания "Ленинец" """,
                PartnerType = context.PartnerTypes.Find(5)!,
                Site = "example.com",
                City = "Санкт-Петербург",
                Address = "197012, ул. Академика Павлова, д. 14 А",
                Directions = context.Directions.Local.Where(d => d.Id is 5 or 4 ).ToList(),
                ContactData = "Александров А.А.\n+79039847273\nsome@example.com"
            },
            new()
            {
                Id = 6,
                ShortName = "ФГУП НИИ Телевидения",
                FullName = "ФГУП НИИ Телевидения",
                PartnerType = context.PartnerTypes.Find(5)!,
                Site = "example.com",
                City = "Санкт-Петербург",
                Address = "197012, ул. Академика Павлова, д. 14 А",
                Directions = context.Directions.Local.Where(d => d.Id is 5 or 4 ).ToList(),
                ContactData = "Александров А.А.\n+79039847273\nsome@example.com"
            },
        }
        .ForEach(AddPartner);
    }

    private void AddPartner(Partner partner)
    {
        if (context.Partners.Any(p => p.Id == partner.Id)) return;
        
        context.Partners.Add(partner);
    }

    private void AddDivisions()
    {
        new List<Division> {
                new()
                {
                    Id = 1,
                    Contacts = "Иванов И.И.\n+798745612343",
                    ShortName = "Кафедра высшей математики",
                    FullName = "Кафедра высшей математики",
                    Faculty = context.Faculties.Find(1)!,
                    Site = "example.ru",
                    Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                },
                new()
                {
                    Id = 2,
                    Contacts = "Иванов И.И.\n+798745612343",
                    ShortName = "Кафедра физики",
                    FullName = "Кафедра физики",
                    Faculty = context.Faculties.Find(1)!,
                    Site = "example.ru",
                    Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                },
                new()
                {
                    Id = 3,
                    Contacts = "Иванов И.И.\n+798745612343",
                    ShortName = "Кафедра физической химии",
                    FullName = "Кафедра физической химии",
                    Faculty = context.Faculties.Find(1)!,
                    Site = "example.ru",
                    Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                },
                new()
                {
                    Id = 4,
                    Contacts = "Иванов И.И.\n+798745612343",
                    ShortName = "ПМИГ",
                    FullName = "Кафедра прикладной механики и инженерной графики",
                    Faculty = context.Faculties.Find(1)!,
                    Site = "example.ru",
                    Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                },
                new()
                {
                    Id = 5,
                    Contacts = "Иванов И.И.\n +798745612343",
                    ShortName = "ТОЭ",
                    FullName = "Кафедра теоретических основ электротехники",
                    Faculty = context.Faculties.Find(1)!,
                    Site = "example.ru",
                    Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                },
                new()
                {
                    Id = 6,
                    Contacts = "Иванов И.И.\n +798745612343",
                    ShortName = "ФВиС",
                    FullName = "Кафедра физического воспитания и спорта",
                    Faculty = context.Faculties.Find(1)!,
                    Site = "example.ru",
                    Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                },
            }.ForEach(AddDivision);
    }

    private void AddDivision(Division division)
    {
        if (context.Divisions.Any(d => d.Id == division.Id)) return;
        
        context.Divisions.Add(division);
    }

    private void AddAgreements()
    {
        new List<Agreement>
        {
            new()
            {
                Id = 1,
                AgreementNumber = "234-АБ/ЦИП/2021",
                AgreementStatus = context.AgreementStatus.Find(1)!,//3
                AgreementType = context.AgreementType.Find(1)!,
                StarDateTime = new DateTime(2021, 01, 01),
                EndDateTime = new DateTime(2021, 03, 01),
                PartnerInAgreements = 
                [
                    new PartnerInAgreement
                    {
                        AgreementId = 1,
                        PartnerId = 1,
                        ContactPersons = "Иванов И.И.\n+798745612343",
                    }
                ],
                DivisionInAgreements = 
                [
                    new DivisionInAgreement
                    {
                        AgreementId = 1,
                        DivisionId = 1,
                        ContactPersons = "Дмитриев Ф.С.\n+79874563802",
                    }
                ]
            },
            new()
            {
                Id = 2,
                AgreementNumber = "876-АБ/ЦИП/2021",
                AgreementStatus = context.AgreementStatus.Find(2)!,
                AgreementType = context.AgreementType.Find(2)!,
                StarDateTime = new DateTime(2023, 10, 01),
                EndDateTime = new DateTime(2024, 12, 01),
                PartnerInAgreements = 
                [
                    new PartnerInAgreement
                    {
                        AgreementId = 2,
                        PartnerId = 2,
                        ContactPersons = "Петров И.И.\n+70384212343",
                    }
                ],
                DivisionInAgreements = 
                [
                    new DivisionInAgreement
                    {
                        AgreementId = 2,
                        DivisionId = 2,
                        ContactPersons = "Фетисов А.В.\n+7978563802",
                    }
                ]
            },
            new()
            {
                Id = 3,
                AgreementNumber = "859-АБ/ЦИП/2023",
                AgreementStatus = context.AgreementStatus.Find(3)!,
                AgreementType = context.AgreementType.Find(3)!,
                StarDateTime = new DateTime(2025, 01, 01),
                EndDateTime = new DateTime(2027, 03, 01),
                PartnerInAgreements = 
                [
                    new PartnerInAgreement
                    {
                        AgreementId = 3,
                        PartnerId = 1,
                        ContactPersons = "Иванов И.И.\n+798745612343",
                    },
                    new PartnerInAgreement
                    {
                        AgreementId = 3,
                        PartnerId = 2,
                        ContactPersons = "Петров И.И.\n+70384212343",
                    }
                ],
                DivisionInAgreements = 
                [
                    new DivisionInAgreement
                    {
                        AgreementId = 3,
                        DivisionId = 1,
                        ContactPersons = "Дмитриев Ф.С.\n+79874563802",
                    },
                    new DivisionInAgreement
                    {
                        AgreementId = 3,
                        DivisionId = 2,
                        ContactPersons = "Фетисов А.В.\n+7978563802",
                    },
                ]
            }
        }.ForEach(AddAgreement);
    }

    private void AddAgreement(Agreement agreement)
    {
        if (context.Agreements.Any(a => a.Id == agreement.Id)) return;
        
        context.Agreements.Add(agreement);
    }

    private void AddInteractions()
    {
        new List<Interaction>
        {
            new()
            {
                Id = 1,
                SigningDateTime = new DateTime(2025, 4, 20),
                BeginigDateTime = new DateTime(2025, 5, 20),
                EndingDateTime = new DateTime(2025, 12, 20),
                Theme = "Тема НИОКР 1",
                ContactCode = "РЭС-1",
                InteractionType = context.InteractionTypes.Find(1)!,
                Directions = context.Directions.Local.Where(d => d.Id is 1 or 4 ).ToList(),
                Division = context.Divisions.Find(1)!,
                Partner = context.Partners.Find(1)!,
            },
            new()
            {
                Id = 2,
                SigningDateTime = new DateTime(2024, 2, 11),
                BeginigDateTime = new DateTime(2025, 5, 20),
                EndingDateTime = new DateTime(2025, 12, 20),
                Theme = "Тема НИОКР 3",
                ContactCode = "РЭС-3",
                InteractionType = context.InteractionTypes.Find(3)!,
                Directions = context.Directions.Local.Where(d => d.Id is 3 or 1 ).ToList(),
                Division = context.Divisions.Find(2)!,
                Partner = context.Partners.Find(2)!,
            },
            new()
            {
                Id = 3,
                SigningDateTime = new DateTime(2025, 1, 12),
                BeginigDateTime = new DateTime(2025, 5, 20),
                EndingDateTime = new DateTime(2025, 12, 20),
                Theme = "Тема НИОКР 2",
                ContactCode = "РЭС-2",
                InteractionType = context.InteractionTypes.Find(2)!,
                Directions = context.Directions.Local.Where(d => d.Id is 3 or 4).ToList(),
                Division = context.Divisions.Find(2)!,
                Partner = context.Partners.Find(2)!,
            },
        }.ForEach(AddInteraction);
    }

    private void AddInteraction(Interaction interaction)
    {
        if (context.Interactions.Any(i => i.Id == interaction.Id)) return;
        context.Interactions.Add(interaction);
    }
}