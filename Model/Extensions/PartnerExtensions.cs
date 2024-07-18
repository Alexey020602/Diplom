using DataBase.Models;
using Model.Partners;
using Interaction = DataBase.Models.Interaction;
using Partner = DataBase.Models.Partner;

namespace Model.Extensions;

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
        Interactions = partner.Interactions.Select(PartnerExtensions.ConvertToModel).ToList(),
        Directions = partner.Directions.Select(DirectionExtensions.ConvertToModel).ToList(),
    };
    public static AgreementInPartner ConvertToModel(this PartnerInAgreement partnerInAgreement) => new()
    {
        Id = partnerInAgreement.AgreementId,
        Description = partnerInAgreement.Agreement.ToString(),
        ContactPerson = partnerInAgreement.ContactPersons,
    };
    public static InteractionInPartner ConvertToModel(this Interaction interaction) => new()
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
        Directions = partner.Directions.Select(DirectionExtensions.ConvertToDao).ToList(),
    };
}