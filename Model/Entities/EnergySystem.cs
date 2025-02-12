using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("ENERGY_SYSTEMS")]
public class EnergySystem
{
    public int MachineryId { get; set; }
    public Machinery Machinery { get; set; }
}