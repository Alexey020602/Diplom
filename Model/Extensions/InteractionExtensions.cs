using System.Data.Common;
using Interaction = Model.Interactions.Interaction;
using DataBaseInteraction = DataBase.Models.Interaction;

namespace Model.Extensions;

public static class InteractionExtensions
{
    public static Interaction ConvertToModel(this DataBaseInteraction interaction) => new()
    {
        Id = interaction.Id,
    };
}