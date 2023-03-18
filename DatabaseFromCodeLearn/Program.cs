// See https://aka.ms/new-console-template for more information

using DatabaseFromCodeLearn;
using Diploma.Models;
using Microsoft.EntityFrameworkCore;

var factory = new SampleContextFactory();

using (var context = factory.CreateDbContext(args))
{
    var partners = context.Partners.Include(nameof(PartnerType));
    foreach (var partner in partners)
    {
        Console.WriteLine($"Name:{partner.FullName} Type{partner.PartnerType.PartnerTypeName}");
    }
}