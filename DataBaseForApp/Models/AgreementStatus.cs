using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[Index("Name", IsUnique = true)]
public class AgreementStatus
{

    public int Id { get; set; }
    [MaxLength(13)] public string Name { get; set; }
    public ICollection<Agreement> Agreements { get; set; }
}