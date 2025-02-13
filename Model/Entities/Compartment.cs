using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("COMPARTMENTS")]
public class Compartment
{
    public int Id { get; set; }
    public int AircraftId { get; set; }

    public Aircraft? Aircraft { get; set; }

    public List<Machinery> Machineries { get; set; }
}