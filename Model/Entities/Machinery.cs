using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class Machinery
{
    public int Id { get; set; }

    [StringLength(45)]
    public string Label { get; set; }
    public string Function { get; set; }

    public int CompartmentId { get; set; }
    public Compartment Compartment { get; set; }
}