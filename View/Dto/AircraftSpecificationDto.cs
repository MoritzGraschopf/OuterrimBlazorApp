using System.ComponentModel.DataAnnotations;

namespace View.Dto;

public class AircraftSpecificationDto
{
    public int Structure { get; set; }

    public int FuelTankCapacity { get; set; }

    public int MinSpeed { get; set; }

    public int MaxSpeed { get; set; }

    public int MaxAltitude { get; set; }

    [StringLength(45)] public string SpecificationCode { get; set; } = "";
}