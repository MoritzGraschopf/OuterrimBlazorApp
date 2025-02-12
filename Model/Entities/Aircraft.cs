using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFTS")]
public class Aircraft(int id, int fuel, int speed, int altitude, string name, int specificationId)
{
    [Key]
    public int Id { get; set; } = id;

    public int Fuel { get; set; } = fuel;

    public int Speed { get; set; } = speed;

    public int Altitude { get; set; } = altitude;

    [StringLength(200)]
    public string Name { get; set; } = name;

    public int SpecificationId { get; set; } = specificationId;
    public AircraftSpecification AircraftSpecification { get; set; }

    public List<AircraftCrew> AircraftCrews { get; set; }
    public List<Compartment> Compartments { get; set; }
}