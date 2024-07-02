using Model.Agreements;
using DataBase.Models;
using Agreement = DataBase.Models.Agreement;
using ModelAgreement = Model.Agreements.Agreement;
using Type = Model.Agreements.Type;
namespace Diploma.Extensions;

public static class AgreementsConvertExtension
{
    public static AgreementShort ConvertToShortModel(this Agreement agreement) => new(
        agreement.Id, 
        agreement.AgreementNumber, 
        ConvertToModel(agreement.AgreementType),
        ConvertToModel(agreement.AgreementStatus),
        agreement.StarDateTime,
        agreement.EndDateTime
        );
    public static Agreement ConvertToDatabaseModel(this ModelAgreement agreement) => new()
    {
        Id  = agreement.Id,
        AgreementNumber = agreement.Number,
        AgreementType = ConvertToDatabaseModel(agreement.Type!),
        AgreementStatus = ConvertToDatabaseModel(agreement.Status!),
        StarDateTime = agreement.Start,
        EndDateTime = agreement.End,
        DivisionInAgreements = ConvertToDatabaseModel(agreement.Divisions, agreement.Id),
        PartnerInAgreements = ConvertToDatabaseModel(agreement.Partners, agreement.Id),
    };
    public static AgreementType ConvertToDatabaseModel(this Type type) => new AgreementType()
        {
            Id = type.Id,
            Name = type.Name,
        };
    public static AgreementStatus ConvertToDatabaseModel(this Status status) =>  new AgreementStatus()
        {
            Id = status.Id,
            Name = status.Name,
        };
    
    public static List<DivisionInAgreement> ConvertToDatabaseModel(
        this IEnumerable<Model.Agreements.Division> newDivisions,
        int agreementId) =>
        newDivisions
            .Select(d => ConvertToDatabaseModel(d, agreementId))
            .ToList();
    public static DivisionInAgreement ConvertToDatabaseModel(this Model.Agreements.Division newDivision, int agreementId) => new DivisionInAgreement()
        {
            AgreementId = agreementId,
            DivisionId = newDivision.Id,
            ContactPersons = newDivision.ContactPersons,
        };
    public static List<PartnerInAgreement> ConvertToDatabaseModel(this IEnumerable<Model.Agreements.Partner> partners, int agreementId) =>
        partners
            .Select(p => ConvertToDatabaseModel(p, agreementId))
            .ToList();
    public static PartnerInAgreement ConvertToDatabaseModel(this Model.Agreements.Partner newPartner, int agreementId) => new ()
        {
            AgreementId = agreementId,
            PartnerId = newPartner.Id,
            ContactPersons = newPartner.ContactPersons,
        };
    
    public static ModelAgreement ConvertToModel(this Agreement agreement) => new()
    {
        Id = agreement.Id,
        Number = agreement.AgreementNumber,
        Type = ConvertToModel(agreement.AgreementType),
        Status = ConvertToModel(agreement.AgreementStatus),
        Start = agreement.StarDateTime,
        End = agreement.EndDateTime,
        Divisions = agreement.DivisionInAgreements.Select(ConvertToModel).ToList(),
        Partners = agreement.PartnerInAgreements.Select(ConvertToModel).ToList(),
    };

    private static Type ConvertToModel(this AgreementType type) => new()
    {
        Id = type.Id,
        Name = type.Name,
    };

    private static Status ConvertToModel(this AgreementStatus status) => new()
    {
        Id = status.Id,
        Name = status.Name,
    };

    private static Model.Agreements.Partner ConvertToModel(this PartnerInAgreement partner) => new()
    {
        Id = partner.PartnerId,
        Name = partner.Partner.ShortName,
        ContactPersons = partner.ContactPersons,
    };

    private static Model.Agreements.Division ConvertToModel(this DivisionInAgreement divisionInAgreement) => new()
    {
        Id = divisionInAgreement.DivisionId,
        Description = divisionInAgreement.Division.ShortName,
        ContactPersons = divisionInAgreement.ContactPersons,
    };
}