using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFTS")]
public class Aircraft
{
    [Key]
    [Column("AIRCRAFT_ID")]
    public int Id { get; set; }

    [Required]
    [Column("FUEL")]
    public int Fuel { get; set; }
    [Required]
    [Column("SPEED")]
    public int Speed { get; set; }
    [Required]
    [Column("ALTITUDE")]
    public int Altitude { get; set; }

    [StringLength(200)]
    [Required]
    [Column("NAME")]
    public string Name { get; set; }

    public int SpezificationId { get; set; }
    public AircraftSpezification AircraftSpezification { get; set; }
}