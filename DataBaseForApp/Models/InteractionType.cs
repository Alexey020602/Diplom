using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models;

public class InteractionType
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    public ICollection<Interaction> Interactions { get; set; }
}