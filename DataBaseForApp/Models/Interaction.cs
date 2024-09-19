using System.ComponentModel.DataAnnotations;

namespace DataBase.Models;

public class Interaction
{
    public int Id { get; set; }
    public Partner Partner { get; set; } = null!;
    public Division Division { get; set; } = null!;
    public InteractionType InteractionType { get; set; } = null!;
    [MaxLength(500)] public string Theme { get; set; } = null!;
    [MaxLength(9)] public string ContactCode { get; set; } = null!;
    public DateTime SigningDateTime { get; set; }
    public DateTime BeginigDateTime { get; set; }
    public DateTime EndingDateTime { get; set; }
    public List<Direction> Directions { get; set; } = [];

    public override string ToString()
    {
        return $"{ContactCode} {InteractionType} от {SigningDateTime.ToShortDateString()}, {BeginigDateTime.ToShortDateString()} - {EndingDateTime.ToShortDateString()}";
    }
}