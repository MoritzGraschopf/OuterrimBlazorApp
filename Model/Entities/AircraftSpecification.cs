using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFT_SPECIFICATIONS")]
public class AircraftSpecification(
    int id,
    int structure,
    int fuelTankCapacity,
    int minSpeed,
    int maxSpeed,
    int maxAltitude,
    string specificationCode)
{
    [Key]
    public int Id { get; set; } = id;

    public int Structure { get; set; } = structure;

    public int FuelTankCapacity { get; set; } = fuelTankCapacity;

    public int MinSpeed { get; set; } = minSpeed;

    public int MaxSpeed { get; set; } = maxSpeed;

    public int MaxAltitude { get; set; } = maxAltitude;

    [StringLength(45)]
    public string SpecificationCode { get; set; } = specificationCode;

    public List<Aircraft> Aircrafts { get; set; }
}