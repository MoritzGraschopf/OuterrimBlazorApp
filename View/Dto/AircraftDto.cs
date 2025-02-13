using System.ComponentModel.DataAnnotations;

namespace View.Dto;

public class AircraftDto
{
    public int Fuel { get; set; }
    public int Speed { get; set; }
    public int Altitude { get; set; }
    [StringLength(200)] public string Name { get; set; } = "";
    public int SpecificationId { get; set; }
}