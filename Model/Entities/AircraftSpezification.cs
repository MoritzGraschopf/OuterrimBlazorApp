using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFT_SPEZIFICATIONS")]
public class AircraftSpezification
{
    [Key]
    [Column("SPEZIFICATION_ID")]
    public int Id { get; set; }

    [Required]
    [Column("STRUCTURE")]
    public int Structure { get; set; }

    [Required]
    [Column("FUEL_TANK_CAPACITY")]
    public int FuelTankCapacity { get; set; }

    [Required]
    [Column("MIN_SPEED")]
    public int MinSpeed { get; set; }

    [Required]
    [Column("MAX_SPEED")]
    public int MaxSpeed { get; set; }

    [Required]
    [Column("MAX_ALTITUDE")]
    public int MaxAltitude { get; set; }

    [StringLength(45)]
    [Required]
    [Column("SPEZIFICATION_CODE")]
    public string SpezificationCode { get; set; }
}