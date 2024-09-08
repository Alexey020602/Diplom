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
    public static AgreementShort ConvertToShortModel(this Agreement agreement)
    {
        return new AgreementShort(
            agreement.Id,
            agreement.AgreementNumber,
            ConvertToModel(agreement.AgreementType),
            ConvertToModel(agreement.AgreementStatus),
            agreement.StarDateTime,
            agreement.EndDateTime
        );
    }

    public static AgreementInPartner ConvertToPartnerModel(this Agreement agreement)
    {
        return new AgreementInPartner()
        {
            Id = agreement.Id,
            Description = agreement.ToString()
        };
    }

    public static Agreement ConvertToDatabaseModel(this ModelAgreement agreement)
    {
        return new Agreement
        {
            Id = agreement.Id,
            AgreementNumber = agreement.Number,
            AgreementType = ConvertToDatabaseModel(agreement.Type!),
            AgreementStatus = ConvertToDatabaseModel(agreement.Status!),
            StarDateTime = agreement.Start,
            EndDateTime = agreement.End,
            DivisionInAgreements = ConvertToDatabaseModel(agreement.Divisions, agreement.Id),
            PartnerInAgreements = ConvertToDatabaseModel(agreement.Partners, agreement.Id)
        };
    }

    public static DataBase.Models.AgreementType ConvertToDatabaseModel(this AgreementType agreementType)
    {
        return new DataBase.Models.AgreementType()
        {
            Id = agreementType.Id,
            Name = agreementType.Name
        };
    }

    public static AgreementStatus ConvertToDatabaseModel(this Status status)
    {
        return new AgreementStatus()
        {
            Id = status.Id,
            Name = status.Name
        };
    }

    public static List<DivisionInAgreement> ConvertToDatabaseModel(
        this IEnumerable<Model.Agreements.DivisionInAgreement> newDivisions,
        int agreementId)
    {
        return newDivisions
            .Select(d => ConvertToDatabaseModel(d, agreementId))
            .ToList();
    }

    public static DivisionInAgreement ConvertToDatabaseModel(
        this Model.Agreements.DivisionInAgreement newDivisionInAgreement, int agreementId)
    {
        return new DivisionInAgreement()
        {
            AgreementId = agreementId,
            DivisionId = newDivisionInAgreement.Id,
            ContactPersons = newDivisionInAgreement.ContactPersons
        };
    }

    public static List<PartnerInAgreement> ConvertToDatabaseModel(
        this IEnumerable<Model.Agreements.PartnerInAgreement> partners, int agreementId)
    {
        return partners
            .Select(p => ConvertToDatabaseModel(p, agreementId))
            .ToList();
    }

    public static PartnerInAgreement ConvertToDatabaseModel(
        this Model.Agreements.PartnerInAgreement newPartnerInAgreement, int agreementId)
    {
        return new PartnerInAgreement
        {
            AgreementId = agreementId,
            PartnerId = newPartnerInAgreement.Id,
            ContactPersons = newPartnerInAgreement.ContactPersons
        };
    }

    public static ModelAgreement ConvertToModel(this Agreement agreement)
    {
        return new ModelAgreement
        {
            Id = agreement.Id,
            Number = agreement.AgreementNumber,
            Type = ConvertToModel(agreement.AgreementType),
            Status = ConvertToModel(agreement.AgreementStatus),
            Start = agreement.StarDateTime,
            End = agreement.EndDateTime,
            Divisions = agreement.DivisionInAgreements.Select(ConvertToModel).ToList(),
            Partners = agreement.PartnerInAgreements.Select(ConvertToModel).ToList()
        };
    }

    public static AgreementType ConvertToModel(this DataBase.Models.AgreementType type)
    {
        return new AgreementType
        {
            Id = type.Id,
            Name = type.Name
        };
    }

    public static Status ConvertToModel(this AgreementStatus status)
    {
        return new Status
        {
            Id = status.Id,
            Name = status.Name
        };
    }

    public static Model.Agreements.PartnerInAgreement ConvertToModel(this PartnerInAgreement partner)
    {
        return new Model.Agreements.PartnerInAgreement
        {
            Id = partner.PartnerId,
            Name = partner.Partner.ShortName,
            ContactPersons = partner.ContactPersons
        };
    }

    public static Model.Agreements.DivisionInAgreement ConvertToModel(this DivisionInAgreement divisionInAgreement)
    {
        return new Model.Agreements.DivisionInAgreement
        {
            Id = divisionInAgreement.DivisionId,
            Description = divisionInAgreement.Division.ShortName,
            ContactPersons = divisionInAgreement.ContactPersons
        };
    }
}