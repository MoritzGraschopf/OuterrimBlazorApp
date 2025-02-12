using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

[Table("AIRCRAFTS")]
public class Aircraft
{
    [Key]
    public int Id { get; set; }

    public int Fuel { get; set; }
    public int Speed { get; set; }
    public int Altitude { get; set; }

    [StringLength(200)]
    public string Name { get; set; }
}