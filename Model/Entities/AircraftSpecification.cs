using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFT_SPECIFICATIONS")]
public class AircraftSpecification
{
    [Key]
    public int Id { get; set; }

    public int Structure { get; set; }

    public int FuelTankCapacity { get; set; }

    public int MinSpeed { get; set; }

    public int MaxSpeed { get; set; }

    public int MaxAltitude { get; set; }

    [StringLength(45)]
    public string SpezificationCode { get; set; }

    public List<Aircraft> Aircrafts { get; set; }
}