using DataBase.Models;
using Model.Partners;
using Interaction = DataBase.Models.Interaction;
using Partner = DataBase.Models.Partner;

namespace Model.Extensions;

public static class PartnerExtensions
{
    public static Partners.Partner ConvertToModel(this Partner partner)
    {
        return new Partners.Partner
        {
            Id = partner.Id,
            FullName = partner.FullName,
            ShortName = partner.ShortName,
            Address = partner.Address ?? string.Empty,
            Site = partner.Site ?? string.Empty,
            ContactData = partner.ContactData ?? string.Empty,
            Type = partner.PartnerType.ConvertToModel(),
            Agreements = partner.PartnersInAgreement.Select(ConvertToModel).ToList(),
            Interactions = partner.Interactions.Select(ConvertToModel).ToList(),
            Directions = partner.Directions.Select(DirectionExtensions.ConvertToModel).ToList()
        };
    }

    public static AgreementInPartner ConvertToModel(this PartnerInAgreement partnerInAgreement)
    {
        return new AgreementInPartner
        {
            Id = partnerInAgreement.AgreementId,
            Description = partnerInAgreement.Agreement.ToString(),
            ContactPerson = partnerInAgreement.ContactPersons
        };
    }

    public static InteractionInPartner ConvertToModel(this Interaction interaction)
    {
        return new InteractionInPartner
        {
            Id = interaction.Id,
            Description = interaction.ToString()
        };
    }

    public static Partner ConvertToDao(this Partners.Partner partner)
    {
        return new Partner
        {
            Id = partner.Id,
            FullName = partner.FullName,
            ShortName = partner.ShortName,
            Address = partner.Address,
            Site = partner.Site,
            ContactData = partner.ContactData,
            PartnerType = (partner.Type ?? throw new ArgumentNullException("Type", "Тип партнера не задан"))
                .ConvertToDao(),
            Directions = partner.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
        };
    }
}