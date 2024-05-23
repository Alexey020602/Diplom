using System.Data.Common;
using Interaction = Model.Interactions.Interaction;
using DataBaseInteraction = DataBase.Models.Interaction;

namespace Model.Extensions;

public static class InteractionExtensions
{
    public static Interaction ConvertToModel(this DataBaseInteraction interaction) => new()
    {
        Id = interaction.Id,
        Directions = interaction.Directions.Select(DirectionExtensions.ConvertToModel).ToList()
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
}