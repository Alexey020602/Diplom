using Model.Extensions;
using Model.Interactions;
using Model.Partners;
using Interaction = Model.Interactions.Interaction;
using DataBaseInteraction = DataBase.Models.Interaction;
using InteractionType = Model.Interactions.InteractionType;
using Partner = DataBase.Models.Partner;

namespace Model.Mappers;

public static class InteractionExtensions
{
    public static InteractionShort ConvertToInteractionShort(this DataBaseInteraction interaction) => 
        new InteractionShort(interaction.Id, interaction.Description());
    public static InteractionInRelationship ConvertToPartnerModel(this DataBaseInteraction interaction)
    {
        return new InteractionInRelationship
        {
            Id = interaction.Id,
            Description = interaction.Description()
        };
    }
    public static DataBaseInteraction ConvertToDatabaseModel(this Interaction interaction)
    {
        return new DataBaseInteraction
        {
            Id = interaction.Id,
            InteractionType = ConvertToDatabaseModel(interaction.Type!),
            Theme = interaction.Theme,
            ContactCode = interaction.ContactCode,
            SigningDateTime = interaction.SigningDate.ToDateTime(default),
            BeginigDateTime = interaction.Begin.ToDateTime(default),
            EndingDateTime = interaction.End.ToDateTime(default),
            Division = interaction.Division!.GetDivisionFormDivisionShort(),
            Partner = interaction.Partner!.GetPartnerFromPartnerShort(),
            Directions = interaction.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
        };
    }
    private static DataBase.Models.InteractionType ConvertToDatabaseModel(this InteractionType interactionType)
    {
        return new ()
        {
            Id = interactionType.Id,
            Name = interactionType.Name
        };
    }
    public static Interaction ConvertToModel(this DataBaseInteraction interaction)
    {
        return new Interaction
        {
            Id = interaction.Id,
            Directions = interaction.Directions.Select(DirectionExtensions.ConvertToModel).ToList(),
            Partner = interaction.Partner.ConvertToPartnerShort(),
            Division = interaction.Division.ConvertToDivisionShort(),
            Type = interaction.InteractionType.ConvertToModel(),
            Theme = interaction.Theme,
            ContactCode = interaction.ContactCode,
            SigningDate = DateOnly.FromDateTime(interaction.SigningDateTime),
            Begin = DateOnly.FromDateTime(interaction.BeginigDateTime),
            End = DateOnly.FromDateTime(interaction.EndingDateTime)
        };
    }

    // public static DataBaseInteraction ConvertToDatabaseModel(this Interaction interaction)
    // {
    //     return new DataBaseInteraction
    //     {
    //         Id = interaction.Id,
    //         InteractionType = new DataBase.Models.InteractionType
    //         {
    //             Id = interaction.Type!.Id,
    //             Name = interaction.Type!.Name
    //         },
    //         Theme = interaction.Theme,
    //         ContactCode = interaction.ContactCode,
    //         SigningDateTime = interaction.SigningDate.ToDateTime(default),
    //         BeginigDateTime = interaction.Begin.ToDateTime(default),
    //         EndingDateTime = interaction.End.ToDateTime(default),
    //         Division = new Division
    //         {
    //             Id = interaction.Division!.Id
    //         },
    //         Partner = new Partner
    //         {
    //             Id = interaction.Partner!.Id
    //         },
    //         Directions = interaction.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
    //     };
    // }

    public static InteractionType ConvertToModel(this DataBase.Models.InteractionType type)
    {
        return new InteractionType
        {
            Id = type.Id,
            Name = type.Name
        };
    }

    public static DataBase.Models.InteractionType ConvertToDao(this InteractionType interactionType)
    {
        return new DataBase.Models.InteractionType
        {
            Id = interactionType.Id,
            Name = interactionType.Name
        };
    }


    public static InteractionInRelationship ConvertToInteractionInPartner(this DataBaseInteraction interaction)
    {
        return new InteractionInRelationship
        {
            Id = interaction.Id,
            Description = interaction.Description()
        };
    }
    
    private static string Description(this DataBase.Models.Interaction interaction) => $"{interaction.InteractionType.Name} {interaction.Theme} ({interaction.SigningDateTime.ToShortDateString()})";
}