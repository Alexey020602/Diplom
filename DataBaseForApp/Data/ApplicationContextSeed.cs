using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data;

public static class ApplicationContextSeed
{
    public static void Seed(ApplicationContext context, ILogger logger, int retry = 0)
    {
        try
        {
            SeedThrows(context);
        }
        catch (Exception ex)
        {
            if (retry >= 10) throw;
            retry++;

            logger.LogError(ex.Message);
            Seed(context, logger, retry);
            throw;
        }
    }

    private static void SeedThrows(ApplicationContext context)
    {
        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }
    private static void AddPartnerTypes(DbSet<PartnerType> partnerTypes)
    {

    }
    private static void AddFaculties(DbSet<Faculty> faculties)
    {
        var dictionary = new Dictionary<int, string>()
        {
            {1, "ИФИО" },
            {2, "ИНПРОТЕХ" },
            {3, "ФРТ" },
            {4, "ФЭЛ" },
            {5, "ФКТИ" },
            {6, "ФЭА" },
            {7, "ФИБС" },
            {8, "ГФ" },
        };
       
        foreach (var faculty in dictionary)
        {
            var storedFaculty = faculties.FirstOrDefault(f => f.Id == faculty.Key);
            if (storedFaculty is null)
            {
                faculties.Add(new() { Id = faculty.Key, Name = faculty.Value });
            }
        }
    }
}
