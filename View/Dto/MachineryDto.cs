using System.ComponentModel.DataAnnotations;

namespace View.Dto;

public class MachineryDto
{
    [StringLength(45)] public string Label { get; set; } = "";
    public string Function { get; set; } = "";

    public int CompartmentId { get; set; }

    public string MachineryType { get; set; } = "";
}