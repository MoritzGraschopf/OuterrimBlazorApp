using System.ComponentModel.DataAnnotations;

namespace View.Dto;

public class CrimeSyndicateDto
{
    [StringLength(120)] public string Name { get; set; } = "";
    [StringLength(200)] public string Location { get; set; } = "";
}