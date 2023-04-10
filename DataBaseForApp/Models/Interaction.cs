using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models;
public class Interaction
{
    public int Id { get; set; }
    public Partner Partner { get; set; }
    public Division Division { get; set; }
    public InteractionType InteractionType { get; set; }
    
    [MaxLength(500)]
    public string Theme { get; set; }
    
    [MaxLength(9)]
    public string ContactCode { get; set; }
    public DateTime SigningDateTime { get; set; }
    public DateTime BeginigDateTime { get; set; }
    public DateTime EndingDateTime { get; set; }
    public ICollection<Direction> Directions { get; set; }
}