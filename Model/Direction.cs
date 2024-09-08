using System.ComponentModel.DataAnnotations;

namespace Model;

public class Direction
{
    public int Id { get; set; }
    [StringLength(200)] public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}