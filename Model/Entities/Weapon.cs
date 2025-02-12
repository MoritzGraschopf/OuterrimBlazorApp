using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("WEAPONS")]
public class Weapon
{
    public int MachineryId { get; set; }
    public Machinery Machinery { get; set; }
}