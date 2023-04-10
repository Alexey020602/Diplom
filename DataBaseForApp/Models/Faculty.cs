using System.ComponentModel.DataAnnotations;

namespace DataBase.Models;

public class Faculty
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public ICollection<Division> Divisions { get; set; }
}