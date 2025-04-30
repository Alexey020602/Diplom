using DataBase.Models;
using Model.Extensions;
using Model.Partners;
using Interaction = DataBase.Models.Interaction;
using Partner = DataBase.Models.Partner;

namespace Model.Mappers;

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
            Type = partner.PartnerType!.ConvertToModel(),
            Agreements = partner.PartnersInAgreement.Select(AgreementsConvertExtension.ConvertToAgreementInRelationship).ToList(),
            Interactions = partner.Interactions.Select(InteractionExtensions.ConvertToInteractionInPartner).ToList(),
            Directions = partner.Directions.Select(DirectionExtensions.ConvertToModel).ToList()
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
            PartnerType = (partner.Type ?? throw new ArgumentNullException(nameof(Partners.Partner.Type), "Тип партнера не задан"))
                .ConvertToDao(),
            Directions = partner.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
        };
    }

    public static Partner GetPartnerFromPartnerShort(this PartnerShort partner) => new Partner
    {
        Id = partner.Id
    };
}