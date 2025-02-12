using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class CrimeSyndicate
{
    public int Id { get; set; }

    [StringLength(120)]
    public string Name { get; set; }
    [StringLength(200)]
    public string Location { get; set; }
}