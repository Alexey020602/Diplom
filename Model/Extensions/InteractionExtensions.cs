using System.Data.Common;
using DataBase.Models;
using Model.Partners;
using Interaction = Model.Interactions.Interaction;
using DataBaseInteraction = DataBase.Models.Interaction;

namespace Model.Extensions;

public static class InteractionExtensions
{
    public static InteractionInPartner ConvertToPartnerModel(this DataBaseInteraction interaction) => new()
    {
        //TODO Доделать конвертацию
    };
    public static Interaction ConvertToModel(this DataBaseInteraction interaction) => new()
    {
        Id = interaction.Id,
        Directions = interaction.Directions.Select(DirectionExtensions.ConvertToModel).ToList()
        //TODO Доделать конвертацию и проверить остальные методы
    };

    public static DataBaseInteraction ConvertToDatabaseModel(this Interaction interaction) => new()
    {
        Id = interaction.Id,
        InteractionType = new ()
        {
            Id = interaction.Type!.Id,
            Name = interaction.Type!.Name
        },
        Theme = interaction.Theme,
        ContactCode = interaction.ContactCode,
        SigningDateTime = interaction.SigningDate,
        BeginigDateTime = interaction.Begin,
        EndingDateTime = interaction.End,
        Division = new()
        {
            Id = interaction.Division!.Id
        },
        Partner = new()
        {
            Id = interaction.Partner!.Id
        },
        Directions = interaction.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
    };

    public static Interactions.InteractionType ConvertToModel(this InteractionType type) => new()
    {
        Id = type.Id,
        Name = type.Name,
    };

    public static InteractionType ConvertToDao(this Interactions.InteractionType interactionType) => new()
    {
        Id = interactionType.Id,
        Name = interactionType.Name,
    };
}