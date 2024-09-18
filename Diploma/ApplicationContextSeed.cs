using DataBase.Data;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma;

public class ApplicationContextSeed(
    ApplicationContext context,
    ILogger<ApplicationContextSeed> logger,
    IWebHostEnvironment environment
    )
{
    public void Seed(int retry = 0)
    {
        try
        {
            SeedThrows();
        }
        catch (Exception ex)
        {
            if (retry >= 10) throw;
            retry++;
            logger.LogError("{Message}", ex.Message);
            Seed(retry);
            throw;
        }
    }

    private void Migrate()
    {
        if (!context.Database.IsRelational()) return;
        context.Database.Migrate();
    }

    private void SeedThrows()
    {
        // Migrate();
        if (!environment.IsDevelopment()) return;
        SeedData();
    }

    private void SeedData()
    {
        AddFaculties();
        AddPartnerTypes();
        AddDirections();
        AddInteractionTypes();
        AddAgreementStatuses();
        AddAgreementTypes();
        // AddUsers();
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

        context.SaveChanges();
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
        context.SaveChanges();
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

        context.SaveChanges();
    }

    private void Add(InteractionType interactionType)
    {
        var storedInteractionType = context.InteractionTypes.FirstOrDefault(type => type.Id == interactionType.Id);
        if (storedInteractionType is null) context.InteractionTypes.Add(interactionType);
    }

    private void Add(AgreementType agreementType)
    {
        var storedAgreementType = context.AgreementType.FirstOrDefault(type => type.Id == agreementType.Id);
        if (storedAgreementType is null) context.AgreementType.Add(agreementType);
    }

    private void Add(AgreementStatus agreementStatus)
    {
        var storedAgreementStatus = context.AgreementStatus.FirstOrDefault(status => status.Id == agreementStatus.Id);
        if (storedAgreementStatus is null) context.AgreementStatus.Add(agreementStatus);
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
        context.SaveChanges();
    }

    private void AddPartnerType(int id, string name)
    {
        var storedPartnerType = context.PartnerTypes.FirstOrDefault(t => t.Id == id);
        if (storedPartnerType is null)
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
        context.SaveChanges();
    }

    private void AddDirection(int id, string name)
    {
        var storedDirection = context.Directions.FirstOrDefault(d => d.Id == id);
        if (storedDirection is null)
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

        foreach (var faculty in dictionary)
        {
            var storedFaculty = context.Faculties.FirstOrDefault(f => f.Id == faculty.Key);
            if (storedFaculty is null) context.Faculties.Add(new Faculty { Id = faculty.Key, Name = faculty.Value });
        }

        context.SaveChanges();
    }

    // private void AddUsers()
    // {
    //     var hasher = new PasswordHasher<User>();
    //     var users = new List<User>
    //     {
    //     new ()
    //     {
    //         Id = "login",
    //         PasswordHash = hasher.HashPassword(null, "password"),
    //         Role = Role.Admin,
    //     }
    //     };
    //     foreach (var user in users)
    //     {
    //         var storedUser = context.Users.Find(user.Id);
    //         if (storedUser is null)
    //         {
    //             context.Users.Add(user);
    //         }
    //     }
    //
    //     context.SaveChanges();
    // }
}