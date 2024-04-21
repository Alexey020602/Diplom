using DataBase.Models;
using Agreement = Model.Partners.Agreement;

namespace Diploma.Extensions.ModelToDao;

public static class PartnerExtensions
{
    public static Model.Partners.Partner ConvertToModel(this Partner partner) => new()
    {
        Id = partner.Id,
        FullName = partner.FullName,
        ShortName = partner.ShortName,
        Address = partner.Address ?? string.Empty,
        Site = partner.Site ?? string.Empty,
        ContactData = partner.ContactData ?? string.Empty,
        Type = partner.PartnerType.ConvertToModel(),
        Agreements = partner.PartnersInAgreement.Select(PartnerExtensions.ConvertToModel).ToList(),
        Interactions = partner.Interactions.Select(PartnerExtensions.ConvertToModel).ToList()
    };
    public static Agreement ConvertToModel(this PartnerInAgreement partnerInAgreement) => new()
    {
        Id = partnerInAgreement.AgreementId,
        Description = partnerInAgreement.Agreement.ToString(),
        ContactPerson = partnerInAgreement.ContactPersons,
    };
    public static Model.Partners.Interaction ConvertToModel(this Interaction interaction) => new()
    {   
        Id = interaction.Id,
        Description = interaction.ToString(),
    };
    public static Partner ConvertToDao(this Model.Partners.Partner partner) => new()
    {   
        Id = partner.Id,
        FullName = partner.FullName,
        ShortName = partner.ShortName,
        Address = partner.Address,
        Site = partner.Site,
        ContactData = partner.ContactData,
        PartnerType = (partner.Type ?? throw new ArgumentNullException("Type", "Тип партнера не задан")).ConvertToDao(),
    };
}