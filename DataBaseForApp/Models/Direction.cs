using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models;

public class Direction
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    public ICollection<Partner> Partners { get; set; }
    public ICollection<Division> Divisions { get; set; }
    public ICollection<Interaction> Interactions { get; set; }
}