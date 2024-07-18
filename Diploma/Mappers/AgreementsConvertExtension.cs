using DataBase.Models;
using Model.Agreements;
using Model.Partners;
using Agreement = DataBase.Models.Agreement;
using AgreementType = Model.Agreements.AgreementType;
using DivisionInAgreement = DataBase.Models.DivisionInAgreement;
using ModelAgreement = Model.Agreements.Agreement;
using PartnerInAgreement = DataBase.Models.PartnerInAgreement;

namespace Diploma.Mappers;

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

    public static AgreementInPartner ConvertToPartnerModel(this Agreement agreement) => new AgreementInPartner
    {
        Id = agreement.Id,
        Description = agreement.ToString(),
    };
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
    public static DataBase.Models.AgreementType ConvertToDatabaseModel(this AgreementType agreementType) => new DataBase.Models.AgreementType()
        {
            Id = agreementType.Id,
            Name = agreementType.Name,
        };
    public static AgreementStatus ConvertToDatabaseModel(this Status status) =>  new AgreementStatus()
        {
            Id = status.Id,
            Name = status.Name,
        };
    
    public static List<DivisionInAgreement> ConvertToDatabaseModel(
        this IEnumerable<Model.Agreements.DivisionInAgreement> newDivisions,
        int agreementId) =>
        newDivisions
            .Select(d => ConvertToDatabaseModel(d, agreementId))
            .ToList();
    public static DivisionInAgreement ConvertToDatabaseModel(this Model.Agreements.DivisionInAgreement newDivisionInAgreement, int agreementId) => new DivisionInAgreement()
        {
            AgreementId = agreementId,
            DivisionId = newDivisionInAgreement.Id,
            ContactPersons = newDivisionInAgreement.ContactPersons,
        };
    public static List<PartnerInAgreement> ConvertToDatabaseModel(this IEnumerable<Model.Agreements.PartnerInAgreement> partners, int agreementId) =>
        partners
            .Select(p => ConvertToDatabaseModel(p, agreementId))
            .ToList();
    public static PartnerInAgreement ConvertToDatabaseModel(this Model.Agreements.PartnerInAgreement newPartnerInAgreement, int agreementId) => new ()
        {
            AgreementId = agreementId,
            PartnerId = newPartnerInAgreement.Id,
            ContactPersons = newPartnerInAgreement.ContactPersons,
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

    public static AgreementType ConvertToModel(this DataBase.Models.AgreementType type) => new()
    {
        Id = type.Id,
        Name = type.Name,
    };

    public static Status ConvertToModel(this AgreementStatus status) => new()
    {
        Id = status.Id,
        Name = status.Name,
    };

    public static Model.Agreements.PartnerInAgreement ConvertToModel(this PartnerInAgreement partner) => new()
    {
        Id = partner.PartnerId,
        Name = partner.Partner.ShortName,
        ContactPersons = partner.ContactPersons,
    };

    public static Model.Agreements.DivisionInAgreement ConvertToModel(this DivisionInAgreement divisionInAgreement) => new()
    {
        Id = divisionInAgreement.DivisionId,
        Description = divisionInAgreement.Division.ShortName,
        ContactPersons = divisionInAgreement.ContactPersons,
    };
}